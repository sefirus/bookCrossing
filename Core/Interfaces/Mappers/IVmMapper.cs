namespace Core.Interfaces.Mappers;

public interface IVmMapper<in TSource, out TDestination>
{
    TDestination Map(TSource source);
}