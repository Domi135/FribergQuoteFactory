using FribergQuoteFactory.Data;
using FribergQuoteFactory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FribergQuoteFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly FribergQuoteFactoryContext _context;
        public QuoteController(FribergQuoteFactoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetAll()
        {
            var quotes = await _context.Quotes.ToListAsync();
            if (quotes == null || !quotes.Any())
            {
                return NotFound("No quotes available.");
            }
            return quotes;
        }


        [HttpGet("random")]
        public async Task<ActionResult<Quote>> GetRandomQuote()
        {
            var count = await _context.Quotes.CountAsync();
            if (count == 0)
            {
                return NotFound("No quotes available.");
            }

            var randomIndex = new Random().Next(count);
            var quote = await _context.Quotes.Skip(randomIndex).FirstOrDefaultAsync();

            if (quote == null)
            {
                return NotFound("No quote found at the random index.");
            }

            return quote;
        }

        [HttpGet("unapproved")]
        public async Task<ActionResult<IEnumerable<Quote>>> GetUnapproved()
        {
            var unapprovedQuotes = await _context.Quotes.Where(q => !q.Approved).ToListAsync();
            if (unapprovedQuotes == null || !unapprovedQuotes.Any())
            {
                return NotFound("No unapproved quotes available.");
            }
            return unapprovedQuotes;
        }


        [HttpGet("category/{category}")]
        public async Task<ActionResult<Quote>> GetRandomQuoteByCategory(string category)
        {
            var quotes = await _context.Quotes.Where(q => q.Category == category).ToListAsync();
            if (quotes == null || !quotes.Any())
            {
                return NotFound($"No quotes found in the category '{category}'.");
            }
            var randomIndex = new Random().Next(quotes.Count);
            return quotes[randomIndex];
        }

        [HttpPost]
        public async Task<ActionResult<Quote>> AddQuoteAsync([FromBody] Quote quote)
        {
            if (quote == null)
            {
                return BadRequest("Quote cannot be null.");
            }
            if(quote.Id == Guid.Empty)
            {
                quote.Id = Guid.NewGuid();
            }
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRandomQuote), new { id = quote.Id }, quote);
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveQuoteAsync(Guid id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound($"Quote with ID {id} not found.");
            }
            quote.Approved = true;
            _context.Entry(quote).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuoteById(Guid id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            return NotFound();
            return Ok(quote);
        }
    }
}
