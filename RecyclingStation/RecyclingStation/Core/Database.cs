namespace RecyclingStation.Core
{
    using RecyclingStation.Core.CoreInterfaces;
    using RecyclingStation.WasteDisposal.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    class Database : IDatabase
    {
        private IList<IProcessingData> collectionOfProcessingData;

        public Database()
        {
            this.collectionOfProcessingData = new List<IProcessingData>();
        } 

        public void AddProcessingDataToDatabaseCollection(IProcessingData processingData)
        {
            this.collectionOfProcessingData.Add(processingData);
        }

        public string GetStatus()
        {
            var energy = this.collectionOfProcessingData.Sum(p => p.EnergyBalance);
            var capital = this.collectionOfProcessingData.Sum(p => p.CapitalBalance);
            return string.Format("Energy: {0:f2} Capital: {1:f2}", 
                energy, capital);
        }
    }
}
