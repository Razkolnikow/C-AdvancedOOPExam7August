namespace RecyclingStation.Models.Waste
{
    using System;
    using RecyclingStation.WasteDisposal.Interfaces;

    public abstract class Waste : IWaste
    {
        private string name;
        private double volumePerKg;
        private double weight;

        protected Waste(string name, double volumePerKg, double weight)
        {
            Name = name;
            VolumePerKg = volumePerKg;
            Weight = weight;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The name can not be null!");
                }

                this.name = value;
            }
        }

        public double VolumePerKg
        {
            get
            {
                return this.volumePerKg;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The volume per kg should be a positive number!");
                }

                this.volumePerKg = value;
            }
        }

        public double Weight
        {
            get
            {
                return this.weight;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The weight should be a positive number!");
                }
                this.weight = value;
            }
        }
    }
}
