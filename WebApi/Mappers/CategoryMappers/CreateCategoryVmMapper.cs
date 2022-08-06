using Core.Entities;
using Core.Interfaces.Mappers;
using Core.ViewModels.CategoryViewModels;

namespace WebApi.Mappers.CategoryMappers;

public class CreateCategoryVmMapper : IVmMapper<CreateCategoryViewModel, Category>
{
    public Category Map(CreateCategoryViewModel source)
    {
        var category = new Category()
        {
            Name = source.Name,
            Description = source.Description,
            ParentCategoryId = source.ParentCategoryId
        };
        return category;
    }
}