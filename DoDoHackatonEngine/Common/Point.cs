using System;
using System.Linq;

namespace Common
{
    public class Point : IEquatable<Point>
    {
        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point AddDirection(this Point point, Direction direction)
        {
            var delta = Mathematical.Instance.LocationDeltas.First(e => e.Direction == direction);

            return new Point(point.X + delta.Delta.Dx, point.Y + delta.Delta.Dy, point.Z + delta.Delta.Dz);
        }
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }

        public override string ToString() => $"{X} {Y} {Z}";
    }
}