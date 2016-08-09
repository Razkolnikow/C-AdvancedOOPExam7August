using RecyclingStation.WasteDisposal.Interfaces;

namespace RecyclingStation.Factories
{
    public interface IProcessingDataFactory
    {
        IProcessingData CreateProcessingData(double energyBalance, double capitalBalance);
    }
}
