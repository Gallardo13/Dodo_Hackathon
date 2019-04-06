using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public static class PointHelper
    {
        public static Point AddDirection(this Point point, Direction direction)
        {
            var delta = Mathematical.Instance.LocationDeltas.First(e => e.Direction == direction);

            return new Point(point.X + delta.Delta.Dx, point.Y + delta.Delta.Dy, point.Z + delta.Delta.Dz);
        }

        public static IEnumerable<Point> NearPoints(this Point point)
        {
            foreach (Direction d in Enum.GetValues(typeof(Direction)))
            {
                yield return point.AddDirection(d);
            }
        }

        public static bool IsValid(this Point point, int speed)
        {
            if (!Graph.Nodes.TryGetValue(point, out var type))
            {
                return false;
            }

            switch (type)
            {
                case HexType.Rock:
                case HexType.DangerousArea when speed > 30:
                case HexType.Pit when speed < 70:
                    return false;
                default:
                    return true;
            }
        }
    }
}