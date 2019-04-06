using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public class PathFinder : IMovingAlgorithm
    {
        private bool hasChanges;
        private Dictionary<Point, Dictionary<DirectionSpeed, PathVariant>> currentVariants;
        
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
            currentVariants = Graph.Nodes.Keys
                .ToDictionary(p => p, p => new Dictionary<DirectionSpeed, PathVariant>());
            
            var pathToCurrent = new PathVariant()
            {
                Point = currentLocation,
                Speed = currentVelocity,
                Direction = currentDirection,
            };
            
            currentVariants[currentLocation] = new Dictionary<DirectionSpeed, PathVariant>()
            {
                { DirectionSpeed.FromPathVariant(pathToCurrent), pathToCurrent}
            };
            
            do
            {
                hasChanges = false;
                foreach (var pathToSource in currentVariants.Values.SelectMany(d => d.Values).ToList())
                for (var acceleration = -30; acceleration <= 30; acceleration += 10)
                foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                {
                    Update(pathToSource, dir, acceleration);
                }
            }
            while (hasChanges);

            var bestPath = currentVariants[Finish].Values.OrderBy(x => x.TotalTime).First();
            var lastKnown = bestPath.Moves
                .TakeWhile(p => Graph.Nodes[p.Point] != HexType.Unknown)
                .Last().Point;

            var result = currentVariants[lastKnown].Values.OrderBy(x => x.TotalTime).First().Moves.First();
            return (result.Direction, result.Acceleration);
        }

        private void Update(PathVariant path, Direction direction, int acceleration)
        {
            var newPath = path.AddMove(direction, acceleration);
            var newPoint = newPath.Point;
            if (newPoint.IsValid(newPath.Speed))
            {
                var dict = currentVariants[newPoint];
                var key = DirectionSpeed.FromPathVariant(newPath);
                if (!dict.TryGetValue(key, out var old) || newPath.IsBetterThan(old))
                {
                    dict[key] = newPath;
                    hasChanges = true;
                }
            }
        }
        
        private class DirectionSpeed : IEquatable<DirectionSpeed>
        {
            public Direction Direction;

            public int Speed;

            public static DirectionSpeed FromPathVariant(PathVariant path)
            {
                return new DirectionSpeed()
                {
                    Direction = path.Direction,
                    Speed = path.Speed,
                };
            }

            public bool Equals(DirectionSpeed other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Direction == other.Direction && Speed == other.Speed;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((DirectionSpeed) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((int) Direction * 397) ^ Speed;
                }
            }
        }
    }
}