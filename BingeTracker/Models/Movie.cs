using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace BingeTracker.Models
{

    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public string IdImdb { get; set; }

        public string Title { get; set; }
        [Display(Name = "Release Year")]
        public string ReleaseYear { get; set; }
        public string Genres { get; set; }
        [Display(Name = "Imdb rating")]
        public string ImdbRating { get; set; }
        public int Votes { get; set; }
        public string AddedToMyMovies { get; set; }





        public static Movie FromTsv(string tsvLine)
        {


            string[] values = tsvLine.Split('\t');

            Movie movie = new Movie();
            movie.IdImdb = values[0];

            movie.Title = values[1];
            movie.ReleaseYear = values[2];

            movie.Genres = values[3].Replace(",", " ");
            movie.ImdbRating = values[4];
            movie.Votes = Convert.ToInt32(values[5]);
            movie.AddedToMyMovies = "";




            return movie;
        }

    }
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
    public class MovieDBContext : DbContext
    {

        public MovieDBContext()
        {

            Database.SetInitializer<MovieDBContext>(null);
        }



        public DbSet<Movie> Movies { get; set; }
        public DbSet<MyMovie> MyMovies { get; set; }




    }






}



