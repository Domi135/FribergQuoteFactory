using FribergQuoteFactory.Data;
using FribergQuoteFactory.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace FribergQuoteFactory.Integrationtest
{
    public class FribergQuoteIntegratoinstests
    {
        private static DbContextOptions<FribergQuoteFactoryContext> _dbContextOptions = new DbContextOptionsBuilder<FribergQuoteFactoryContext>()
            .UseInMemoryDatabase(databaseName: "FribergQuoteFactoryTestDb")
            .Options;

        private WebApplicationFactory<Program> _webAppFactory;
        private HttpClient _httpClient;

        //Konstruktor för integrationstestning
        public FribergQuoteIntegratoinstests()
        {
            _webAppFactory = new CustomWebApplicationFactory();
            _httpClient = _webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetAllQuotes_ShouldReturnStatusCode200()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync("/api/Quote");
            var stringResult = await response.Content.ReadAsStringAsync();
            //Assert
            var quotes = await response.Content.ReadFromJsonAsync<List<Quote>>();
            response.EnsureSuccessStatusCode();
            Assert.NotNull(stringResult);
        }

        [Fact]
        public async Task GetRandomQuote_ShouldReturnStatusCode200()
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/api/Quote/random");
            var stringResult = await response.Content.ReadAsStringAsync();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(stringResult);
        }

        [Fact]
        public async Task GetUnapproved_ShouldReturn200()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync("/api/Quote/unapproved");
            var stringResult = await response.Content.ReadAsStringAsync();
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(stringResult);
        }

        [Fact]
        public async Task GetRandomQuoteByCategory_ShouldReturnStatusCode200()
        {
            //Arrange
            var category = "motivation";
            //Act
            var response = await _httpClient.GetAsync("/api/Quote/random?category=" + category);
            var stringResult = await response.Content.ReadAsStringAsync();
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(stringResult);
        }

        [Fact]
        public async Task AddQuote_ShouldReturnStatusCode201()
        {
            // Arrange
            var newQuote = new
            {
                Id = Guid.NewGuid(),
                QuoteText = "Test quote for integration test",
                Category = "TestCategory",
                Approved = false
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/Quote", newQuote);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test quote for integration test", content);
        }

        [Fact]
        public async Task ApproveQuote_ShouldReturnStatusCode204()
        {
            // Arrange
            var newQuote = new Quote
            {
                Id = Guid.NewGuid(),
                QuoteText = "Pending quote",
                Category = "Test",
                Approved = false
            };

            var createResponse = await _httpClient.PostAsJsonAsync("/api/Quote", newQuote);
            createResponse.EnsureSuccessStatusCode();

            // Act
            var approveResponse = await _httpClient.PutAsync($"/api/Quote/approve/{newQuote.Id}", null);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, approveResponse.StatusCode);

            var getResponse = await _httpClient.GetAsync($"/api/Quote/{newQuote.Id}");
            getResponse.EnsureSuccessStatusCode();

            var updatedQuote = await getResponse.Content.ReadFromJsonAsync<Quote>();
            Assert.NotNull(updatedQuote);
            Assert.True(updatedQuote.Approved);
        }
    }
}