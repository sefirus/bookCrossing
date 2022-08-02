using Core.Interfaces.Mappers;
using Core.Pagination;
using Core.ViewModels;

namespace WebApi.Mappers;

public class GenericPagedMapper<TSource, TDestination> : IPagedVmMapper<TSource, TDestination>
{
    private readonly IEnumerableVmMapper<TSource, TDestination> _mapper;

    public GenericPagedMapper(IEnumerableVmMapper<TSource, TDestination> mapper)
    {
        _mapper = mapper;
    }
    public PagedViewModel<TDestination> Map(PagedList<TSource> source)
    {
        var mappedEntities = _mapper.Map(source.Entities);
        var viewModel = new PagedViewModel<TDestination>()
        {
            CurrentPage = source.CurrentPage,
            Entities = mappedEntities,
            HasNext = source.HasNext,
            HasPrevious = source.HasPrevious,
            PageSize = source.PageSize,
            Total = source.TotalCount,
            TotalPages = source.TotalPages
        };
        return viewModel;
    }
}