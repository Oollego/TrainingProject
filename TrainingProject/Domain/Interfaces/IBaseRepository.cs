namespace TrainingProject.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T vehicle);
        Task DeleteAsync(Guid id);
        Task<T> GetValueAsync(Guid id);
        Task UpdateAsync(T vehicle);
    }
}
