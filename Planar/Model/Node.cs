using Planar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planar.Model
{
    //CN: For directed Graphs
    public class Node<T>
    {
        public T Value { get; }
        public IEnumerable<Node<T>> Children { get; }
        public IEnumerable<Node<T>> Parents { get; }

        private readonly List<Node<T>> _children = new List<Node<T>>();
        private readonly List<Node<T>> _parents = new List<Node<T>>();

        public Node(T value, IEnumerable<Node<T>> children, IEnumerable<Node<T>> parents)
        {
            Value = value;
            Children = children;
            Parents = parents;
        }
    }

    public enum Strategy
    {
        MinimizeMemory,
        MinimizeBuildTime,
        Balanced
    }

    public class GraphBuildingExtensions
    {

        public static IEnumerable<Node<T>> CreateAcyclicGraph<T,U>(
            IEnumerable<T> elementsToConnect, Func<T,U> identifier, Func<T,IEnumerable<T>> selector, Strategy strategy)
        {
            var valueMap = new Dictionary<U, List<T>>();

            foreach (var element in elementsToConnect)
            {
                valueMap.SafeAddRange(identifier(element), selector(element));
            }

            if (strategy != Strategy.MinimizeBuildTime)
            {
                //CN: Sanity Check
                var identifiers = valueMap.Flatten()
                    .Select(identifier)
                    .Distinct();

                foreach (var item in identifiers)
                {
                    if (!valueMap.ContainsKey(item))
                        throw new InvalidOperationException($"Graph inputs included child nodes that were not in {nameof(elementsToConnect)}");
                }
            }



        }

        public static IEnumerable<Node<T>> CreateAcyclicGraph<T,U>(IEnumerable<T> elementsToConnect) where T : IEquatable<T>
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Node<T>> CreateAcyclicGraph<T,U>(IEnumerable<T> elementsToConnect, Func<T, IEnumerable<U>> selector)
        {
            throw new NotImplementedException();
        }
    }



}
