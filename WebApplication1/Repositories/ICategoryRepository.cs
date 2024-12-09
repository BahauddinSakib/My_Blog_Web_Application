using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface ICategoryRepository
    {
       Task<Category>CreateAsync(Category category);
       Task<IEnumerable<Category>> GetAllAsync();
    }
}
