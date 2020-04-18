using MovieRank.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories.ObjectPersistenceModel
{
    public interface IMovieRankRepository
    {
        Task<IEnumerable<MovieDb>> GetAllItems();
        Task<MovieDb> GetMovieAsync(int userId, string movieName);
        Task<IEnumerable<MovieDb>> GetUsersRankedMoviesByMovieTitleAsync(int userId, string movieName);
        Task AddMovie(MovieDb movieDb);
        Task UpdateMovieAsync(MovieDb movieDb);
        Task<IEnumerable<MovieDb>> GetMovieRank(string movieName);
    }
}