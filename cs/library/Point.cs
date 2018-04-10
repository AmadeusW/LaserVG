namespace Deo.LaserVg
{
    public struct Point
    {
        public decimal X;
        public decimal Y;

        public Point((decimal x, decimal y) point)
        {
            X = point.x;
            Y = point.y;
        }

        public Point(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator (decimal x, decimal y) (Point point)
        {
            return (point.X, point.Y);
        }

        public static implicit operator Point ((decimal x, decimal y) tuple)
        {
            return new Point(tuple);
        }
    }
}
