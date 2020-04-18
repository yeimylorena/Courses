using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories.DocumentModel
{
    public interface IMovieRankRepository
    {
        Task<IEnumerable<Document>> GetAllItems();
        Task<Document> GetMovieAsync(int userId, string movieName);
        Task<IEnumerable<Document>> GetUsersRankedMoviesByMovieTitleAsync(int userId, string movieName);
        Task AddMovie(Document documentModel);
        Task UpdateMovieAsync(Document movieDb);
        Task<IEnumerable<Document>> GetMovieRank(string movieName);
    }
}