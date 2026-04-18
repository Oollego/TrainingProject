namespace TrainingProject.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T vehicle, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
        Task<T> GetValueAsync(Guid id, CancellationToken ct);
        Task UpdateAsync(T vehicle, CancellationToken ct);
    }
}
