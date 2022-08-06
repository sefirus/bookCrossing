using Core.Entities;
using Core.Interfaces.Mappers;
using Core.Interfaces.Services;
using Core.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IVmMapper<Category, ReadCategoryViewModel> _readMapper;
    private readonly IVmMapper<CreateCategoryViewModel, Category> _createMapper;

    public CategoryController(
        ICategoryService categoryService,
        IVmMapper<Category, ReadCategoryViewModel> readMapper,
        IVmMapper<CreateCategoryViewModel, Category> createMapper)
    {
        _categoryService = categoryService;
        _readMapper = readMapper;
        _createMapper = createMapper;
    }
    
    [HttpGet]
    public async Task<ReadCategoryViewModel> GetCatalog()
    {
        var category = await _categoryService.GetTreeAsync();
        var viewModel = _readMapper.Map(category);
        return viewModel;
    }    
    
    [HttpDelete("{id:int:min(1)}")]
    public async Task DeleteCategoryById([FromRoute]int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
    }

    [HttpPost]
    public async Task CreateCategory(CreateCategoryViewModel viewModel)
    {
        var category = _createMapper.Map(viewModel);
        await _categoryService.CreateCategoryAsync(category);
    }
}