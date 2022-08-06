using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;
    private readonly ILoggerManager _logger;

    public CategoryService(IRepository<Category> repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    private async Task<Category> GetByIdAsync(int categoryId)
    {
        var category = await _repository.GetFirstOrDefaultAsync(filter: cat => cat.Id == categoryId);
        if (category is null)
        {
            _logger.LogWarn($"There is no category with id {categoryId}");
            throw new NotFoundException($"There is no category with id {categoryId}");
        }

        return category;
    }
    
    public async Task<Category> GetTreeAsync()
    {
        var root = await _repository.GetFirstOrDefaultAsync(
            filter: category => category.Id == 1,
            include: q => 
                q.Include(cat => cat.ChildCategories)
                    .ThenInclude(cat => cat.ChildCategories)
                    .ThenInclude(cat => cat.ChildCategories)
                    .ThenInclude(cat => cat.ChildCategories));
        
        if (root is null)
        {
            _logger.LogWarn("There is no parent category");
            throw new NotFoundException("There is no parent category");
        }
        return root;
    }

    public async Task CreateCategoryAsync(Category newCategory)
    {
        if (newCategory.ParentCategoryId is null or 0)
        {
            _logger.LogWarn("There can be only one root category");
            throw new BadRequestException("There can be only one root category");
        }
        await GetByIdAsync(newCategory.ParentCategoryId!.Value);
        await _repository.InsertAsync(newCategory);
        await _repository.SaveChangesAsync();
        _logger.LogInfo($"Inserted new category {newCategory.Name}");
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var categoryToDelete = await _repository
            .GetFirstOrDefaultAsync(
                cat => cat.Id == categoryId,
                include: prop => prop.Include(cat => cat.ChildCategories));
        if (categoryToDelete is null)
        {
            _logger.LogWarn($"There is no category with id {categoryId}");
            throw new NotFoundException($"There is no category with id {categoryId}");
        }
        foreach (var category in categoryToDelete.ChildCategories)
        {
            category.ParentCategoryId = category.ParentCategoryId;
        }
        _repository.Delete(categoryToDelete);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(int categoryId, string newName, string newDescription)
    {
        var category = await GetByIdAsync(categoryId);
        category.Name = newName;
        category.Description = newDescription;
        await _repository.SaveChangesAsync();
    }
}