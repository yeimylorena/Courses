using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers.DocumentModel
{
    public class Mapper : IMapper
    {
        public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Document> items)
        {
            return items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(Document item)
        {
            return new MovieResponse
            {
                MovieName = item["MovieName"],
                Description = item["Description"],
                Actors = item["Actors"].AsListOfString(),
                Ranking = Convert.ToInt32(item["Ranking"]),
                TimeRankend = item["RankedDate"]
            };
        }

        public Document ToDocumentModel(int userId, MovieRankRequest movieRankRequest)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["MovieName"] = movieRankRequest.MovieName,
                ["Description"] = movieRankRequest.Description,
                ["Actors"] = movieRankRequest.Actors,
                ["Ranking"] = movieRankRequest.Ranking,
                ["RankedDateTime"] = DateTime.UtcNow.ToString()
            };
        }

        public Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieUpdateRequest)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["MovieName"] = movieResponse.MovieName,
                ["Description"] = movieResponse.Description,
                ["Actors"] = movieResponse.Actors,
                ["Ranking"] = movieResponse.Ranking,
                ["RankedDateTime"] = DateTime.UtcNow.ToString()
            };
        }
    }
}
