using System;
using System.Collections.Generic;
using GameData;
using GeneralUtilities;

namespace GameLogic
{
    /// <summary>
    /// A settlement is an immovable game entity that can be controlled by the
    /// player/AI to do things such as build new units and add buildings to
    /// improve the city.
    /// </summary>
    public class Settlement
    {
        private const int MAXIMUM_POPULATION_CAP = 25;

        private readonly RaceType _race;
        private readonly SettlementCitizens _citizens;
        private readonly SettlementBuildings _buildings;

        public string Name { get; }
        public Point2 Location { get; }
        public int Population { get; private set; } // every 1 population is 1,000 residents
        public int SettlementSize => _citizens.Count;
        public int RaceId => _race.Id;
        public string RaceName => _race.Name;
        public int GrowthRate => DetermineGrowthRate();
        public int FoodConsumption => _citizens.Count;
        public int FoodSurplus => DetermineFoodProductionPerTurn() - _citizens.Count;
        public int Production => DetermineProductionPointsPerTurn();

        public int SubsistenceFarmers => _citizens.SubsistenceFarmers;
        public int AdditionalFarmers => _citizens.AdditionalFarmers;
        public int TotalFarmers => _citizens.TotalFarmers;
        public int TotalWorkers => _citizens.TotalWorkers;
        public int TotalRebels => _citizens.TotalRebels;

        public BuildingTypes BuildingThatHaveBeenBuilt => _buildings.BuildingsThatHaveBeenBuilt;
        public BuildingTypes CanCurrentlyBuild => _buildings.CanCurrentlyBuild();
        public BuildingType CurrentlyProducing { get; set; }

        public SettlementType SettlementType
        {
            get
            {
                if (_citizens.Count <= 4)
                    return SettlementType.Hamlet;
                if (_citizens.Count >= 5 && _citizens.Count <= 8)
                    return SettlementType.Village;
                if (_citizens.Count >= 9 && _citizens.Count <= 12)
                    return SettlementType.Town;
                if (_citizens.Count >= 13 && _citizens.Count <= 16)
                    return SettlementType.City;
                if (_citizens.Count >= 17)
                    return SettlementType.Capital;

                throw new Exception("Unknown settlement type.");
            }
        }

        private Settlement(string name, RaceType raceType, Point2 location, int settlementSize)
        {
            Name = name;
            _race = raceType;
            Location = location;
            Population = settlementSize * 1000;
            _citizens = new SettlementCitizens(settlementSize, raceType.FarmingRate);
            _buildings = new SettlementBuildings(this);
            _buildings.IncreaseProduction(Globals.Instance.BuildingTypes["Barracks"], Globals.Instance.BuildingTypes[0].ConstructionCost); // give barracks
        }

        public static Settlement CreateNew(string name, RaceType raceType, Point2 location)
        {
            var settlement = new Settlement(name, raceType, location, 1);

            return settlement;
        }

        public void EndTurn()
        {
            // increase population
            Population += GrowthRate;
            if (Population / 1000 > SettlementSize)
            {
                _citizens.Increase();
            }

            // build stuff
            _buildings.IncreaseProduction(CurrentlyProducing, Production);
        }

        private int DetermineFoodProductionPerTurn()
        {
            float farmingRate = _buildings.BuildingHasBeenBuilt("Animists Guild") ? 3 : _race.FarmingRate;
            float fromFarmers = TotalFarmers * farmingRate;
            int fromForestersGuild = _buildings.BuildingHasBeenBuilt("Foresters Guild") ? 2 : 0;
            float foodProductionPerTurn = fromFarmers + fromForestersGuild;
            //foodProductionPerTurn = IsCityEnchantmentFamineActive ? foodProductionPerTurn / 2 : foodProductionPerTurn;

            int baseFoodLevel = DetermineBaseFoodLevel();
            if (foodProductionPerTurn > baseFoodLevel)
            {
                int excess = ((int)foodProductionPerTurn - baseFoodLevel) / 2;
                foodProductionPerTurn = baseFoodLevel + excess;
            }

            foodProductionPerTurn = _buildings.BuildingHasBeenBuilt("Granary") ? foodProductionPerTurn + 2 : foodProductionPerTurn;
            foodProductionPerTurn = _buildings.BuildingHasBeenBuilt("Farmers Market") ? foodProductionPerTurn + 3 : foodProductionPerTurn;
            foodProductionPerTurn += TerrainHelper.GetMineralFoodModifierFromTerrain(Location);
            //foodProductionPerTurn += NumberOfSharedWildGameTiles;

            return (int)foodProductionPerTurn;
        }

        private int DetermineGrowthRate()
        {
            int maxSettlementSize = DetermineMaximumSettlementSize();
            if (_citizens.Count >= maxSettlementSize) return 0;

            float baseGrowthRate = (maxSettlementSize - _citizens.Count + 1) / 2.0f;
            int baseGrowthRateRoundedUp = (int)Math.Ceiling(baseGrowthRate);

            var adjustedGrowthRate = baseGrowthRateRoundedUp * 10 + _race.GrowthRateModifier;

            //// stream of life (+100%)
            //// housing project (0-125%)
            //// dark rituals (-25%)

            return adjustedGrowthRate;
        }

        private int DetermineMaximumSettlementSize()
        {
            int baseFoodLevel = DetermineBaseFoodLevel();
            ////baseFoodLevel = IsCityEnchantmentFamineActive ? baseFoodLevel / 2 : baseFoodLevel;
            /////baseFoodLevel = SettlementHasGranary ? baseFoodLevel + 2 : baseFoodLevel;
            /////baseFoodLevel = SettlementHasFarmersMarket ? baseFoodLevel + 3 : baseFoodLevel;
            baseFoodLevel += TerrainHelper.GetMineralFoodModifierFromTerrain(Location);
            //baseFoodLevel += NumberOfSharedWildGameTiles;

            return baseFoodLevel > MAXIMUM_POPULATION_CAP ? MAXIMUM_POPULATION_CAP : baseFoodLevel;
        }

        private int DetermineBaseFoodLevel() // used by Surveyor
        {
            // Each city has a base food level of Food it can produce
            int baseFoodLevel = TerrainHelper.GetBaseFoodLevelFromTerrain(Location);

            ////baseFoodLevel = IsCityEnchantmentGaiasBlessingActive ? baseFoodLevel * 1.5f : baseFoodLevel;

            return baseFoodLevel;
        }

        private int DetermineProductionPointsPerTurn()
        {
            float productionPoints = TotalWorkers * _race.WorkerProductionRate + TotalFarmers * _race.FarmerProductionRate;

            // TODO: buildings

            float percentMultiplier = TerrainHelper.GetBaseProductionPointsFromTerrain(Location);
            percentMultiplier = 1 + percentMultiplier / 100;

            float totalProductionPoints = productionPoints * percentMultiplier;

            // TODO: spells

            return (int)totalProductionPoints;
        }
    }
}