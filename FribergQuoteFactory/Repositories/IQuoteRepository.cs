using FribergQuoteFactory.Models;

namespace FribergQuoteFactory.Repositories
{
    public interface IQuoteRepository
    {
        Task<List<Quote>>GetAll();
        Task<List<Quote>> GetUnapproved();
        Task<Quote?> GetRandomQuote();
        Task<Quote?> GetRandomQuoteByCategory(string category);
        Task AddQuoteAsync(Quote quote);
        Task ApproveQuoteAsync(Guid id);
    }
}
