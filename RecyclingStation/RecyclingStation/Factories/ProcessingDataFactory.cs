using RecyclingStation.Models;
using RecyclingStation.WasteDisposal.Interfaces;

namespace RecyclingStation.Factories
{
    public class ProcessingDataFactory : IProcessingDataFactory
    {
        public IProcessingData CreateProcessingData(double energyBalance, double capitalBalance)
        {
            return new ProcessingData(energyBalance, capitalBalance);
        }
    }
}
