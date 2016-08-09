namespace RecyclingStation.Models.DisposalStrategies
{
    using System;
    using RecyclingStation.WasteDisposal.Interfaces;
    using RecyclingStation.Factories;

    public abstract class GarbageDisposalStrategy : IGarbageDisposalStrategy
    {
        protected GarbageDisposalStrategy(IProcessingDataFactory processingDataFactory)
        {
            this.ProcessingDataFactory = processingDataFactory;
        }

        protected IProcessingDataFactory ProcessingDataFactory { get; private set; }

        public abstract IProcessingData ProcessGarbage(IWaste garbage);

        protected double GetTotalGarbageVolume(IWaste garbage)
        {
            return garbage.Weight*garbage.VolumePerKg;
        }
    }
}
