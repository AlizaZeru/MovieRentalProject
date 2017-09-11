using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentMovieProject_EF_API.Models
{
    public class Rental
    {
        public long Id { get; set; }//primary key
        public long CustomerId { get; set; }//forign key
        public long MovieId { get; set; }//forign key
    }
}