using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;

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
        //public List<string> Genres { get; set; }   
        public string Genres { get; set; }
        [Display(Name = "Imdb rating")]
        public string ImdbRating { get; set; }
        public int Votes { get; set; }
        public string AddedToMyMovies { get; set; }
        //public int MyRating { get; set; }
        //public string Note { get; set; }
        //public bool ToBinge { get; set; }
        //public bool Watched { get; set; }


        

        public static Movie FromTsv(string tsvLine)
        {
            /*
            string ConvertStringArrayToStringJoin(string[] array)
            {
                // Use string Join to concatenate the string elements.
                string result = string.Join(".", array);
                return result;
            }*/

            string[] values = tsvLine.Split('\t');
            //List<string> stringlist = new List<string>();
            //stringlist.Add(values[3]);
            

            Movie movie = new Movie();
            movie.IdImdb = values[0];
       
            movie.Title = values[1];
            movie.ReleaseYear = values[2];
            //movie.Genres = (values[3]).Split(new char[] { ','}).ToList();
            movie.Genres = values[3].Replace(",", " ");
            movie.ImdbRating = values[4];
            movie.Votes = Convert.ToInt32(values[5]);
            movie.AddedToMyMovies = "";
            //movie.Note = "-";
            //movie.ToBinge = false;
            //movie.Watched = false;
            //movie.MyRating = 0;
            


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

         
        //public DbSet<ApplicationIdentity> ApplicationIdentities { get; set; }
       
        /*public MovieDBContext() : base("Movies")
        {
            Database.SetInitializer<MovieDBContext>(new CreateDatabaseIfNotExists<MovieDBContext>());
        }
       */


    }

    




}



