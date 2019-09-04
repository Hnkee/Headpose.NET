namespace Headpose.NET
{
    public interface ILogger
    {
        void Debug(string message);
        void Warning(string message);
        void Error(string message);
    }
}