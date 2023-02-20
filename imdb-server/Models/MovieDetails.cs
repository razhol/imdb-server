namespace imdb_server.Models
{
    public class MovieDetails
    {
        public string Title { get; set; }
        public string imdbRating { get; set; }
        public string Language { get; set; }
        public string Poster { get; set; }
        public string Director { get; set; }
        public string imdbID { get; set; }
        public string Country { get; set; }

        public string Plot { get; set; }
        public string Runtime { get; set; }

        public string Type { get; set; }
    }
}
