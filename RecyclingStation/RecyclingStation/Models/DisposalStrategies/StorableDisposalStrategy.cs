namespace RecyclingStation.Models.DisposalStrategies
{
    using RecyclingStation.WasteDisposal.Interfaces;
    using RecyclingStation.Factories;

    public class StorableDisposalStrategy : GarbageDisposalStrategy
    {
        public StorableDisposalStrategy(IProcessingDataFactory processingDataFactory)
            : base(processingDataFactory)
        {
        }

        public override IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = this.GetTotalGarbageVolume(garbage);
            double energyProduced = 0;
            double energyUsed = 0.13 * totalGarbageVolume;
            double earnedCapital = 0;
            double usedCapital = 0.65 * totalGarbageVolume;

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
