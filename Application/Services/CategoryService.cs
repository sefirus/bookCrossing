using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CategoryService
{
    private readonly IRepository<Category> _repository;
    private readonly ILoggerManager _logger;

    public CategoryService(IRepository<Category> repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Category> GetTree()
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
    
}