using System.Threading.Tasks;
using GameData;
using GameLogic.NewLocationCalculators;
using GameMap;
using GeneralUtilities;

namespace GameLogic.Processors
{
    /// <summary>
    /// This class is immutable.
    /// </summary>
    public class MovementProcessor
    {
        public ProcessResponse[] Process(ProcessRequest[] requests, INewLocationCalculator newLocationCalculator)
        {
            ProcessResponse[] response = new ProcessResponse[requests.Length];

            //int cnt = 0;
            //foreach (ProcessRequest request in requests)
            Parallel.ForEach(requests, (request, state, cnt) =>
            {
                var resp = Process(request, newLocationCalculator);
                response[cnt] = resp;
            }
            );

            return response;
        }

        public ProcessResponse Process(ProcessRequest request, INewLocationCalculator newLocationCalculator)
        {
            Point2 newLocation = DetermineNewPosition(request.Location, newLocationCalculator);
            int movementCost = DetermineMovementCost(newLocation);
            bool canMoveIntoCell = CanMoveIntoCell(request.MovementPoints, movementCost);

            if (canMoveIntoCell)
            {
                float newMovementPoints = request.MovementPoints - movementCost;

                return new ProcessResponse(newLocation, newMovementPoints);
            }

            return new ProcessResponse(request.Location, request.MovementPoints);
        }

        private Point2 DetermineNewPosition(Point2 currentLocation, INewLocationCalculator newLocationCalculator)
        {
            return newLocationCalculator.Calculate(currentLocation);
        }

        private int DetermineMovementCost(Point2 location)
        {
            // get terrain type for location
            Cell cell = Globals.Instance.GameWorld.GetCell(location);

            // get movement cost for that terrain type
            TerrainType terrainType = Globals.Instance.TerrainTypes[cell.TerrainTypeId];
            int movementCost = terrainType.MovementCost;

            return movementCost;
        }

        private bool CanMoveIntoCell(float movementPoints, int movementCost)
        {
            return movementPoints >= 0.5f && movementCost >= 0;
        }
    }

    /// <summary>
    /// This struct is immutable.
    /// </summary>
    public struct ProcessRequest
    {
        public Point2 Location { get; }
        public float MovementPoints { get; }

        public ProcessRequest(Point2 location, float movementPoints)
        {
            Location = location;
            MovementPoints = movementPoints;
        }
    }

    /// <summary>
    /// This struct is immutable.
    /// </summary>
    public struct ProcessResponse
    {
        public Point2 NewLocation { get; }
        public float NewMovementPoints { get; }

        public ProcessResponse(Point2 newLocation, float newMovementPoints)
        {
            NewLocation = newLocation;
            NewMovementPoints = newMovementPoints;
        }
    }
}