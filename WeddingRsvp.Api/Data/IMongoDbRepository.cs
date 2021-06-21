using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WeddingRsvp.Api.Data
{
    public interface IMongoDbRepository
    {
        Task<T> AddOneAsync<T>(T item) where T : Document, new();

        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter) where T : Document, new();
         
        Task<T> GetOneAsync<T>(Guid Id) where T : Document, new();

        Task<T> UpdateOneAsync<T>(T item) where T : Document, new();
    }
}
