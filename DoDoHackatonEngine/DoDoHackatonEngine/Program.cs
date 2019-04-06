using Common;
using System;

namespace DoDoHackatonEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new Api();
            api.BaseUrl = "http://51.15.100.12:5000";

            Mathematical.Instance = api.GetMath();

            api.Login("SberWild", "LqA7xP");

            var algo = new PathFinder.PathFinder();

            var mapDescription = api.Play("test");
            algo.AddHexes(GetVisiblecells(mapDescription.NeighbourCells));

            for (int i = 0; i < 15; i++)
            {
                var result = api.Move(mapDescription.SessionId, Direction.East, 30);
                Console.WriteLine(result.Status);
                Console.ReadLine();
            }
        }

        public static Visiblecell[] GetVisiblecells(Visiblecell[] visibleCells)
        {

        }
    }
}
