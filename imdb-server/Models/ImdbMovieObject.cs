using imdb_server.Models;

namespace imdb_server.Controllers.Models
{
    public class ImdbMovieObject
    {
        public List<MovieDetails> Search { get; set; }
        public string? totalResults { get; set; }
        public string? Response { get; set; }
    }
}
