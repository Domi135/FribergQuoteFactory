using FribergQuoteFactory.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FribergQuoteFactory.Integrationtest
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Skapa en isolerad servicemiljö
            builder.ConfigureServices(services =>
            {
                // Ta bort befintlig DbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<FribergQuoteFactoryContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Lägg till en in-memory testdatabas
                services.AddDbContext<FribergQuoteFactoryContext>(options =>
                {
                    options.UseInMemoryDatabase("FribergQuoteFactoryTestDb");
                });

                // (Valfritt) Bygg service provider och seeda databasen om du vill
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<FribergQuoteFactoryContext>();
                    db.Database.EnsureCreated();

                    // SeedData(db); // <-- Lägg till testdata här om du vill
                }
            });

            return base.CreateHost(builder);
        }
    }
}
