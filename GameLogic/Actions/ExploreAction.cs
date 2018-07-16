using System.Collections.Generic;
using GeneralUtilities;

namespace GameLogic.Actions
{
    public class ExploreAction : IAct
    {
        public Unit Execute(Unit unit, object parameters)
        {
            // find closest non-visible cell
            Dictionary<Point2, Point2> cameFrom = BreadthFirstSearch.CalculateCameFrom(unit.Location, Globals.Instance.GameWorld);
            Point2 closest = FindClosestNonVisibleCell(cameFrom);

            if (closest != Point2.Null)
            {
                Point2[] path = BreadthFirstSearch.GetPath(unit.Location, closest, cameFrom);

                // move towards there
                if (path.Length > 0)
                {
                    //return path[0];
                }
            }

            return unit;
        }

        private Point2 FindClosestNonVisibleCell(Dictionary<Point2, Point2> cameFrom)
        {
            foreach (Point2 item in cameFrom.Keys)
            {
                if (!Globals.Instance.GameWorld.IsCellVisible(item))
                {
                    // the location to move towards
                    return item;
                }
            }

            return Point2.Null;
        }
    }
}