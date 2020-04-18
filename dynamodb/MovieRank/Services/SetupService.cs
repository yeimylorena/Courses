using MovieRank.Libs.Repositories;
using MovieRank.Libs.Repositories.ObjectPersistenceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public class SetupService: ISetupService
    {
        private readonly IMovieRankRepository movieRankRepository;

        public SetupService(IMovieRankRepository movieRankRepository)
        {
            this.movieRankRepository = movieRankRepository;
        }

        public async Task CreateDynamoDbTable(string dynamoDbTableName)
        {
            //await movieRankRepository.CreateDynamoDBTable(dynamoDbTableName);
        }

        public async Task DeleteDynamoDbTableAsync(string dynamoDbTableName)
        {
            //await movieRankRepository.DeleteDynamoDbTable(dynamoDbTableName);
        }
    }
}
