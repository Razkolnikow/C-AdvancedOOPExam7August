namespace RecyclingStation.Models.DisposalStrategies
{
    using RecyclingStation.WasteDisposal.Interfaces;
    using RecyclingStation.Factories;

    public class RecyclableDisposalStrategy : GarbageDisposalStrategy
    {
        public RecyclableDisposalStrategy(IProcessingDataFactory processingDataFactory) 
            : base(processingDataFactory)
        {
        }

        public override IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = this.GetTotalGarbageVolume(garbage);
            double energyProduced = 0;
            double energyUsed = 0.5 * totalGarbageVolume;
            double earnedCapital = 400*garbage.Weight;
            double usedCapital = 0;

            double energyBalance = 0;
            energyBalance -= energyUsed;
            energyBalance += energyProduced;
            double capitalBalance = 0;
            capitalBalance -= usedCapital;
            capitalBalance += earnedCapital;

            return this.ProcessingDataFactory.CreateProcessingData(energyBalance, capitalBalance);
        }
    }
}
