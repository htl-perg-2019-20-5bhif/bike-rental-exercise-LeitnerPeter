using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRentalExercise.Data;
using BikeRentalExercise.Model;

namespace BikeRentalExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly BikeDataContext _context;

        public RentalsController(BikeDataContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals.Include(r => r.Bike).Include(r => r.Customer).ToListAsync();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return rental;
        }

        [HttpGet("unpaid")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetUnpaidRentals()
        {
            // TODO: Return: Customer's ID, first and last name, Rental's ID, start end, end date, and total price
            var rentals = _context.Rentals.Include(r => r.Bike).Include(r => r.Customer);
            return await rentals.Where(r => r.Paid == false && r.Total > 0).ToListAsync();
        }

        [HttpPut("{id}/end")]
        public async Task<IActionResult> EndRental(int id)
        {
            var rental = _context.Rentals.Where(r => r.RentalId == id).First();
            if (id != rental.RentalId)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                rental.RentalEnd = DateTime.Now;
                rental.Total = CalculateCosts(rental);
                rental.Paid = false;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // PUT: api/Rentals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, Rental rental)
        {
            if (id != rental.RentalId)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rentals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new { id = rental.RentalId }, rental);
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rental>> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        private double CalculateCosts(Rental rental)
        {
            var time = (rental.RentalEnd - rental.RentalBegin).TotalMinutes;

            var firstHourCosts = rental.Bike.RentalPriceFirstHour;
            if (time <= 15)
            {
                return 0.0;
            }
            if (time <= 60)
            {
                return firstHourCosts;
            }
            var additionalHours = (int)Math.Ceiling((time - 60.0) / 60.0);
            var additionalCosts = additionalHours * rental.Bike.RentalPrice;
            var total = firstHourCosts + additionalCosts;
            return total;
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalId == id);
        }
    }
}
