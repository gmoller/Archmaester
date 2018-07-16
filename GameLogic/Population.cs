namespace GameLogic
{
    public class Population
    {
        public int SubsistenceFarmers { get; private set; }
        public int AdditionalFarmers { get; private set; }
        public int Workers { get; private set; }
        public int Rebels { get; private set; }
        public int TotalFarmers => SubsistenceFarmers + AdditionalFarmers;
        public int Total => SubsistenceFarmers + AdditionalFarmers + Workers + Rebels;

        public Population(int subsistenceFarmers, int additionalFarmers, int workers, int rebels)
        {
            SubsistenceFarmers = subsistenceFarmers;
            AdditionalFarmers = additionalFarmers;
            Workers = workers;
            Rebels = rebels;
        }

        //public void Set(int farmers, int workers, int rebels)
        //{
        //    Farmers = farmers;
        //    Workers = workers;
        //    Rebels = rebels;
        //}
    }
}