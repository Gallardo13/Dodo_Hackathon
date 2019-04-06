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
                //BaseUrl = "http://127.0.0.1:5000"
                BaseUrl = "http://51.158.109.80:5000"
            };

            Mathematical.Instance = api.GetMath();

            api.Login("SberWild", "LqA7xP");

            var algo = new PathFinder.PathFinder();

            var mapDescription = api.Play("the_maze");

            algo.Init(mapDescription.Finish, mapDescription.Radius);
            algo.AddHexes(mapDescription.NeighbourCells);

            var currentDirection = mapDescription.CurrentDirection;
            var currentSpeed = mapDescription.CurrentSpeed;
            var currentLocation = mapDescription.CurrentLocation;

            var turnCount = 0;
            while (true)
            {
                var (direction, acceleration) = algo.WhereToGo(currentLocation, currentDirection, currentSpeed);

                var moveResult = api.Move(mapDescription.SessionId, direction, acceleration);

                api.RefreshMap(mapDescription.SessionId);

                algo.AddHexes(moveResult.VisibleCells);
                currentDirection = moveResult.Heading;
                currentSpeed = moveResult.Speed;
                currentLocation = moveResult.Location;

                Console.Write($"Position: {moveResult.Location.X} {moveResult.Location.Y} {moveResult.Location.Z}, " +
                    $"Velocity: {moveResult.Speed}, Status: {moveResult.Status}, ");

                if (moveResult.VisibleCells.Count() > 0)
                    Console.Write($"Hex type: {moveResult.VisibleCells.First(e=>e.Hex.X == moveResult.Location.X && e.Hex.Y == moveResult.Location.Y && e.Hex.Z == moveResult.Location.Z).HexType}");

                Console.WriteLine();

                turnCount++;

                if (
                    moveResult.Status == TurnStatus.Punished
                    || moveResult.Status == TurnStatus.HappyAsInsane
                    || moveResult.Status == TurnStatus.Hungry)

                    break;
            }

            Console.WriteLine($"Total turns: {turnCount}");
            Console.ReadLine();
        }
    }
}
