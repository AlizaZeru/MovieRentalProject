using RentMovieProject_EF_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentMovieProject_EF_API.Controllers.api
{
    public class CustomerController : ApiController
    {
        ApplicationDbContext m_db = new ApplicationDbContext();

        // /api/Customer
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return m_db.Customers.AsEnumerable();
        }

        [HttpGet]
        // GET /api/Customer/1
        public IHttpActionResult GetCustomer(long id)
        {
            Customer customer = m_db.Customers.SingleOrDefault(cus => cus.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // simple validation
        bool validationIsOk(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer cus)
        {
            string age = cus.Age.ToString();

            if (!validationIsOk(cus.Name) || !validationIsOk(cus.Subscription) ||
                !validationIsOk(age))
            {
                return BadRequest();
            }

            m_db.Customers.Add(cus);
            m_db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cus.Id }, cus);
        }



        // PUT /api/Customer
        [HttpPut]
        public IHttpActionResult UpdateCustomer(Customer cus)
        {
            string age = cus.Age.ToString();

            if (!validationIsOk(cus.Name) || !validationIsOk(cus.Subscription) ||
                !validationIsOk(age))
            {
                return BadRequest();
            }

            Customer customer = m_db.Customers.Find(cus.Id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = cus.Name;
            customer.Age = cus.Age;
            customer.Subscription = cus.Subscription;
            m_db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE /api/customer/4 -> delete customer with id 4
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(long id)
        {
            Customer customer = m_db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            m_db.Customers.Remove(customer);
            m_db.SaveChanges();

            return Ok(customer);
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

