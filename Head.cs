using System.Text;

namespace Headpose.NET
{
    public partial class Head : IHead
    {
        private static ApiContext _api;
        private TobiiTracker _tracker;

        public Head()
        {
            _api = new ApiContext();
            _tracker = new TobiiTracker(_api);
        }

        public float PositionX => _tracker.PositionX;

        public float PositionY => _tracker.PositionY;

        public float PositionZ => _tracker.PositionZ;

        public float RotationX => _tracker.RotationX;

        public float RotationY => _tracker.RotationY;

        public float RotationZ => _tracker.RotationZ;

    }
}
