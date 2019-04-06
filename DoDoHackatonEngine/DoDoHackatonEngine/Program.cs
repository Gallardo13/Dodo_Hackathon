using Common;
using System;
using System.Linq;
using System.Threading;

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
            algo.AddHexes(mapDescription.NeighbourCells));

            var currentDirection = mapDescription.CurrentDirection;
            var currentSpeed = mapDescription.CurrentSpeed;

            while (true)
            {
                var (direction, acceleration) = algo.WhereToGo(currentDirection, currentSpeed);

                var moveResult = api.Move(mapDescription.SessionId, direction, acceleration);
                algo.AddHexes(moveResult.VisibleCells);
                currentDirection = moveResult.Heading;
                currentSpeed = moveResult.Speed;

                Console.WriteLine(moveResult.Status);
                Thread.Sleep(500);

                if (
                    moveResult.Status == TurnStatus.Punished
                    || moveResult.Status == TurnStatus.HappyAsInsane
                    || moveResult.Status == TurnStatus.Hungry)

                    break;
            }
        }
    }
}
