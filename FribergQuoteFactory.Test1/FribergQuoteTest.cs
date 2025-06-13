using FribergQuoteFactory.Repositories;
using FribergQuoteFactory;
using FribergQuoteFactory.Models;
using Microsoft.EntityFrameworkCore;
using FribergQuoteFactory.Data;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FribergQuoteFactory.Test1
{
    public class TestQuoteRepository
    {
        [Fact]
        public async Task TestGetRandomQuote()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FribergQuoteFactoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new FribergQuoteFactoryContext(options);
            var quoteRepository = new QuoteRepository(context);

            context.Quotes.Add(new Quote
            {
                Id = Guid.NewGuid(),
                QuoteText = "Test quote",
                Category = "motivation",
                Approved = true

            });
            await context.SaveChangesAsync();

            //Act
            var quote = await quoteRepository.GetRandomQuote();

            //Assert
            Assert.NotNull(quote);
        }

        [Fact]
        public async Task TestGetRandomQuoteByCategory()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FribergQuoteFactoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new FribergQuoteFactoryContext(options);
            var quoteRepository = new QuoteRepository(context);

            context.Quotes.Add(new Quote
            {
                Id = Guid.NewGuid(),
                QuoteText = "Test quote",
                Category = "motivation",
                Approved = true

            });
            await context.SaveChangesAsync();

            var category = "motivation";
            //Act
            var quote = await quoteRepository.GetRandomQuoteByCategory("motivation");
            //Assert
            Assert.Contains(category, quote.Category);
        }

        [Fact]
        public async Task TestAddQuoteAsync()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FribergQuoteFactoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new FribergQuoteFactoryContext(options);
            var quoteRepository = new QuoteRepository(context);

            var quote = new Quote
            {
                Id = Guid.NewGuid(),
                QuoteText = "Test Quote",
                Category = "Test Category",
                Approved = true
            };

            // Act
            await quoteRepository.AddQuoteAsync(quote);
            await context.SaveChangesAsync();
            var addedQuote = await quoteRepository.GetRandomQuote();
            // Assert
            Assert.NotNull(addedQuote);
            Assert.Equal(quote.QuoteText, addedQuote?.QuoteText);
        }

        [Fact]
        public async Task TestUnApprovedQuotes()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FribergQuoteFactoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new FribergQuoteFactoryContext(options);
            var quoteRepository = new QuoteRepository(context);

            var quote = new Quote
            {
                Id = Guid.NewGuid(),
                QuoteText = "Test Quote",
                Category = "Test Category",
                Approved = false
            };

            //Act
            var unapprovedQuote = await quoteRepository.GetUnapprovedQuotes();

            //Assert
            Assert.NotNull(quote);
        }

        [Fact]
        public async Task TestApproveQuoteAsync()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FribergQuoteFactoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new FribergQuoteFactoryContext(options);
            var quoteRepository = new QuoteRepository(context);

            var quote = new Quote
            {
                Id = Guid.NewGuid(),
                QuoteText = "Test Quote",
                Category = "Test Category",
                Approved = false
            };

            await quoteRepository.AddQuoteAsync(quote);
            await context.SaveChangesAsync();

            // Act
            await quoteRepository.ApproveQuoteAsync(quote.Id);

            // Assert
            var approvedQuote = await context.Quotes.FindAsync(quote.Id);
            Assert.NotNull(approvedQuote);
            Assert.True(approvedQuote.Approved);
        }
    }
}