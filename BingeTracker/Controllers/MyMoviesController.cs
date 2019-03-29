using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BingeTracker.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BingeTracker.Controllers
{
    public class MyMoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();



        // GET: MyMovies
        public ActionResult Index(string sortOrder, string currentFilter, string currentFilter2, string Title, string Genres, int? page)

        {



            var ratingList = new List<SelectListItem>();
            double gain = 0.1;
            double txt = 0.0;
            for (var i = 0; i < 101; i++)
            {
                txt = i * gain;
                ratingList.Add(new SelectListItem { Text = txt.ToString(), Value = txt.ToString() });
            }
            ViewBag.myRating = ratingList;




            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
            ViewBag.ReleaseYearSortParm = sortOrder == "ReleaseYear" ? "ReleaseYear_desc" : "ReleaseYear";
            ViewBag.ImdbRatingSortParm = sortOrder == "ImdbRating" ? "ImdbRating_desc" : "ImdbRating";
            ViewBag.VotesSortParm = sortOrder == "Votes" ? "Votes_desc" : "Votes";
            ViewBag.MyRatingSortParm = sortOrder == "MyRating" ? "MyRating_desc" : "MyRating";
            ViewBag.WatchedSortParm = sortOrder == "Watched" ? "Watched_desc" : "Watched";



            if (Title != null)
            {
                page = 1;
            }
            else
            {
                Title = currentFilter;
            }

            ViewBag.CurrentFilter = Title;

            if (Genres != null)
            {
                page = 1;
            }
            else
            {
                Genres = currentFilter2;
            }

            ViewBag.CurrentFilter2 = Genres;

            var userid = User.Identity.GetUserId();

            var movies = from m in db.MyMovies
                         where m.UserID == userid
                         select m;
            if (!string.IsNullOrEmpty(Title))
            {
                movies = movies.Where(m => m.Title.Contains(Title));
            }
            if (!string.IsNullOrEmpty(Genres))
            {
                movies = movies.Where(m => m.Genres.Contains(Genres));
            }



            switch (sortOrder)
            {

                case "Title":
                    movies = movies.OrderBy(m => m.Title);
                    break;
                case "Title_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "ReleaseYear":
                    movies = movies.OrderBy(m => m.ReleaseYear);
                    break;
                case "ReleaseYear_desc":
                    movies = movies.OrderByDescending(m => m.ReleaseYear);
                    break;
                case "ImdbRating":
                    movies = movies.OrderBy(m => m.ImdbRating);
                    break;
                case "ImdbRating_desc":
                    movies = movies.OrderByDescending(m => m.ImdbRating);
                    break;
                case "Votes":
                    movies = movies.OrderBy(m => m.Votes);
                    break;
                case "Votes_desc":
                    movies = movies.OrderByDescending(m => m.Votes);
                    break;
                case "MyRating":
                    movies = movies.OrderBy(m => m.MyRating);
                    break;
                case "MyRating_desc":
                    movies = movies.OrderByDescending(m => m.MyRating);
                    break;
                case "Watched":
                    movies = movies.OrderBy(m => m.Watched);
                    break;
                case "Watched_desc":
                    movies = movies.OrderByDescending(m => m.Watched);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;





            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(movies.ToPagedList(pageNumber, pageSize));
        }



        public ActionResult Remove(int id, string idimdb, string returnUrl)
        {

            MyMovie myMovie = db.MyMovies.Find(id);
            string ii = idimdb;
            Movie movie = db.Movies.Where(i => i.IdImdb == ii).FirstOrDefault();

            var userid = User.Identity.GetUserId();
            var m = movie.AddedToMyMovies;
            var mm = m.Replace(userid, "");
            movie.AddedToMyMovies = mm;
            db.MyMovies.Remove(myMovie);
            db.SaveChanges();
            return Redirect(returnUrl);

        }

        [HttpPost]
        public ActionResult ChangeMyRating(string myRating, int id, string returnUrl)
        {

            MyMovie myMovie = db.MyMovies.Find(id);
            myMovie.MyRating = myRating;
            db.SaveChanges();
            return Redirect(returnUrl);

        }



        [HttpPost]
        public ActionResult ChangeMyNote(string myNote, int id, string returnUrl)
        {

            MyMovie myMovie = db.MyMovies.Find(id);
            myMovie.Note = myNote;
            db.SaveChanges();
            return Redirect(returnUrl);

        }



        public ActionResult ChangeWatched(int id, string returnUrl)
        {

            MyMovie myMovie = db.MyMovies.Find(id);
            if (myMovie.Watched == "yes") { myMovie.Watched = "no"; } else if (myMovie.Watched == "no") { myMovie.Watched = "yes"; }


            db.SaveChanges();
            return Redirect(returnUrl);

        }






        // GET: MyMovies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IdImdb,Title,ReleaseYear,Genres,ImdbRating,Votes,MyRating,Note,ToBinge,Watched,UserID")] MyMovie myMovie)
        {
            if (ModelState.IsValid)
            {
                db.MyMovies.Add(myMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myMovie);
        }

        // GET: MyMovies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyMovie myMovie = db.MyMovies.Find(id);
            if (myMovie == null)
            {
                return HttpNotFound();
            }
            return View(myMovie);
        }

        // POST: MyMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IdImdb,Title,ReleaseYear,Genres,ImdbRating,Votes,MyRating,Note,ToBinge,Watched,UserID")] MyMovie myMovie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myMovie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myMovie);
        }

        // GET: MyMovies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyMovie myMovie = db.MyMovies.Find(id);
            if (myMovie == null)
            {
                return HttpNotFound();
            }
            return View(myMovie);
        }

        // POST: MyMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyMovie myMovie = db.MyMovies.Find(id);
            db.MyMovies.Remove(myMovie);
            db.SaveChanges();
            return RedirectToAction("Index");
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

