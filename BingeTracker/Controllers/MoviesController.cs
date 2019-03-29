using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BingeTracker.Models;
using Microsoft.AspNet.Identity;
using PagedList;


namespace BingeTracker.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();



        // GET: Movies


        public ActionResult Index(string sortOrder, string currentFilter, string currentFilter2, string Title, string Genres, int? page)

        {



            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
            ViewBag.ReleaseYearSortParm = sortOrder == "ReleaseYear" ? "ReleaseYear_desc" : "ReleaseYear";
            ViewBag.ImdbRatingSortParm = sortOrder == "ImdbRating" ? "ImdbRating_desc" : "ImdbRating";
            ViewBag.VotesSortParm = sortOrder == "Votes" ? "Votes_desc" : "Votes";



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

            var movies = from m in db.Movies
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

                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;





            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(movies.ToPagedList(pageNumber, pageSize));
        }



        // GET: Movies/Details/5


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public ActionResult AddToMyMovies(int id, string returnUrl)
        {



            Movie movie = db.Movies.Find(id);

            var userid = User.Identity.GetUserId();
            var m = movie.AddedToMyMovies + userid;
            movie.AddedToMyMovies = m;

            ;


            var myMovie = new MyMovie
            {

                Genres = movie.Genres,
                IdImdb = movie.IdImdb,
                ImdbRating = movie.ImdbRating,
                ReleaseYear = movie.ReleaseYear,
                Title = movie.Title,
                Votes = movie.Votes,
                Note = "",
                Watched = "no",
                UserID = User.Identity.GetUserId(),
                MyRating = ""
            };

            db.MyMovies.Add(myMovie);
            db.SaveChanges();


            return Redirect(returnUrl);
        }


        public ActionResult RemoveFromMyMovies(int id, string returnUrl)
        {
            Movie movie = db.Movies.Find(id);
            var userid = User.Identity.GetUserId();
            if (movie.AddedToMyMovies.Contains(userid))
            {
                var m = movie.AddedToMyMovies;
                var mm = m.Replace(userid, "");
                movie.AddedToMyMovies = mm;
            }
            var myMs = from mm in db.MyMovies where mm.IdImdb == movie.IdImdb select mm;
            MyMovie myMovie = (from m in myMs where m.UserID == userid select m).First();
            db.MyMovies.Remove(myMovie);
            db.SaveChanges();
            return Redirect(returnUrl);
        }


        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);

        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
