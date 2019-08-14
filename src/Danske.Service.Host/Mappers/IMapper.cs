namespace Danske.Service.Host.Mappers
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn resource);
    }
}