using System.Collections.Generic;
using System.Diagnostics;

namespace GameData
{
    /// <summary>
    /// This struct is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public struct MineralType
    {
        public static readonly MineralType Invalid = new MineralType(-1, "None", 0.0f);

        public int Id { get; }
        public string Name { get; }
        public float FoodModifier { get; }

        private MineralType(int id, string name, float foodModifier)
        {
            Id = id;
            Name = name;
            FoodModifier = foodModifier;
        }

        public static MineralType Create(int id, string name, float foodModifier)
        {
            return new MineralType(id, name, foodModifier);
        }

        private string DebuggerDisplay => $"{{Id={Id},Name={Name}}}";
    }

    /// <summary>
    /// This class is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MineralTypes
    {
        private readonly Dictionary<int, MineralType> _items;

        private MineralTypes(List<MineralType> items)
        {
            _items = new Dictionary<int, MineralType>();
            foreach (MineralType item in items)
            {
                _items.Add(item.Id, item);
            }
        }

        public static MineralTypes Create(List<MineralType> items)
        {
            return new MineralTypes(items);
        }

        public int Count => _items.Count;

        public MineralType this[int index]
        {
            get
            {
                if (index < 0 || index > _items.Count - 1)
                {
                    return MineralType.Invalid;
                }

                return _items[index];
            }
        }

        private string DebuggerDisplay => $"{{Count={_items.Count}}}";
    }
}