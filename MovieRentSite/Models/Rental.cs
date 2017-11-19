using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentSite.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public string CustomerId { get; set;}
        public int MovieId { get; set; }
    }
}