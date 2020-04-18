using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class Mapper: IMapper
    {
        public IEnumerable<MovieResponse> ToMovieContract(ScanResponse response)
        {
            return response.Items.Select(ToMovieContract);
        }

        public IEnumerable<MovieResponse> ToMovieContract(QueryResponse response)
        {
            return response.Items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(Dictionary<string, AttributeValue> item)
        {
            return new MovieResponse
            {
                MovieName = item["MovieName"].S,
                Description = item["Description"].S,
                Actors = item["Actors"].SS,
                Ranking = Convert.ToInt32(item["Ranking"].N),
                TimeRankend = item["RankedDateTime"].S
            };
        }

        public MovieResponse ToMovieContract(GetItemResponse response)
        {
            return new MovieResponse
            {
                MovieName = response.Item["MovieName"].S,
                Description = response.Item["Description"].S,
                Actors = response.Item["Actors"].SS,
                Ranking = Convert.ToInt32(response.Item["Ranking"].N),
                TimeRankend = response.Item["RankedDateTime"].S
            };
        }

        //public Document ToDocumentModel(int userId, MovieRankRequest movieRankRequest)
        //{
        //    return new Document
        //    {
        //       ["UserId"] = userId,
        //       ["MovieName"] = movieRankRequest.MovieName,
        //       ["Description"] = movieRankRequest.Description,
        //       ["Actors"] = movieRankRequest.Actors,
        //       ["Ranking"] = movieRankRequest.Ranking,
        //       ["RankedDateTime"] = DateTime.UtcNow.ToString()
        //    };
        //}

        //public Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieUpdateRequest)
        //{
        //    return new Document
        //    {
        //        ["UserId"] = userId,
        //        ["MovieName"] = movieResponse.MovieName,
        //        ["Description"] = movieResponse.Description,
        //        ["Actors"] = movieResponse.Actors,
        //        ["Ranking"] = movieResponse.Ranking,
        //        ["RankedDateTime"] = DateTime.UtcNow.ToString()
        //    };
        //}
    }
}
