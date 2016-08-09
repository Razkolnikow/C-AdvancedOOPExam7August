namespace RecyclingStation.Core
{
    using RecyclingStation.Core.CoreInterfaces;
    using RecyclingStation.IO;

    public class AppEngine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ICommandInterpreter commandInterpreter;

        public AppEngine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            string command = this.reader.ReadLine();
            while (command != "TimeToRecycle")
            {
                string output = this.commandInterpreter.ExecuteCommand(command);

                this.writer.WriteLine(output);

                command = this.reader.ReadLine();
            }
        }
    }
}
