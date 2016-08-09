namespace RecyclingStation
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using RecyclingStation.Core;
    using RecyclingStation.Factories;
    using RecyclingStation.IO;
    using RecyclingStation.Models.Waste;
    using RecyclingStation.WasteDisposal;
    using RecyclingStation.WasteDisposal.Attributes;
    using RecyclingStation.WasteDisposal.Interfaces;

    public class RecyclingStationMain
    {
        public static void Main()
        {
            var reader = new ConsoleReader();
            var writer = new ConsoleWriter();
            var database = new Database();
            var processingDataFactory = new ProcessingDataFactory();
            var wasteFactory = new WasteFactory();
            var strategyHolder = new StrategyHolder();
            var garbageProcessor = new GarbageProcessor(strategyHolder);
            var disposalStrategyFactory = new DisposalStrategyFactory();
            var commandInterpreter = new CommandInterpreter(
                processingDataFactory, wasteFactory, 
                garbageProcessor, strategyHolder, 
                database, disposalStrategyFactory);
            var engine = new AppEngine(reader, writer, commandInterpreter);
            engine.Run();
        }
    }
}
