namespace RecyclingStation.Models.Waste
{
    using RecyclingStation.WasteDisposal.Attributes;

    [Recyclable]
    public class RecyclableGarbage : Waste
    {
        public RecyclableGarbage(string name, double volumePerKg, double weight) 
            : base(name, volumePerKg, weight)
        {
        }
    }
}
