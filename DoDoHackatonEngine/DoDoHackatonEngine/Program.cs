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

            api.Login("SberWild", "LqA7xP");

            var mapDescription = api.Play("test");

            for (int i = 0; i < 15; i++)
            {
                var result = api.Move(mapDescription.SessionId, Direction.East, 30);
                Console.WriteLine(result.Status);
                Console.ReadLine();
            }
        }
    }
}
