using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRank.Services;
using MovieRank.Contracts;

namespace MovieRank.Controllers
{
    [Route("movies")]
    public class MovieController : Controller
    {
        private readonly IMovieRankService movieRankService;

        public MovieController(IMovieRankService movieRankService)
        {
            this.movieRankService = movieRankService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDataBase() {
            var result = await movieRankService.GetAllItemsFromDataBase();
            return result;
        }

        [HttpGet]
        [Route("{userId}/{movieName}")]
        public async Task<MovieResponse> GetMovie(int userId, string movieName)
        {
            var result = await movieRankService.GetMovieAsync(userId, movieName);
            return result;
        }

        [HttpGet]
        [Route("user/{userId}/rankedMovies/{movieName}")]
        public async Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName) 
        {
            var result = await movieRankService.GetUsersRankedMoviesByMovieTitle(userId, movieName);
            return result;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> AddMovie(int userId, [FromBody] MovieRankRequest movieRankRequest)
        {
            try
            {
                await movieRankService.AddMovie(userId, movieRankRequest);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
           
            return Ok();
        }

        [HttpPatch]
        [Route("{userId}")]
        public async Task<IActionResult> UpdateMovie(int userId, [FromBody] MovieUpdateRequest request)
        {
            await movieRankService.UpdateMovieAsync(userId, request);
            return Ok();
        }

        [HttpGet]
        [Route("{movieName}/ranking")]
        public async Task<MovieRankResponse> GetMovieRanking(string movieName)
        {
            var result = await movieRankService.GetMovieRank(movieName);
            return result;
        }        
    }
}