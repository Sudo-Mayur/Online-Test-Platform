namespace Online_Test_Platform.Services
{
    public interface IService<TEntity,in TPK> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TPK id);
    }
}
