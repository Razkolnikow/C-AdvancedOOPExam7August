namespace RecyclingStation.Models.Waste
{
    using RecyclingStation.WasteDisposal.Attributes;

    [Burnable]
    public class BurnableGarbage : Waste
    {
        public BurnableGarbage(string name, double volumePerKg, double weight) 
            : base(name, volumePerKg, weight)
        {
        }
    }
}
