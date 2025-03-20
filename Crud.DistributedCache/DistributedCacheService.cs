using StackExchange.Redis;
using System.Text.Json;

namespace Crud.Cache
{
    public class DistributedCacheService : IDistributedCacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabaseAsync _database;

        public DistributedCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetRecordAsync<T>(string recordId)
        {
            var record = await _database.StringGetAsync(recordId);

            if (!record.IsNull)
                return JsonSerializer.Deserialize<T>(record.ToString());

            return default(T);
        }

        public async Task SetRecordAsync<T>(string recordId, T data)
        {
            await _database.StringSetAsync(recordId, JsonSerializer.Serialize(data), TimeSpan.FromHours(1));
        }
    }

}
