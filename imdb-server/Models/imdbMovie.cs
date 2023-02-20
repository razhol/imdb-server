using imdb_server.Models;

namespace imdb_server.Controllers.Models
{
    public class ImdbMovie
    {
        public List<MoiveItem> Search { get; set; }
        public string? totalResults { get; set; }
        public string? Response { get; set; }
    }
}
