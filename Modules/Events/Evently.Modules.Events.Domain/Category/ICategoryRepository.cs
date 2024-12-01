namespace Evently.Modules.Events.Domain.Category;
public interface ICategoryRepository
{
    Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Category category);
}
