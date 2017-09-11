using RentMovieProject_EF_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentMovieProject_EF_API.Controllers.api
{
    public class RentalController : ApiController
    {
        ApplicationDbContext m_db = new ApplicationDbContext();

        // /api/Rental
        [HttpGet]
        public IEnumerable<Rental> GetRentals()
        {
            return m_db.Rentals.AsEnumerable();
        }

        [HttpGet]
        // GET /api/Rental/1
        public IHttpActionResult GetRental(long id)
        {
            Rental rental = m_db.Rentals.SingleOrDefault(mv => mv.Id == id);

            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        // simple validation
        bool validationIsOk(long id)
        {
            string strId = id.ToString(); 
            return !string.IsNullOrEmpty(strId);
        }

        // POST /api/Rental
        [HttpPost]
        public IHttpActionResult CreateRental(Rental rent)
        {
            if (!validationIsOk(rent.CustomerId) || !validationIsOk(rent.MovieId))
            {
                return BadRequest();
            }

            m_db.Rentals.Add(rent);
            m_db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rent.Id }, rent);
        }


        // PUT /api/Rental
        [HttpPut]
        public IHttpActionResult UpdateRental(Rental rent)
        {
            if (!validationIsOk(rent.CustomerId) || !validationIsOk(rent.MovieId))
            {
                return BadRequest();
            }

            Rental rental = m_db.Rentals.Find(rent.Id);

            if (rental == null)
            {
                return NotFound();
            }

            rental.CustomerId = rent.MovieId;
            rental.MovieId = rent.CustomerId;
            m_db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE /api/Rental/4 -> delete Movie with id 4
        [HttpDelete]
        public IHttpActionResult DeleteRental(long id)
        {
            Rental rental = m_db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            m_db.Rentals.Remove(rental);
            m_db.SaveChanges();

            return Ok(rental);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
