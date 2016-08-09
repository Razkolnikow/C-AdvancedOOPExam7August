using RecyclingStation.WasteDisposal.Interfaces;

namespace RecyclingStation.Core.CoreInterfaces
{
    public interface IDatabase
    {
        void AddProcessingDataToDatabaseCollection(IProcessingData processingData);

        string GetStatus();
    }
}
