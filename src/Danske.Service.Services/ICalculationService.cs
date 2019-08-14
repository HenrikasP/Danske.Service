using Danske.Service.Core;

namespace Danske.Service.Services
{
    public interface ICalculationService
    {
        Graph Traverse(int[] input);
    }
}