using System.Collections.Generic;
using System.Linq;
using GeneralUtilities;

namespace GameLogic
{
    internal static class BreadthFirstSearch
    {
        internal static Point2[] FloodFill(Point2 start, GameWorld gameWorld)
        {
            var frontier = new Queue<Point2>();
            frontier.Enqueue(start);
            var visited = new Dictionary<Point2, bool> { [start] = true };

            while (frontier.Count > 0)
            {
                Point2 current = frontier.Dequeue();

                List<Point2> neighbors = gameWorld.GetCellNeighbors(current);
                foreach (Point2 item in neighbors)
                {
                    if (!visited.ContainsKey(item))
                    {
                        frontier.Enqueue(item);
                        visited[item] = true;
                    }
                }
            }

            Point2[] array = visited.Keys.ToArray();

            return array;
        }

        internal static Dictionary<Point2, Point2> CalculateCameFrom(Point2 start, GameWorld gameWorld)
        {
            var frontier = new Queue<Point2>();
            frontier.Enqueue(start);
            var cameFrom = new Dictionary<Point2, Point2>();
            cameFrom[start] = Point2.Null;

            while (frontier.Count > 0)
            {
                Point2 current = frontier.Dequeue();

                List<Point2> neighbors = gameWorld.GetCellNeighbors(current);
                foreach (Point2 next in neighbors)
                {
                    if (!cameFrom.ContainsKey(next))
                    {
                        frontier.Enqueue(next);
                        cameFrom[next] = current;
                    }
                }
            }

            return cameFrom;
        }

        internal static Point2[] GetPath(Point2 start, Point2 goal, Dictionary<Point2, Point2> cameFrom)
        {
            Point2 current = goal;
            var path = new List<Point2>();
            while (current != start)
            {
                path.Add(current);
                Point2? o = cameFrom[current];
                current = (Point2)o;
            }

            //path.Add(start);
            path.Reverse();

            return path.ToArray();
        }
    }
}