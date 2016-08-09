namespace RecyclingStation.Core.CoreInterfaces
{
    public interface ICommandInterpreter
    {
        string ExecuteCommand(string command);
    }
}
