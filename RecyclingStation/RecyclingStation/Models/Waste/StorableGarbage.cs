namespace RecyclingStation.Models.Waste
{
    using RecyclingStation.WasteDisposal.Attributes;

    [Storable]
    public class StorableGarbage : Waste
    {
        public StorableGarbage(string name, double volumePerKg, double weight) 
            : base(name, volumePerKg, weight)
        {
        }
    }
}
