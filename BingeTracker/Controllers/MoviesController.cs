using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BingeTracker.Models;
using Microsoft.AspNet.Identity;
using PagedList;


namespace BingeTracker.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
/*
        private List<MyMovie> GetMyMovies()

        {
            List<MyMovie> mymovies = new List<MyMovie>();
            
            return mymovies;


        }
*/
/*
        public ActionResult GetMyMovies()
        {

            ViewBag.MyMovies = GetMyMovies();
            return View();

        }
*/


        // GET: Movies
        //public ActionResult Index()
        //{
        //    return View(db.Movies.ToList());
        //}


        /*   

            //Searching
            public ActionResult Index(string searchString)
            {
                var movies = from m in db.Movies
                             select m;
                if (!string.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(m => m.Title.Contains(searchString) || m.Genres.Contains(searchString));
                }
                return View(movies);
            }

            //Advanced search
            public ActionResult Index2(string Title,string Genres)
            {
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
                return View(movies);
            }

            //Sorting
            public ActionResult Index3(string sortOrder)
            {
                ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
                ViewBag.ReleaseYearSortParm = sortOrder ==  "ReleaseYear" ? "ReleaseYear_desc" : "ReleaseYear";
                ViewBag.ImdbRatingSortParm = sortOrder == "ImdbRating" ? "ImdbRating_desc" : "ImdbRating";
                ViewBag.VotesSortParm = sortOrder == "Votes" ? "Votes_desc" : "Votes";
                ViewBag.MyRatingSortParm = sortOrder == "MyRating" ? "MyRating_desc" : "MyRating";
                var movies = from m in db.Movies
                             select m;

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
                    default:
                        movies = movies.OrderBy(m => m.Title);
                        break;





                }

                return View(movies);
            }

            //Paging
            public ActionResult Index4(int? page)
            {
                var movies = from m in db.Movies
                             orderby m.ID
                             select m;

                int pageSize = 10;
                int pageNumber = (page ?? 1);

                return View(movies.ToPagedList(pageNumber, pageSize));
            }

            //Searching and Paging
            public ActionResult Index5(string currentFilter, string searchString, int? page)
            {
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var movies = from m in db.Movies                
                             select m;
                if (!string.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(m => m.Title.Contains(searchString) || m.Genres.Contains(searchString));
                }



                int pageSize = 10;
                int pageNumber = (page ?? 1);

                return View(movies.OrderBy(m => m.Title).ToPagedList(pageNumber, pageSize));
            }

            //Searching,Sorting and Paging
            public ActionResult Index6(string sortOrder, string currentFilter, string searchString, int? page)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
                ViewBag.ReleaseYearSortParm = sortOrder == "ReleaseYear" ? "ReleaseYear_desc" : "ReleaseYear";
                ViewBag.ImdbRatingSortParm = sortOrder == "ImdbRating" ? "ImdbRating_desc" : "ImdbRating";
                ViewBag.VotesSortParm = sortOrder == "Votes" ? "Votes_desc" : "Votes";
                ViewBag.MyRatingSortParm = sortOrder == "MyRating" ? "MyRating_desc" : "MyRating";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var movies = from m in db.Movies
                             select m;
                if (!string.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(m => m.Title.Contains(searchString) || m.Genres.Contains(searchString));
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
                    default:
                        movies = movies.OrderBy(m => m.Title);
                        break;





                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);

                return View(movies.ToPagedList(pageNumber, pageSize));
            }

            //Advanced Searching, Sorting, Paging





            public ActionResult Index7(string sortOrder, string currentFilter, string currentFilter2, string Title, string Genres, int? page)

            {



            ViewBag.CurrentSort = sortOrder;
                ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
                ViewBag.ReleaseYearSortParm = sortOrder == "ReleaseYear" ? "ReleaseYear_desc" : "ReleaseYear";
                ViewBag.ImdbRatingSortParm = sortOrder == "ImdbRating" ? "ImdbRating_desc" : "ImdbRating";
                ViewBag.VotesSortParm = sortOrder == "Votes" ? "Votes_desc" : "Votes";
                ViewBag.MyRatingSortParm = sortOrder == "MyRating" ? "MyRating_desc" : "MyRating";


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
                    case "MyRating":
                        movies = movies.OrderBy(m => m.MyRating);
                        break;
                    case "MyRating_desc":
                        movies = movies.OrderByDescending(m => m.MyRating);
                        break;
                    default:
                        movies = movies.OrderBy(m => m.Title);
                        break;





                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);

                return View(movies.ToPagedList(pageNumber, pageSize));
            }
    */

        public ActionResult Index(string sortOrder, string currentFilter, string currentFilter2, string Title, string Genres, int? page)

        {



            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
            ViewBag.ReleaseYearSortParm = sortOrder == "ReleaseYear" ? "ReleaseYear_desc" : "ReleaseYear";
            ViewBag.ImdbRatingSortParm = sortOrder == "ImdbRating" ? "ImdbRating_desc" : "ImdbRating";
            ViewBag.VotesSortParm = sortOrder == "Votes" ? "Votes_desc" : "Votes";
            //ViewBag.MyRatingSortParm = sortOrder == "MyRating" ? "MyRating_desc" : "MyRating";


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
                //case "MyRating":
                //    movies = movies.OrderBy(m => m.MyRating);
                //    break;
                //case "MyRating_desc":
                //    movies = movies.OrderByDescending(m => m.MyRating);
                //    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;





            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(movies./*OrderBy(m => m.Title).*/ToPagedList(pageNumber, pageSize));
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
/*
        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Movies/Create
      


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseYear,ImdbRating,Note,Watched")] MyMovie movie)
        {
            if (ModelState.IsValid)
            {
                var movie = new MyMovie();
                db.MyMovies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }
 */     
        // GET: Movies/Edit/5
     /*   public ActionResult Edit(int? id)
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
*/
        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
 //       [HttpPost]
 //       [ValidateAntiForgeryToken]
        public ActionResult AddToMyMovies(/*[Bind(Include = "ID = movie.ID,Genres,IdImdbImdb,Rating,ReleaseYear,Title,Votes,Note,Watched,ToBinge,UserID,MyRating")]*/ int id, string returnUrl)
        {

            
            
                Movie movie = db.Movies.Find(id);
            
                var userid = User.Identity.GetUserId();
                var m = movie.AddedToMyMovies+userid;
                movie.AddedToMyMovies = m;
                        
;
            
               
                        var myMovie = new MyMovie
                        {
                            //ID = movie.ID,
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

            //    if (ModelState.IsValid)
            //    {
            //        db.Entry(movie).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            // return View(movie);
            // return RedirectToAction("Index");
            return Redirect(returnUrl);
        }


        public ActionResult RemoveFromMyMovies(/*[Bind(Include = "ID = movie.ID,Genres,IdImdbImdb,Rating,ReleaseYear,Title,Votes,Note,Watched,ToBinge,UserID,MyRating")]*/ int id, string returnUrl)
        {
            Movie movie = db.Movies.Find(id);           
            var userid = User.Identity.GetUserId();
            if(movie.AddedToMyMovies.Contains(userid))
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
