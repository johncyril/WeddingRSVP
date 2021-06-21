using MongoDB.Driver;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WeddingRsvp.Api.Data
{
    public class MongoDbRepository : BaseMongoRepository, IMongoDbRepository
    {
        public MongoDbRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        public MongoDbRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        public MongoDbRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        public async Task<T> AddOneAsync<T>(T item)
            where T : MongoDbGenericRepository.Models.Document, new()
        {
            base.FormatDocument<T>(item);
            await base.AddOneAsync<T>(item, CancellationToken.None);

            return await base.GetByIdAsync<T>(item.Id);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter)
              where T : MongoDbGenericRepository.Models.Document, new()
        {
            return await base.GetAllAsync<T>(filter);
        }

        public async Task<T> GetOneAsync<T>(Guid Id)
              where T : MongoDbGenericRepository.Models.Document, new()
        {
            return await base.GetByIdAsync<T>(Id);
        }

        public new async Task<T> UpdateOneAsync<T>(T item)
              where T : MongoDbGenericRepository.Models.Document, new()
        {
            var updated = await base.UpdateOneAsync<T>(item);
            if (!updated)
            {
                throw new Exception($"Failed to update record with type:{nameof(T)} and Id:{item.Id} in mongo Db");
            }

            return await GetByIdAsync<T>(item.Id);
        }
    }
}
