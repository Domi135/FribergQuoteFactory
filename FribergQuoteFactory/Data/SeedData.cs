using FribergQuoteFactory.Models;

namespace FribergQuoteFactory.Data
{
    public static class SeedData
    {
        public static async Task Initialize(FribergQuoteFactoryContext context)
        {
            if(!context.Quotes.Any())
            {
                var quotes = new List<Quote>
            {
                // Entrepreneurship
                new Quote { Id = Guid.NewGuid(), QuoteText = "The best way to predict the future is to create it.", Category = "Entrepreneurship", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Don't worry about failure; you only have to be right once.", Category = "Entrepreneurship", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Success usually comes to those who are too busy to be looking for it.", Category = "Entrepreneurship", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Your time is limited, so don’t waste it living someone else’s life.", Category = "Entrepreneurship", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Ideas are easy. Implementation is hard.", Category = "Entrepreneurship", Approved = false },

                // Self-development
                new Quote { Id = Guid.NewGuid(), QuoteText = "Strive not to be a success, but rather to be of value.", Category = "Self-development", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "What you get by achieving your goals is not as important as what you become by achieving your goals.", Category = "Self-development", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Be not afraid of growing slowly, be afraid only of standing still.", Category = "Self-development", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Invest in yourself. Your career is the engine of your wealth.", Category = "Self-development", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "The only person you are destined to become is the person you decide to be.", Category = "Self-development", Approved = false },

                // Motivation
                new Quote { Id = Guid.NewGuid(), QuoteText = "Life is 10% what happens to us and 90% how we react to it.", Category = "Motivation", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Believe you can and you're halfway there.", Category = "Motivation", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "The harder you work for something, the greater you'll feel when you achieve it.", Category = "Motivation", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Don’t watch the clock; do what it does. Keep going.", Category = "Motivation", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Push yourself, because no one else is going to do it for you.", Category = "Motivation", Approved = false },

                // Leadership
                new Quote { Id = Guid.NewGuid(), QuoteText = "A leader is one who knows the way, goes the way, and shows the way.", Category = "Leadership", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Leadership is not about being in charge. It's about taking care of those in your charge.", Category = "Leadership", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "The greatest leader is not necessarily the one who does the greatest things. He is the one that gets the people to do the greatest things.", Category = "Leadership", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Before you are a leader, success is all about growing yourself. When you become a leader, success is all about growing others.", Category = "Leadership", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Earn your leadership every day.", Category = "Leadership", Approved = false },

                // Success
                new Quote { Id = Guid.NewGuid(), QuoteText = "Success is not final, failure is not fatal: It is the courage to continue that counts.", Category = "Success", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "The road to success and the road to failure are almost exactly the same.", Category = "Success", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Success is walking from failure to failure with no loss of enthusiasm.", Category = "Success", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "Don’t be afraid to give up the good to go for the great.", Category = "Success", Approved = true },
                new Quote { Id = Guid.NewGuid(), QuoteText = "I never dreamed about success. I worked for it.", Category = "Success", Approved = false }
            };

                context.Quotes.AddRange(quotes);
                await context.SaveChangesAsync();
            }
        }
    }
}
