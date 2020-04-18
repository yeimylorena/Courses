using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories.DocumentModel
{
    public class MovieRankRepository: IMovieRankRepository
    {
        private const string TableName = "MovieRank";
        private readonly Table table;
        public MovieRankRepository(IAmazonDynamoDB dynamoDbClient)
        {
            this.table = Table.LoadTable(dynamoDbClient, TableName);
        }

        public async Task<IEnumerable<Document>> GetAllItems()
        {
            var config = new ScanOperationConfig();
            return await table.Scan(config).GetRemainingAsync();
        }

        public async Task<Document> GetMovieAsync(int userId, string movieName)
        {
            //Required PK and SK
            return await table.GetItemAsync(userId, movieName);
        }

        public async Task<IEnumerable<Document>> GetUsersRankedMoviesByMovieTitleAsync(int userId, string movieName)
        {
            var filter = new QueryFilter("UserId", QueryOperator.Equal, userId);
            filter.AddCondition("MovieName", QueryOperator.BeginsWith, movieName);
            return await table.Query(filter).GetRemainingAsync();
        }

        public async Task AddMovie(Document documentModel)
        {
            await table.PutItemAsync(documentModel);
        }

        public async Task UpdateMovieAsync(Document request)
        {
            await table.UpdateItemAsync(request);
        }

        public async Task<IEnumerable<Document>> GetMovieRank(string movieName)
        {
            var filter = new QueryFilter("MovieName", QueryOperator.Equal, movieName);
            var config = new QueryOperationConfig()
            {
                IndexName = "MovieName-index",
                Filter = filter
            };
            return await table.Query(config).GetRemainingAsync();
        }
    }
}
