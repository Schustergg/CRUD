namespace Crud.Cache
{
    public interface IDistributedCacheService
    {
        Task SetRecordAsync<T>(string recordId, T data);
        Task<T> GetRecordAsync<T>(string recordId);

    }
}
