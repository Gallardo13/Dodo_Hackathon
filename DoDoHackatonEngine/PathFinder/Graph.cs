﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public static class Graph
    {
        public static Dictionary<Point, HexType> Nodes = new Dictionary<Point, HexType>();
        
        public static void Init(int radius)
        {
            var countOfNodesInMaxRadius = (radius - 2) * 6 + 6;
            
            for (var r = 0; r <= int.MaxValue; r++)
            {
                var nodes = GenerateCircle(r).ToList();
                if (r > 0 && nodes.Count > countOfNodesInMaxRadius)
                {
                    break;
                }
                
                nodes.ForEach(n => Nodes.Add(n.Hex, n.HexType));
            }
        }

        public static IEnumerable<Direction> GetAvailableDirections(Point current)
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (Nodes.TryGetValue(current.AddDirection(dir), out var node) && node != HexType.Rock)
                    yield return dir;
            }
        }

        private static IEnumerable<Visiblecell> GenerateCircle(int radius)
        {
            for (var x = -radius; x <= radius; x++)
                for (var y = -radius; y <= radius; y++)
                {
                    var z = 0 - x - y;
                    var isOnEdge = Math.Abs(x) == radius || Math.Abs(y) == radius || Math.Abs(z) == radius;
                    if (isOnEdge && Math.Abs(z) <= radius)
                    {
                        yield return new Visiblecell()
                        {
                            Hex = new Point(x, y, 0 - x - y),
                            HexType = HexType.Unknown,
                        };
                    }
                }
        }
    }
}