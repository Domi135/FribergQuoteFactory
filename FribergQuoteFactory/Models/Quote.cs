namespace FribergQuoteFactory.Models
{
    public class Quote
    {
        public Guid Id { get; set; }
        public string QuoteText { get; set; }
        public string Category { get; set; }
        public bool Approved { get; set; }
    }
}
