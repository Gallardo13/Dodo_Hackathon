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
            var api = new Api
            {
                BaseUrl = "http://51.15.100.12:5000"
            };

            Mathematical.Instance = api.GetMath();

            api.Login("SberWild", "LqA7xP");

            var algo = new PathFinder.PathFinder();

            var mapDescription = api.Play("test");

            algo.Init(mapDescription.Finish, mapDescription.Radius);
            algo.AddHexes(mapDescription.NeighbourCells);

            var currentDirection = mapDescription.CurrentDirection;
            var currentSpeed = mapDescription.CurrentSpeed;
            var currentLocation = mapDescription.CurrentLocation;

            while (true)
            {
                var (direction, acceleration) = algo.WhereToGo(currentLocation, currentDirection, currentSpeed);

                var moveResult = api.Move(mapDescription.SessionId, direction, acceleration);
                algo.AddHexes(moveResult.VisibleCells);
                currentDirection = moveResult.Heading;
                currentSpeed = moveResult.Speed;
                currentLocation = moveResult.Location;

                Console.WriteLine($"{moveResult.Location.X} {moveResult.Location.Y} {moveResult.Location.Z} " +
                    $"{moveResult.VisibleCells.First(e=>e.Hex.X == moveResult.Location.X && e.Hex.Y == moveResult.Location.Y && e.Hex.Z == moveResult.Location.Z).HexType}");
                Console.WriteLine(moveResult.Status);
                Thread.Sleep(300);

                if (
                    moveResult.Status == TurnStatus.Punished
                    || moveResult.Status == TurnStatus.HappyAsInsane
                    || moveResult.Status == TurnStatus.Hungry)

                    break;
            }

            Console.ReadLine();
        }
    }
}
