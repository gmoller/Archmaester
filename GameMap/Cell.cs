using System.Diagnostics;

namespace GameMap
{
    /// <summary>
    /// This struct is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public struct Cell
    {
        public static readonly Cell Null = new Cell(-1, -1);

        public int TerrainTypeId { get; }
        public int MineralTypeId { get; }

        private Cell(int terrainTypeId, int mineralTypeId)
        {
            TerrainTypeId = terrainTypeId;
            MineralTypeId = mineralTypeId;
        }

        public static Cell Create(int terrainTypeId)
        {
            return new Cell(terrainTypeId, -1);
        }

        private string DebuggerDisplay => $"{{TerrainTypeId={TerrainTypeId}}}";
    }
}