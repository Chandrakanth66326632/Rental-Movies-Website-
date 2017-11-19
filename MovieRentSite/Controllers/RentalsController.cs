using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRentSite.Models;

namespace MovieRentSite.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rentals
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Rentals);
        }

        // GET: Rentals/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        [Authorize]
        public ActionResult MyMovies()
        {
            var currentcustomer = db.Users.FirstOrDefault(user => user.Email == User.Identity.Name);

            var MyRents = db.Rentals.Where(ren => ren.CustomerId == currentcustomer.Id);

            var MyMovies = from mov in db.Movies
                      where MyRents.ToList().Any(my => mov.Id == my.MovieId)
                      select mov;

            return View(MyMovies);
        }

        [Authorize]
        public ActionResult RentPage()
        {
            var AvailableMovies = db.Movies.Where(mov => db.Rentals.ToList().Any(ren => ren.MovieId != mov.Id));

            return View(AvailableMovies.ToList());
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Create(Rental rental)
        {
           var currentcustomer =  db.Users.FirstOrDefault(user => user.Email == User.Identity.Name);
            rental.CustomerId = currentcustomer.Id;
            db.Rentals.Add(rental);
            db.SaveChanges();
            return RedirectToAction("MyMovies");
        }
        [Authorize]
        public ActionResult Delete(int? id)
        {
            var currentcustomer = db.Users.FirstOrDefault(user => user.Email == User.Identity.Name);

            if (currentcustomer == null)
            {
                TempData["Error"] = "Authorazation Problem";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.FirstOrDefault( ren => ren.MovieId == id && ren.CustomerId == currentcustomer.Id);
            if (rental == null)
            {
                TempData["Error"] = "This Movie Is Not rented";

                return HttpNotFound();
            }
            db.Rentals.Remove(rental);
            db.SaveChanges();
            TempData["Error"] = "Delete was Successful";

            return RedirectToAction("MyMovies");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
