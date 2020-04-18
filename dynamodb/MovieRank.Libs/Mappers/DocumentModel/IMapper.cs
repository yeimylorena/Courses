using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;
namespace MovieRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<MovieResponse> ToMovieContract(ScanResponse response);
        MovieResponse ToMovieContract(Dictionary<string, AttributeValue> item);
        MovieResponse ToMovieContract(GetItemResponse response);
        IEnumerable<MovieResponse> ToMovieContract(QueryResponse response);
        //Document ToDocumentModel(int userId, MovieRankRequest movieRankRequest);
        //Document ToDocumentModel(int userId, Document movieResponse, MovieUpdateRequest movieUpdateRequest);
    }
}
