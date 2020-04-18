using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;
namespace MovieRank.Libs.Mappers.DocumentModel
{
    public interface IMapper
    {
        IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Document> items);
        MovieResponse ToMovieContract(Document item);
        Document ToDocumentModel(int userId, MovieRankRequest movieRankRequest);
        Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieUpdateRequest);
    }
}
