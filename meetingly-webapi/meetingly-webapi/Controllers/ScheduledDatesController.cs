using meetingly_webapi.Data;
using meetingly_webapi.Models;
using meetingly_webapi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace meetingly_webapi.Controllers
{
    [ApiController]
    [Route("api/scheduledDates")]
    public class ScheduledDatesController : ControllerBase
    {

        private readonly ILogger<ScheduledDatesController> _logger;
        private readonly MeetinglyDbContext _context;

        public ScheduledDatesController(ILogger<ScheduledDatesController> logger, MeetinglyDbContext context)
        {
            _logger = logger;
            _context = context;

            //_context.User.AddRangeAsync(
            //    new User { Name = "John Doe" },
            //    new User { Name = "Jane Doe" },
            //    new User { Name = "John Smith" });

            //_context.SaveChanges();
        }

        // GET: api/ScheduledDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduledDate>>> GetScheduledDates()
        {
            return await _context.ScheduledDates.ToListAsync();
        }

        // GET: api/ScheduledDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduledDate>> GetScheduledDate(int id)
        {
            var scheduledDate = await _context.ScheduledDates.FindAsync(id);

            if (scheduledDate == null)
            {
                return NotFound();
            }

            return scheduledDate;
        }

        // POST: api/ScheduledDates
        [HttpPost]
        public async Task<ActionResult<ScheduledDate>> AddScheduledDate(ScheduledDateDto request)
        {
            var scheduledDate = new ScheduledDate { Topic = request.Topic, Description = request.Description, StartDate = request.StartDate, EndDate = request.EndDate, AvailabilityType = request.AvailabilityType, EventType = request.EventType, Status = request.Status, Source = request.Source, UserId = request.UserId };
            _context.ScheduledDates.Add(scheduledDate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetScheduledDate), new { id = scheduledDate.Id }, scheduledDate);
        }

        // PUT: api/ScheduledDates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScheduledDate(int id, ScheduledDateDto request)
        {
            var scheduledDate = await _context.ScheduledDates.FindAsync(id);
            if (scheduledDate == null)
            {
                return NotFound();
            }

            scheduledDate.StartDate = request.StartDate;
            scheduledDate.EndDate = request.EndDate;
            scheduledDate.AvailabilityType = request.AvailabilityType;
            scheduledDate.EventType = request.EventType;
            scheduledDate.Status = request.Status;
            scheduledDate.Source = request.Source;
            scheduledDate.Topic = request.Topic;
            scheduledDate.Description = request.Description;
            scheduledDate.UserId = request.UserId;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduledDateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(scheduledDate);
        }

        // DELETE: api/ScheduledDates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheduledDate(int id)
        {
            var scheduledDate = await _context.ScheduledDates.FindAsync(id);
            if (scheduledDate == null)
            {
                return NotFound();
            }

            _context.ScheduledDates.Remove(scheduledDate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduledDateExists(int id)
        {
            return _context.ScheduledDates.Any(e => e.Id == id);
        }
    }
}