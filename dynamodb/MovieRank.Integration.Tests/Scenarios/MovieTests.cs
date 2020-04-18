using MovieRank.Contracts;
using MovieRank.Integration.Tests.Setup;
using MovieRank.Libs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieRank.Integration.Tests.Scenarios
{
    [Collection("api")]
    public class MovieTests
    {
        private readonly TestContext sut;
        public MovieTests(TestContext sut)
        {
            this.sut = sut;
        }

        [Fact]
        public async Task AddMovieRankDataReturnsOkStatus()
        {
            const int userId = 1;

            var response = await AddMovieRankData(userId);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetAllItemsFromDataReturnsNotNullMovieResponse()
        {
            const int userId = 2;

            await AddMovieRankData(userId);

            var response = await sut.Client.GetAsync("movies");

            MovieResponse[] result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<MovieResponse[]>(await content);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMovieReturnsExpectedMovieName()
        {
            const int userId = 1;
            const string movieName = "Test-GetMovieBack";

            await AddMovieRankData(userId, movieName);
            var response = await sut.Client.GetAsync($"movies/{userId}/{movieName}");
            MovieResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<MovieResponse>(await content);
            }
            Assert.Equal(movieName, result.MovieName);
        }

       
        
        [Fact]
        public async Task UpdateMovieReturnUpdatedMovieRankValue()
        {
            const int userId = 4;
            const string movieName = "Test-UpdateMovie";
            const int ranking = 10;
            await AddMovieRankData(userId, movieName);

            var updateMovie = new MovieUpdateRequest
            {
                MovieName = movieName,
                Ranking = ranking
            };
            var json = JsonConvert.SerializeObject(updateMovie);
            var stringContent = new StringContent(json, Encoding.UTF8, "aplication/json");

            await sut.Client.PatchAsync($"movies/{userId}", stringContent);

            var response = await sut.Client.GetAsync($"movies/{userId}/{movieName}");

            MovieResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<MovieResponse>(await content);
            }
            Assert.Equal(ranking, result.Ranking);
        }

        [Fact]
        public async Task GetMoviesRankingReturnsOverallMovieRanking()
        {
            const int userId = 5;
            const string movieName = "Test-GetMovieOverallRanking";

            await AddMovieRankData(userId, movieName);

            var response = await sut.Client.GetAsync($"movies/{movieName}/ranking");

            MovieRankResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<MovieRankResponse>(await content);
            }
            Assert.NotNull(result);
        }

        private async Task<HttpResponseMessage> AddMovieRankData(int testUserId, string movieName = "Test-MovieName")
        {
            var movieDbData = new MovieDb
            {
                UserId = testUserId,
                MovieName = movieName,
                Description = "test-Description",
                Actors = new List<string>
                {
                    "testUser1",
                    "testUser2"
                },
                RankedDateTime = "5/10/2018 6:17:17 PM",
                Ranking = 4
            };
            var json = JsonConvert.SerializeObject(movieDbData);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                return await sut.Client.PostAsync($"movies/{testUserId}", stringContent);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
