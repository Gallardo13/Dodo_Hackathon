using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public class PathVariant
    {
        public int AggregateSpeed { get; set; }
        
        public List<Move> Moves { get; } = new List<Move>();

        public int Speed { get; set; }

        public Direction Direction { get; set; }

        public PathVariant AddMove(Direction direction, int acceleration)
        {
            var result = new PathVariant();
            Moves.ForEach(result.Moves.Add);
            Moves.Add(new Move() { Direction = direction, Acceleration = acceleration });
            
            result.AggregateSpeed += AggregateSpeed;
            result.Speed += acceleration;
            result.Direction = Direction;
            
            return result;
        }
    }

    public class Move
    {
        public Direction Direction { get; set; }
        
        public int Acceleration { get; set; } = 0;
    }
}