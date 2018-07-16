using System.Collections.Generic;
using System.Linq;
using GameData;

namespace GameLogic
{
    public class SettlementBuildings
    {
        private readonly Settlement _settlement;
        private BuildingTypes _allBuildings;

        public BuildingTypes BuildingsThatHaveBeenBuilt => BuildingTypes.Create(from building in _allBuildings where building.HasBeenBuilt select building);

        public SettlementBuildings(Settlement settlement)
        {
            _settlement = settlement;
            _allBuildings = Globals.Instance.BuildingTypes;
        }

        public BuildingTypes CanCurrentlyBuild()
        {
            List<BuildingType> canCurrentlyBuild = new List<BuildingType>();

            foreach (BuildingType building in _allBuildings)
            {
                if (building.HasBeenBuilt) continue; // already built - skip

                if (!building.CanBeBuiltBy(_settlement.RaceId)) continue; // race cannot build this - skip

                bool hasAllDependentBuildings = true;
                foreach (int item in building.DependentBuildings)
                {
                    hasAllDependentBuildings = BuildingHasBeenBuilt(item);
                }

                if (hasAllDependentBuildings)
                {
                    canCurrentlyBuild.Add(building);
                }
            }

            return BuildingTypes.Create(canCurrentlyBuild);
        }

        internal void IncreaseProduction(BuildingType currentlyProducing, float production)
        {
            List<BuildingType> list = new List<BuildingType>();
            foreach (BuildingType item in _allBuildings)
            {
                if (item.Id == currentlyProducing.Id)
                {
                    list.Add(BuildingType.Create(item.Id, item.Name, item.ConstructionCost, item.ConstructedAmount + production, item.UpkeepGold, item.UpkeepMana, item.FoodProduced, item.GrowthRateIncrease, item.DependentBuildings, item.Races));
                }
                else
                {
                    list.Add(item);
                }
            }

            _allBuildings = BuildingTypes.Create(list);
        }

        internal bool BuildingHasBeenBuilt(int buildingId)
        {
            return _allBuildings[buildingId].HasBeenBuilt;
        }

        internal bool BuildingHasBeenBuilt(string buildingName)
        {
            return _allBuildings[buildingName].HasBeenBuilt;
        }
    }
}