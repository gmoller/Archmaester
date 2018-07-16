using System.Collections.Generic;
using System.Diagnostics;

namespace GameData
{
    /// <summary>
    /// This struct is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public struct UnitType
    {
        public static readonly UnitType Invalid = new UnitType(-1, "None", 0, MovementType.Invalid);

        public int Id { get; }
        public string Name { get; }
        public int Moves { get; }
        public MovementType MovementType { get; }

        private UnitType(int id, string name, int moves, MovementType movementType)
        {
            Id = id;
            Name = name;
            Moves = moves;
            MovementType = movementType;
        }

        public static UnitType Create(int id, string name, int moves, MovementType movementType)
        {
            return new UnitType(id, name, moves, movementType);
        }

        private string DebuggerDisplay => $"{{Id={Id},Name={Name}}}";
    }

    /// <summary>
    /// This class is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class UnitTypes
    {
        private readonly Dictionary<int, UnitType> _items;

        private UnitTypes(List<UnitType> items)
        {
            _items = new Dictionary<int, UnitType>();
            foreach (UnitType item in items)
            {
                _items.Add(item.Id, item);
            }
        }

        public static UnitTypes Create(List<UnitType> items)
        {
            return new UnitTypes(items);
        }

        public int Count => _items.Count;

        public UnitType this[int index]
        {
            get
            {
                if (index < 0 || index > _items.Count - 1)
                {
                    return UnitType.Invalid;
                }

                return _items[index];
            }
        }

        private string DebuggerDisplay => $"{{Count={_items.Count}}}";
    }
}