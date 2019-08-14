using Danske.Service.Contracts;

namespace Danske.Service.Host.Mappers
{
    public interface IGraphMapper : IMapper<Core.Graph, Graph>
    {
    }

    public class GraphMapper : IGraphMapper
    {
        public Graph Map(Core.Graph resource)
        {
            return new Graph
            {
                IsTraversable = resource.IsTraversable,
                Path = resource.Path
            };
        }
    }
}
