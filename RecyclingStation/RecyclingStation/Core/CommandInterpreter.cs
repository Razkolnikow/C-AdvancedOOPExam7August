using RecyclingStation.WasteDisposal;

namespace RecyclingStation.Core
{
    using System;
    using RecyclingStation.Core.CoreInterfaces;
    using RecyclingStation.Factories;
    using RecyclingStation.WasteDisposal.Interfaces;
    using System.Linq;
    using RecyclingStation.WasteDisposal.Attributes;

    public class CommandInterpreter : ICommandInterpreter
    {
        private IProcessingDataFactory processingDataFactory;
        private IWasteFactory wasteFactory;
        private IGarbageProcessor garbageProcessor;
        private IStrategyHolder strategyHolder;
        private IDatabase database;
        private IDisposalStrategyFactory disposalStrategyFactory;

        public CommandInterpreter(IProcessingDataFactory processingDataFactory, 
            IWasteFactory wasteFactory, IGarbageProcessor garbageProcessor, 
            IStrategyHolder strategyHolder, IDatabase database, IDisposalStrategyFactory disposalStrategyFactory)
        {
            this.processingDataFactory = processingDataFactory;
            this.wasteFactory = wasteFactory;
            this.garbageProcessor = garbageProcessor;
            this.strategyHolder = strategyHolder;
            this.database = database;
            this.disposalStrategyFactory = disposalStrategyFactory;
        }

        public string ExecuteCommand(string command)
        {
            string resultFromCommand = string.Empty;
            var commandArgs = command.Split(new[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries);
            var currentCommand = commandArgs[0];

            switch (currentCommand)
            {
                case "ProcessGarbage":
                    resultFromCommand = this.ProcessGarbage(commandArgs);
                    break;
                case "Status":
                    resultFromCommand = this.GetCurrentStatus();
                    break;

            }

            return resultFromCommand;
        }

        private string GetCurrentStatus()
        {
            return this.database.GetStatus();
        }

        private string ProcessGarbage(string[] commandArgs)
        {
            string garbageName = commandArgs[1];
            double weight = double.Parse(commandArgs[2]);
            double volumePerKg = double.Parse(commandArgs[3]);
            string type = commandArgs[4];
            var garbage = this.wasteFactory.CreateGarbage(type, garbageName, weight, volumePerKg);
            var strategy = this.disposalStrategyFactory
                .CreateDisposalStrategy(type, this.processingDataFactory);
            DisposableAttribute disposalAttribute = (DisposableAttribute)garbage
                .GetType()
                .GetCustomAttributes(true)
                .FirstOrDefault();
            this.strategyHolder.AddStrategy(disposalAttribute.GetType(), strategy);
            var processedData = this.garbageProcessor.ProcessWaste(garbage);
            this.database.AddProcessingDataToDatabaseCollection(processedData);
            return string.Format("{0:f2} kg of {1} successfully processed!", 
                garbage.Weight, garbage.Name);
        }
    }
}
