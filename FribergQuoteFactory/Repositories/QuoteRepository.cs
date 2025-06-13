
using FribergQuoteFactory.Data;
using FribergQuoteFactory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

namespace FribergQuoteFactory.Repositories
{
    public class QuoteRepository
    {
        private readonly FribergQuoteFactoryContext context;
        private readonly Random random = new ();

        public QuoteRepository()
        {
        }

        public QuoteRepository(FribergQuoteFactoryContext context)
        {
            this.context = context;
        }

        public async Task AddQuoteAsync(Quote quote)
        {
            await context.Quotes.AddAsync(quote);
            await context.SaveChangesAsync();
        }

        public async Task ApproveQuoteAsync(Guid quoteId)
        {
            var quote = await context.Quotes.FirstOrDefaultAsync(q => q.Id == quoteId);

            if (quote == null)
            {
                 throw new InvalidOperationException("Quote not found.");
            }

            quote.Approved = true;
            context.Quotes.Update(quote);
            await context.SaveChangesAsync();
        }

        public async Task<Quote?> GetRandomQuote()
        {
            int count = context.Quotes.Count();
            if(count == 0)
            {
                return null;
            }
            int index = random.Next(count);
            return await context.Quotes.Skip(index).FirstOrDefaultAsync();
        }

        public async Task<Quote?> GetRandomQuoteByCategory(string category)
        {
            int count = await context.Quotes
                .Where(q => q.Category == category)
                .CountAsync();

            if (count == 0)
                return null;

            int index = random.Next(count);

            return await context.Quotes
                .Where(q => q.Category == category)
                .Skip(index)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Quote>> GetUnapprovedQuotes()
        {
            return await context.Quotes.Where(q => !q.Approved)
                .ToListAsync();
        }
    }
}
