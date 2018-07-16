using System;
using System.Collections.Generic;

namespace GeneralUtilities
{
    public class SimpleGraph<T>
    {
        public Dictionary<T, T[]> Edges = new Dictionary<T, T[]>();

        public T[] Neighbors(T id)
        {
            return Edges[id];
        }
    }

    public class Test
    {
        public void Main()
        {
            SimpleGraph<string> g = new SimpleGraph<string>
            {
                Edges = new Dictionary<string, string[]>
                {
                    {"A", new[] {"B"}},
                    {"B", new[] {"A", "C", "D"}},
                    {"C", new[] {"A"}},
                    {"D", new[] {"E", "A"}},
                    {"E", new[] {"B"}}
                }
            };

            Search(g, "A");
        }

        private void Search(SimpleGraph<string> graph, string start)
        {
            var frontier = new Queue<string>();
            frontier.Enqueue(start);

            var visited = new HashSet<string>();
            visited.Add(start);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                Console.WriteLine("Visiting {0}", current);
                foreach (var next in graph.Neighbors(current))
                {
                    if (!visited.Contains(next))
                    {
                        frontier.Enqueue(next);
                        visited.Add(next);
                    }
                }
            }
        }
    }
}