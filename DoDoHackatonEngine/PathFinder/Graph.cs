using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace PathFinder
{
    public class Graph
    {
        public Dictionary<Point, GraphNode> Nodes = new Dictionary<Point, GraphNode>();
        
        public void Init(int radius)
        {
            var countOfNodesInMaxRadius = (radius - 2) * 6 + 6;
            
            for (var r = 0; r <= int.MaxValue; r++)
            {
                var nodes = GenerateCircle(r).ToList();
                if (r > 0 && nodes.Count > countOfNodesInMaxRadius)
                {
                    break;
                }
                
                nodes.ForEach(n => Nodes.Add(n.Point, n));
            }
        }

        private IEnumerable<GraphNode> GenerateCircle(int radius)
        {
            for (var x = -radius; x <= radius; x++)
                for (var y = -radius; y <= radius; y++)
                {
                    var z = 0 - x - y;
                    var isOnEdge = Math.Abs(x) == radius || Math.Abs(y) == radius || Math.Abs(z) == radius;
                    if (isOnEdge && Math.Abs(z) <= radius)
                    {
                        yield return new GraphNode()
                        {
                            Point = new Point(x, y, 0 - x - y),
                            HexType = HexType.Unknown,
                        };
                    }
                }
        }
    }
}