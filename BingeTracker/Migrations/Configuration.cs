namespace BingeTracker.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;
    using BingeTracker.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BingeTracker.Models.MovieDBContext>
    {

        private string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
                return HostingEnvironment.MapPath(seedFile);

            var localPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var directoryName = Path.GetDirectoryName(localPath);
            var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }


        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BingeTracker.Models.MovieDBContext";
        }



        protected override void Seed(BingeTracker.Models.MovieDBContext context)
        {


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            

            List<Movie> listMovies = File.ReadAllLines(MapPath("~/App_Data/imdb.tsv")) // reads all lines into a string array
                    .Skip(1) // skip header line
                    .Select(f => Movie.FromTsv(f)) // uses Linq to select each line and create a new Cae instance using the FromTsv method.
                    .ToList(); // converts to type List

            listMovies.ForEach(s => context.Movies.Add(s));
            context.SaveChanges();
        }
    }
}
