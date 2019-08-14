using System.ComponentModel.DataAnnotations;

namespace Danske.Service.Contracts
{
    public class Graph
    {
        [Required]
        public bool IsTraversable { get; set; }
        public int[] Path { get; set; }
    }
}
