using System.Collections.Generic;
using System.Diagnostics;

namespace GameData
{
    /// <summary>
    /// This struct is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public struct MovementType
    {
        public static readonly MovementType Invalid = new MovementType(-1, "Invalid");

        public int Id { get; }
        public string Name { get; }

        private MovementType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static MovementType Create(int id, string name)
        {
            return new MovementType(id, name);
        }

        private string DebuggerDisplay => $"{{Id={Id},Name={Name}}}";
    }

    /// <summary>
    /// This class is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MovementTypes
    {
        private readonly Dictionary<string, MovementType> _items;

        private MovementTypes(List<MovementType> items)
        {
            _items = new Dictionary<string, MovementType>();
            foreach (MovementType item in items)
            {
                _items.Add(item.Name, item);
            }
        }

        public static MovementTypes Create(List<MovementType> items)
        {
            return new MovementTypes(items);
        }

        public int Count => _items.Count;

        //public MovementType this[int index]
        //{
        //    get
        //    {
        //        if (index < 0 || index > _items.Count - 1)
        //        {
        //            return MovementType.Invalid;
        //        }

        //        return _items[index];
        //    }
        //}

        public MovementType this[string name] => _items[name];

        private string DebuggerDisplay => $"{{Count={_items.Count}}}";
    }
}