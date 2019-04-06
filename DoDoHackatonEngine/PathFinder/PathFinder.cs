using Common;

namespace PathFinder
{
    public class PathFinder : IMovingAlgorithm
    {
        public Graph Graph = new Graph();

        public Point Current { get; set; }
        
        public Point Finish { get; private set; }

        public void Init(Point start, Point finish, int radius)
        {
            Graph.Init(radius);
            Current = start;
            Finish = finish;
        }
        
        public void AddHexes(HexType[] hexes)
        {
            
        }
        
        
        public (Direction, int) WhereToGo(Direction currentDirection, int currentVelocity) { throw new System.NotImplementedException(); }
    }
}