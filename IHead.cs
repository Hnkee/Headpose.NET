namespace Headpose.NET
{
    public interface IHead
    {
        float PositionX { get; }
        float PositionY { get; }
        float PositionZ { get; }
        float RotationX { get; }
        float RotationY { get; }
        float RotationZ { get; }
    }
}