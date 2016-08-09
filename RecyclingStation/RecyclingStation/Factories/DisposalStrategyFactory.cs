namespace RecyclingStation.Factories
{
    using RecyclingStation.WasteDisposal.Interfaces;
    using RecyclingStation.Models.DisposalStrategies;

    public class DisposalStrategyFactory : IDisposalStrategyFactory
    {
        public IGarbageDisposalStrategy CreateDisposalStrategy(string type, IProcessingDataFactory processingDataFactory)
        {
            switch (type)
            {
                case "Recyclable":
                    return new RecyclableDisposalStrategy(processingDataFactory);
                case "Burnable":
                    return new BurnableDisposalStrategy(processingDataFactory);
                case "Storable":
                    return new StorableDisposalStrategy(processingDataFactory);
                default:
                    return null;
            }
        }
    }
}
