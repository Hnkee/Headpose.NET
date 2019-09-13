using Headpose.NET.Filters;

namespace Headpose.NET
{
    public class FilteredHead : IHead
    {
        private IHead _head;
        public OneEuroFilter PosXFilter;
        public OneEuroFilter PosYFilter;
        public OneEuroFilter PosZFilter;
        public OneEuroFilter RotXFilter;
        public OneEuroFilter RotYFilter;
        public OneEuroFilter RotZFilter;
        public double Rate;
        public FilteredHead(IHead head, double minCutOff = 1, double beta = 0.001d, double rate = 100)
        {
            _head = head;
            Rate = rate;
            PosXFilter = new OneEuroFilter(minCutOff, beta);
            PosYFilter = new OneEuroFilter(minCutOff, beta);
            PosZFilter = new OneEuroFilter(minCutOff, beta);
            RotXFilter = new OneEuroFilter(minCutOff, beta);
            RotYFilter = new OneEuroFilter(minCutOff, beta);
            RotZFilter = new OneEuroFilter(minCutOff, beta);
        }

        public float PositionX => (float) PosXFilter.Filter(_head.PositionX, Rate);

        public float PositionY => (float) PosYFilter.Filter(_head.PositionY, Rate);

        public float PositionZ => (float) PosZFilter.Filter(_head.PositionZ, Rate);

        public float RotationX => (float) RotXFilter.Filter(_head.RotationX, Rate);

        public float RotationY => (float) RotYFilter.Filter(_head.RotationY, Rate);

        public float RotationZ => (float) RotZFilter.Filter(_head.RotationZ, Rate);
    }
}
