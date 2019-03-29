using System.ComponentModel.DataAnnotations;

namespace BingeTracker.Models
{
    public class MyMovie
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
        public string MyRating { get; set; }
        [StringLength(60)]
        public string Note { get; set; }
        public string Watched { get; set; }
        public string UserID { get; set; }

    }





}
