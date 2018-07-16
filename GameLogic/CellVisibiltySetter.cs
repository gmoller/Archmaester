using GeneralUtilities;

namespace GameLogic
{
    internal static class CellVisibilitySetter
    {
        internal static void SetCellVisibility(Point2 location, GameWorld gameWorld)
        {
            gameWorld.SetCellVisible(Point2.Create(location.X - 1, location.Y + 1)); // northwest
            gameWorld.SetCellVisible(Point2.Create(location.X, location.Y + 1)); // north
            gameWorld.SetCellVisible(Point2.Create(location.X + 1, location.Y + 1)); // northeast
            gameWorld.SetCellVisible(Point2.Create(location.X - 1, location.Y)); // west
            gameWorld.SetCellVisible(location);
            gameWorld.SetCellVisible(Point2.Create(location.X + 1, location.Y)); // east
            gameWorld.SetCellVisible(Point2.Create(location.X - 1, location.Y - 1)); // southwest
            gameWorld.SetCellVisible(Point2.Create(location.X, location.Y - 1)); // south
            gameWorld.SetCellVisible(Point2.Create(location.X + 1, location.Y - 1)); // southeast
        }
    }
}