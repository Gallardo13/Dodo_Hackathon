using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public class PathFinder : IMovingAlgorithm
    {
        private Dictionary<Point, PathVariant> currentVariants;
        
        public Graph Graph = new Graph();

        public Point Current { get; set; }
        
        public Point Finish { get; private set; }

        public void Init(Point start, Point finish, int radius)
        {
            Graph.Init(radius);
            Current = start;
            Finish = finish;
        }

        public void AddHexes(Visiblecell[] cells)
        {
            foreach (var cell in cells.Where(c => c != null))
            {
                if (Graph.Nodes.ContainsKey(cell.Hex))
                {
                    Graph.Nodes[cell.Hex] = cell.HexType;
                }
            }
        }
        
        public (Direction, int) WhereToGo(Direction currentDirection, int currentVelocity)
        {
            bool hasBetterVariants;
            currentVariants = new Dictionary<Point, PathVariant>()
            {
                {Current, new PathVariant() }
            };
            
            do
            {
                hasBetterVariants = false;
                foreach (var point in currentVariants.Keys.ToList())
                {
                    foreach (var dirPoint in Graph.GetAvailablePoints(point))
                    {
                        hasBetterVariants = hasBetterVariants ||
                                            Update(dirPoint.Item2, currentVariants[point], dirPoint.Item1);
                    }
                }
            }
            while (hasBetterVariants);

            var bestDirection = currentVariants[Finish].Moves.First().Direction;
            Current = Current.AddDirection(bestDirection);
            return (bestDirection, currentVelocity == 0 ? 30 : 0);
        }

        private bool Update(Point point, PathVariant path, Direction direction)
        {
            var thisTry = path.Copy();
            thisTry.Moves.Add(new Move() { Direction = direction });
            
            if (!currentVariants.TryGetValue(point, out var existing) || thisTry.Cost() < existing.Cost())
            {
                currentVariants[point] = thisTry;
                return true;
            }

            return false;
        }
    }
}