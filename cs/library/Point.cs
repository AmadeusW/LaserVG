using System;

namespace Deo.LaserVg
{
    public struct Point : IEquatable<Point>
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

        public override string ToString()
        {
            return $"{X:0.####},{Y:0.####}";
        }

        bool IEquatable<Point>.Equals(Point other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point otherPoint)
                return ((IEquatable<Point>)this).Equals(otherPoint);
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public static implicit operator (decimal x, decimal y) (Point point)
        {
            return (point.X, point.Y);
        }

        public static implicit operator Point ((decimal x, decimal y) tuple)
        {
            return new Point(tuple);
        }

        public static Point operator + (Point a, Point b)
        {
            return (a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return (a.X - b.X, a.Y - b.Y);
        }

        public static Point operator *(Point a, decimal scale)
        {
            return (a.X * scale, a.Y * scale);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static Point operator ~(Point a)
        {
            return (-a.X, -a.Y);
        }
    }
}
