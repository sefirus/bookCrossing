namespace Core.Interfaces.Mappers;

public interface IEnumerableVmMapper<in TSource, out TDestination>
{
    IEnumerable<TDestination> Map(IEnumerable<TSource> source);
}