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

    public CategoryController(
        ICategoryService categoryService,
        IVmMapper<Category, ReadCategoryViewModel> readMapper)
    {
        _categoryService = categoryService;
        _readMapper = readMapper;
    }
    
    //[HttpGet("{id:int:min(1)}")]
    [HttpGet]
    public async Task<ReadCategoryViewModel> GetCategoryById([FromRoute]int id)
    {
        var category = await _categoryService.GetTreeAsync();
        var viewModel = _readMapper.Map(category);
        return viewModel;
    }
}