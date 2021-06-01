using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(Category category);
        Task<IEnumerable<Category>> ListAllAsync();
        Task UpdateCategoryAsync(Category category);

        Task<Category> FindCategoryByIdAsync(int id);
        Task DeleteCategory(Category category);



    }
}