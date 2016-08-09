namespace RecyclingStation.Models.DisposalStrategies
{
    using RecyclingStation.WasteDisposal.Interfaces;
    using RecyclingStation.Factories;
    public class BurnableDisposalStrategy : GarbageDisposalStrategy
    {
        public BurnableDisposalStrategy(IProcessingDataFactory processingDataFactory) 
            : base(processingDataFactory)
        {
        }

        public override IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = this.GetTotalGarbageVolume(garbage);
            double energyProduced = totalGarbageVolume;
            double energyUsed = 0.2 * totalGarbageVolume;

            double energyBalance = 0;
            energyBalance -= energyUsed;
            energyBalance += energyProduced;
            double capitalBalance = 0;

            return this.ProcessingDataFactory.CreateProcessingData(energyBalance, capitalBalance);
        }
    }
}
