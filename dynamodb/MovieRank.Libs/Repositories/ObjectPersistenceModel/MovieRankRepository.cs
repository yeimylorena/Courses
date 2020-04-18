using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using MovieRank.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories.ObjectPersistenceModel
{
    public class MovieRankRepository : IMovieRankRepository
    {
        private readonly DynamoDBContext context;
        public MovieRankRepository(IAmazonDynamoDB dynamoDbClient)
        {
            this.context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<IEnumerable<MovieDb>> GetAllItems()
        {
            // Scan all items in the db. This is an expensive
            // process in terms of speedn and cost. 
            return await context.ScanAsync<MovieDb>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<MovieDb> GetMovieAsync(int userId, string movieName)
        {
            // Limited to pass partion key and sort 
            return await context.LoadAsync<MovieDb>(userId, movieName);
        }

        public async Task<IEnumerable<MovieDb>> GetUsersRankedMoviesByMovieTitleAsync(int userId, string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("MovieName", Amazon.DynamoDBv2.DocumentModel.ScanOperator.BeginsWith, movieName)
                }
            };
            return await context.QueryAsync<MovieDb>(userId, config).GetRemainingAsync();
        }

        public async Task AddMovie(MovieDb movieDb)
        {
            await context.SaveAsync(movieDb);
        }

        public async Task UpdateMovieAsync(MovieDb request)
        {
            await context.SaveAsync(request);
        }

        public async Task<IEnumerable<MovieDb>> GetMovieRank(string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "MovieName-index"
            };
            return await context.QueryAsync<MovieDb>(movieName, config).GetRemainingAsync();
        }
    }
}
