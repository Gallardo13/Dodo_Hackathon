using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public class PathVariant
    {
        public int Cost() => Moves.Count();
        
        public List<Move> Moves { get; } = new List<Move>();

        public PathVariant Copy()
        {
            var result = new PathVariant();
            Moves.ForEach(result.Moves.Add);
            return result;
        }
    }

    public class Move
    {
        public Direction Direction { get; set; }
        public int Acceleration { get; set; } = 0;
    }
}