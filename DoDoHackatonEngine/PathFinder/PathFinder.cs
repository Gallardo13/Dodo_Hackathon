using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public class PathFinder : IMovingAlgorithm
    {
        private Dictionary<Point, PathVariant> currentVariants;
        
        public readonly Graph Graph = new Graph();
        
        public Point Finish { get; private set; }

        public void Init(Point finish, int radius)
        {
            Graph.Init(radius);
            Finish = finish;
        }

        public void AddHexes(Visiblecell[] cells)
        {
            cells
                .Where(c => c != null && Graph.Nodes.ContainsKey(c.Hex))
                .ToList()
                .ForEach(c => Graph.Nodes[c.Hex] = c.HexType);
        }

        public (Direction, int) WhereToGo(Point currentLocation, Direction currentDirection, int currentVelocity)
        {
            bool hasBetterVariants;
            
            currentVariants = new Dictionary<Point, PathVariant>()
            {
                { currentLocation, new PathVariant { Speed = currentVelocity, Direction = currentDirection, } }
            };
            
            do
            {
                hasBetterVariants = false;
                foreach (var point in currentVariants.Keys.ToList())
                {
                    hasBetterVariants |= Graph.GetAvailableDirections(point)
                        .Select(d => Update(point, d))
                        .ToList()
                        .Any(b => b);
                }
            }
            while (hasBetterVariants);

            var bestPath = currentVariants[Finish].Moves.First();
            return (bestPath.Direction, bestPath.Acceleration);
        }

        private bool Update(Point from, Direction direction)
        {
            var currentVariant = currentVariants[from];
            var thisTry = currentVariant.AddMove(direction, FindAcceleration(from, direction, currentVariant.Speed));
            var targetPoint = from.AddDirection(direction);
            
            if (!currentVariants.TryGetValue(targetPoint, out var existing) ||
                existing.AggregateSpeed < thisTry.AggregateSpeed)
            {
                currentVariants[targetPoint] = thisTry;
                return true;
            }

            return false;
        }

        private int FindAcceleration(Point from, Direction direction, int currentSpeed)
        {
            var next = from.AddDirection(direction);
            var nextNext = next.AddDirection(direction);

            Graph.Nodes.TryGetValue(next, out var nextType);
            Graph.Nodes.TryGetValue(nextNext, out var nextNextType);
            
            var targetSpeed = 70;
            
            switch (nextType)
            {
                case HexType.Pit:
                    targetSpeed = 70;
                    break;
                case HexType.DangerousArea:
                    targetSpeed = 30;
                    break;
                default:
                    switch (nextNextType)
                    {
                        case HexType.Pit:
                            targetSpeed = 70;
                            break;
                        case HexType.DangerousArea:
                            targetSpeed = 30;
                            break;
                    }

                    break;
            }

            if (targetSpeed > currentSpeed)
            {
                return Math.Min(30, targetSpeed - currentSpeed);
            }

            return Math.Max(-30, targetSpeed - currentSpeed);
        }
    }
}