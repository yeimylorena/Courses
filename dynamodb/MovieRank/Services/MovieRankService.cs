using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRank.Contracts;
using MovieRank.Libs.Repositories.ObjectPersistenceModel;
using MovieRank.Libs.Mappers.ObjectPersistenceModel;
using System.Linq;
using System;

namespace MovieRank.Services
{
    public class MovieRankService: IMovieRankService
    {
        private readonly IMovieRankRepository movieRankRepository;
        private readonly IMapper map;
        public MovieRankService(IMovieRankRepository movieRankRepository, IMapper map)
        {
            this.movieRankRepository = movieRankRepository;
            this.map = map;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDataBase()
        {
            var response = await movieRankRepository.GetAllItems();
            return map.ToMovieContract(response);
        }

        public async Task<MovieResponse> GetMovieAsync(int userId, string movieName)
        {
            var response = await movieRankRepository.GetMovieAsync(userId,movieName);
            return map.ToMovieContract(response);
        }

        public async Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var response = await movieRankRepository.GetUsersRankedMoviesByMovieTitleAsync(userId, movieName);
            return map.ToMovieContract(response);
        }

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            await movieRankRepository.AddMovie(userId, movieRankRequest);
        }

        public async Task UpdateMovieAsync(int userId, MovieUpdateRequest request)
        {
            await movieRankRepository.UpdateMovieAsync(userId, request);
        }

        public async Task<MovieRankResponse> GetMovieRank(string movieName)
        {
            var response = await movieRankRepository.GetMovieRank(movieName);
            var overallMovieRaking = Math.Round(response.Items.Select(item => Convert.ToInt32(item["Ranking"].N)).Average());
            return new MovieRankResponse
            {
                MovieName = movieName,
                OverallRanking = overallMovieRaking
            };
        }
    }
}
