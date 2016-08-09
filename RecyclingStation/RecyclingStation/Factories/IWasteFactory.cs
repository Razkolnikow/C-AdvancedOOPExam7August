using RecyclingStation.WasteDisposal.Interfaces;

namespace RecyclingStation.Factories
{
    public interface IWasteFactory
    {
        IWaste CreateGarbage(string type, string name, double weight, double volumePerKg);
    }
}
