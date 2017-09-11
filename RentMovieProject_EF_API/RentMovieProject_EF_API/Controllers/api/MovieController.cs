using RentMovieProject_EF_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentMovieProject_EF_API.Controllers.api
{
    public class MovieController : ApiController
    {
        ApplicationDbContext m_db = new ApplicationDbContext();

        // /api/Movies
        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return m_db.Movies.AsEnumerable();
        }

        [HttpGet]
        // GET /api/Movies/1
        public IHttpActionResult GetMovie(long id)
        {
            Movie movie = m_db.Movies.SingleOrDefault(mv => mv.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // simple validation
        bool validationIsOk(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(Movie mv)
        {
            if (!validationIsOk(mv.Name) || !validationIsOk(mv.Category))
            {
                return BadRequest();
            }

            m_db.Movies.Add(mv);
            m_db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mv.Id }, mv);
        }


        // PUT /api/movies
        [HttpPut]
        public IHttpActionResult UpdateMovie(Movie mv)
        {
            if (!validationIsOk(mv.Name) || !validationIsOk(mv.Category))
            {
                return BadRequest();
            }

            Movie movie = m_db.Movies.Find(mv.Id);

            if (movie == null)
            {
                return NotFound();
            }

            movie.Name = mv.Name;
            movie.Category = mv.Category;
            m_db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE /api/Movies/4 -> delete Movie with id 4
        [HttpDelete]
        public IHttpActionResult DeleteMovie(long id)
        {
            Movie movie = m_db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            m_db.Movies.Remove(movie);
            m_db.SaveChanges();

            return Ok(movie);
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

