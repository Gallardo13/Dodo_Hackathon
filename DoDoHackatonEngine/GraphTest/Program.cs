using System;
using PathFinder;

namespace GraphTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var g = new Graph();
            g.Init(3);
            Console.WriteLine(g.Nodes.Count);

            foreach (var p in g.Nodes.Keys)
            {
                Console.WriteLine($"{p.X} {p.Y} {p.Z}");
            }
        }
    }
}