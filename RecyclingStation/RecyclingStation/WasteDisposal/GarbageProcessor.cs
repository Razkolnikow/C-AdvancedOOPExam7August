namespace RecyclingStation.WasteDisposal
{
    using System;
    using System.Linq;
    using RecyclingStation.WasteDisposal.Attributes;
    using RecyclingStation.WasteDisposal.Interfaces;

    public class GarbageProcessor : IGarbageProcessor
    {
        private IStrategyHolder strategyHolder;

        public GarbageProcessor(IStrategyHolder strategyHolder)
        {
            this.StrategyHolder = strategyHolder;
        }

        public IStrategyHolder StrategyHolder
        {
            get
            {
                return this.strategyHolder;
            }
            private set
            {
                this.strategyHolder = value;
            }
        }

        public IProcessingData ProcessWaste(IWaste garbage)
        {
            //Removed this part -> x => x.GetType() == typeof(DisposableAttribute) from .FirstOrDefault...
            Type type = garbage.GetType();
            DisposableAttribute disposalAttribute = (DisposableAttribute)type.GetCustomAttributes(true)
                .FirstOrDefault();
            IGarbageDisposalStrategy currentStrategy;
            
            if (disposalAttribute == null || !this.StrategyHolder
                .GetDisposalStrategies
                .TryGetValue(disposalAttribute.GetType(), out currentStrategy))
            {
                throw new ArgumentException(
                    "The passed in garbage does not implement a supported Disposable Strategy Attribute.");
            }

            return currentStrategy.ProcessGarbage(garbage);
        }
    }
}
