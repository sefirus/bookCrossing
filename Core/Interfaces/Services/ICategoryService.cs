using Core.Entities;

namespace Core.Interfaces.Services;

public interface ICategoryService
{
    Task<Category> GetTreeAsync();
    Task CreateCategoryAsync(Category newCategory);
    Task DeleteCategoryAsync(int categoryId);
    Task RenameCategoryAsync(int categoryId, string newName);
}