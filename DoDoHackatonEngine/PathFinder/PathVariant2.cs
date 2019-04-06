using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public class PathVariant
    {
        public List<Move> Moves { get; } = new List<Move>();

        public int Speed { get; set; }

        public Direction Direction { get; set; }
        
        public Point Point { get; set; }
        
        public decimal TotalTime { get; set; }

        public PathVariant AddMove(Direction direction, int acceleration)
        {
            var result = new PathVariant();
            Moves.ForEach(result.Moves.Add);

            result.Speed = Speed + acceleration;
            result.Direction = direction;
            result.TotalTime += 1m / result.Speed;
            result.Point = Point.AddDirection(direction);
            
            result.Moves.Add(new Move() { Direction = direction, Acceleration = acceleration, Point = result.Point });

            return result;
        }

        public bool IsBetterThan(PathVariant other) => TotalTime < other.TotalTime;
    }

    public class Move
    {
        public Direction Direction { get; set; }
        
        public int Acceleration { get; set; } = 0;
        
        public Point Point { get; set; }
    }
}