namespace RecyclingStation.Factories
{
    using RecyclingStation.WasteDisposal.Interfaces;

    public interface IDisposalStrategyFactory
    {
        IGarbageDisposalStrategy CreateDisposalStrategy(string type, IProcessingDataFactory processingDataFactory);
    }
}
