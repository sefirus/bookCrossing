using Core.Pagination;
using Core.ViewModels;

namespace Core.Interfaces.Mappers;

public interface IPagedVmMapper<TSource, TDestination>
{
    PagedViewModel<TDestination> Map(PagedList<TSource> source);
}