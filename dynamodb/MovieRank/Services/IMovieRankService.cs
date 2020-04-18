using MovieRank.Contracts;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public interface IMovieRankService
    {
        Task<IEnumerable<MovieResponse>> GetAllItemsFromDataBase();
        Task<MovieResponse> GetMovieAsync(int userId, string movieName);
        Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
        Task AddMovie(int userId, MovieRankRequest movieRankRequest);
        Task UpdateMovieAsync(int userId, MovieUpdateRequest request);
        Task<MovieRankResponse> GetMovieRank(string movieName);
    }
}