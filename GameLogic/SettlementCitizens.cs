using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public class SettlementCitizens
    {
        // TODO: de-hardcode the citizen types to allow new citizen types to be created through mod-ing
        private readonly List<CitizenType> _citizens;

        public int Count => _citizens.Count;
        public int SubsistenceFarmers => _citizens.Count(item => item == CitizenType.SubsistenceFarmer);
        public int AdditionalFarmers => _citizens.Count(item => item == CitizenType.AdditionalFarmer);
        public int TotalFarmers => _citizens.Count(item => item == CitizenType.SubsistenceFarmer || item == CitizenType.AdditionalFarmer);
        public int TotalWorkers => _citizens.Count(item => item == CitizenType.Worker);
        public int TotalRebels => _citizens.Count(item => item == CitizenType.Rebel);

        public SettlementCitizens(int settlementSize, float raceFarmingRate)
        {
            int subsistenceFarmers = CalculateSubsistenceFarmers(settlementSize, 0, raceFarmingRate);
            int additionalFarmers = settlementSize - subsistenceFarmers;

            _citizens = new List<CitizenType>();
            for (int i = 0; i < subsistenceFarmers; ++i)
            {
                _citizens.Add(CitizenType.SubsistenceFarmer);
            }

            for (int i = 0; i < additionalFarmers; ++i)
            {
                _citizens.Add(CitizenType.AdditionalFarmer);
            }
        }

        public void Increase()
        {
            _citizens.Add(CitizenType.AdditionalFarmer);
        }

        private int CalculateSubsistenceFarmers(int totalPopulation, int rebelPopulation, float raceFarmingRate)
        {
            // TODO: wild game not being factored in
            int foodNeeded = totalPopulation;
            // subtract food from granary, farmers market, foresters guild

            float farmersSubsistenceFloat = foodNeeded / raceFarmingRate;
            int farmersSubsistence = (int)Math.Ceiling(farmersSubsistenceFloat);

            if (totalPopulation - rebelPopulation >= farmersSubsistence)
            {
                return farmersSubsistence;
            }

            return totalPopulation - rebelPopulation;
        }
    }
}