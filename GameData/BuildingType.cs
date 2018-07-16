using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameData
{
    /// <summary>
    /// This struct is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public struct BuildingType
    {
        public static readonly BuildingType Invalid = new BuildingType(-1, "None", 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, new List<int>(), new List<int>());

        public int Id { get; }
        public string Name { get; }
        public float ConstructionCost { get; }
        public float ConstructedAmount { get; }
        public float UpkeepGold { get; }
        public float UpkeepMana { get; }
        public float FoodProduced { get; }
        public float GrowthRateIncrease { get; }
        public List<int> DependentBuildings { get; } // TODO: don't expose this as a list
        public List<int> Races { get; } // TODO: don't expose this as a list

        public bool HasBeenBuilt => ConstructedAmount >= ConstructionCost;

        private BuildingType(int id, string name, float constructionCost, float constructedAmount, float upkeepGold, float upkeepMana, float foodProduced, float growthRateIncrease, List<int> dependentBuildings, List<int> races)
        {
            Id = id;
            Name = name;
            ConstructionCost = constructionCost;
            ConstructedAmount = constructedAmount;
            UpkeepGold = upkeepGold;
            UpkeepMana = upkeepMana;
            FoodProduced = foodProduced;
            GrowthRateIncrease = growthRateIncrease;
            DependentBuildings = dependentBuildings;
            Races = races;
        }

        public static BuildingType Create(int id, string name, float constructionCost, float constructedAmount, float upkeepGold, float upkeepMana, float foodProduced, float growthRateIncrease, List<int> dependentBuildings, List<int> races)
        {
            return new BuildingType(id, name, constructionCost, constructedAmount, upkeepGold, upkeepMana, foodProduced, growthRateIncrease, dependentBuildings, races);
        }

        public bool CanBeBuiltBy(int raceTypeId)
        {
            return Races.Contains(raceTypeId);
        }

        private string DebuggerDisplay => $"{{Id={Id},Name={Name}}}";
    }

    /// <summary>
    /// This class is immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class BuildingTypes : IEnumerable<BuildingType>
    {
        private readonly Dictionary<int, BuildingType> _items;

        private BuildingTypes(List<BuildingType> items)
        {
            _items = new Dictionary<int, BuildingType>();
            foreach (BuildingType item in items)
            {
                _items.Add(item.Id, item);
            }
        }

        public static BuildingTypes Create(List<BuildingType> items)
        {
            return new BuildingTypes(items);
        }

        public static BuildingTypes Create(IEnumerable<BuildingType> items)
        {
            return new BuildingTypes(items.ToList());
        }

        public int Count => _items.Count;

        public BuildingType this[int index]
        {
            get
            {
                if (index < 0 || index > _items.Count - 1)
                {
                    return BuildingType.Invalid;
                }

                return _items[index];
            }
        }

        public BuildingType this[string name]
        {
            get
            {
                foreach (BuildingType item in this)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }

                return BuildingType.Invalid;
            }
        }

        public IEnumerator<BuildingType> GetEnumerator()
        {
            foreach (KeyValuePair<int, BuildingType> item in _items)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private string DebuggerDisplay => $"{{Count={_items.Count}}}";
    }
}