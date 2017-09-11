using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentMovieProject_EF_API.Models
{
    public class Movie
    {
        public long Id { get; set; }//primary key
        public string Name { get; set; }
        public string Category { get; set; }
    }
}