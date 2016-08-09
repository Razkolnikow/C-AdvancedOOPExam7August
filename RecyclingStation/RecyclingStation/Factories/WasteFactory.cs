namespace RecyclingStation.Factories
{
    using RecyclingStation.Models.Waste;
    using RecyclingStation.WasteDisposal.Interfaces;

    public class WasteFactory : IWasteFactory
    {
        public IWaste CreateGarbage(string type, string name, double weight, double volumePerKg)
        {
            switch (type)
            {
                case "Recyclable":
                    return new RecyclableGarbage(name, volumePerKg, weight);
                case "Burnable":
                    return new BurnableGarbage(name, volumePerKg, weight);
                case "Storable":
                    return new StorableGarbage(name, volumePerKg, weight);
                default:
                    return null;
            }
        }
    }
}
