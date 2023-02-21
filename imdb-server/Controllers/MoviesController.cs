using Microsoft.AspNetCore.Mvc;
using imdb_server.Controllers.Models;
using Newtonsoft.Json;
using imdb_server.Models;
using Newtonsoft.Json.Linq;

namespace imdb_server.Controllers
{
    [ApiController]
    [Route("/[Controller]")]
    public class MoviesController : Controller
    {
        static HttpClient client = new HttpClient();

        public MoviesController(){}
        [HttpGet]
        [Route("/[Controller]/getById")]
        public async Task<IActionResult> SerachMovieById(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    throw new ArgumentException("Cannot be null or be empty");
                }
                string url = $"https://www.omdbapi.com/?i={Id}&apikey=9b75e4d";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content?.ReadAsStringAsync();
                    var ObjectError = JsonConvert.DeserializeObject<ErrorImdb>(jsonString);
                    if (!string.IsNullOrEmpty(ObjectError.Error))
                    {
                        throw new ArgumentException(ObjectError.Error);
                    }
                    var res = JsonConvert.DeserializeObject<MovieDetails>(jsonString);
                    return Ok(res);
                }
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchMoviesByStr(string searchStr, int pageNumber)
        {
            try
            {
                string url = $"https://www.omdbapi.com/?s={searchStr}&page={pageNumber}&apikey=9b75e4d";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    if (string.IsNullOrEmpty(searchStr))
                    {
                        throw new ArgumentException("Cannot be null or be empty");
                    }
                    var jsonString = await response.Content?.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ImdbMovieObject>(jsonString);
                    var IdList = res.Search.Select(x => x.imdbID);
                    List<MovieDetails> MovieList = new List<MovieDetails>();
                    foreach (var id in IdList)
                    {
                        string url2 = $"https://www.omdbapi.com/?i={id}&apikey=9b75e4d";
                        HttpResponseMessage responseById = await client.GetAsync(url2);
                        if (!responseById.IsSuccessStatusCode)
                        {
                            return StatusCode(500);
                        }
                        var json = await responseById.Content?.ReadAsStringAsync();
                        var resMovies = JsonConvert.DeserializeObject<MovieDetails>(json);
                        MovieList.Add(resMovies);
                    }
                    return Ok(MovieList);
                }
                return StatusCode(500);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
