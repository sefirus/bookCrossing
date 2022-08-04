using Core.Entities;
using Core.Interfaces.Mappers;
using Core.ViewModels.CategoryViewModels;

namespace WebApi.Mappers.CategoryMappers;

public class ReadCategoryVmMapper : IVmMapper<Category, ReadCategoryViewModel>
{
    public ReadCategoryViewModel Map(Category source)
    {
        var vm = new ReadCategoryViewModel()
        {
            Id = source.Id,
            Description = source.Description,
            Name = source.Name,
            ChildCategories = source.ChildCategories.Select(category => Map(category))
        };
        return vm;
    }
}