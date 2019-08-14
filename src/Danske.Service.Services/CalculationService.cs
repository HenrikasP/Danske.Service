using System.Collections.Generic;
using Danske.Service.Core;

namespace Danske.Service.Services
{
    public class CalculationService : ICalculationService
    {
        private const int DefaultRef = -1;

        public Graph Traverse(int[] input)
        {
            var response = new Graph();
            var nodes = new Node[input.Length];

            nodes[0] = InitializeNodeWithDefaults(input[0], 0);

            for (var i = 1; i < nodes.Length; i++)
            {
                var prevIndex = i - 1;
                var prevNode = InitializeAndRetrieveNode(nodes, prevIndex, input[prevIndex]);

                if (prevNode.Ref == DefaultRef) continue;

                TraverseFromNode(input, prevNode, prevIndex, nodes);
            }

            response.IsTraversable = nodes[nodes.Length - 1] != null && nodes[nodes.Length - 1].Ref != DefaultRef;

            if (response.IsTraversable)
            {
                response.Path = PreparePath(nodes);
            }

            return response;
        }

        private Node InitializeAndRetrieveNode(Node[] nodes, int index, int value)
        {
            return nodes[index] = nodes[index] ?? InitializeNodeWithDefaults(value);
        }

        private Node InitializeNodeWithDefaults(int value, int reference = DefaultRef)
        {
            return new Node
            {
                Hop = 0,
                Value = value,
                Ref = reference
            };
        }

        private void TraverseFromNode(int[] input, Node prevNode, int prevIndex, Node[] nodes)
        {
            for (var i = 1; i <= prevNode.Value; i++)
            {
                var index = prevIndex + i;

                if (index >= nodes.Length) break;

                var node = InitializeAndRetrieveNode(nodes, index, input[index]);

                var canBeReachedFromPrevRef = prevNode.Ref != DefaultRef && nodes[prevNode.Ref].Value + prevNode.Ref >= index;
                if (canBeReachedFromPrevRef)
                {
                    node.Hop = prevNode.Hop;
                    node.Ref = prevNode.Ref;
                }
                else
                {
                    node.Hop ++;
                    node.Ref = prevIndex;
                }
            }
        }

        private static int[] PreparePath(Node[] nodes)
        {
            var path = new List<int>();
            var index = nodes.Length - 1;

            path.Add(index);

            while (index != 0)
            {
                var node = nodes[index];

                path.Add(node.Ref);
                index = node.Ref;
            }

            path.Reverse();

            return path.ToArray();
        }
    }
}
