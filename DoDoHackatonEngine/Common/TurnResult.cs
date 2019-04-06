using Newtonsoft.Json;

namespace Common
{
    public class TurnResult
    {
        public Command Command { get; set; }
        public Visiblecell[] VisibleCells { get; set; }
        public Point Location { get; set; }
        public int ShortestWayLength { get; set; }
        public int Speed { get; set; }
        public string Status { get; set; }
        public string Heading { get; set; }
        public int FuelWaste { get; set; }
    }

    public class Command
    {
        public Point Location { get; set; }
        public int Acceleration { get; set; }
        public Direction MovementDirection { get; set; }
        public Direction Heading { get; set; }
        public int Speed { get; set; }
        public int Fuel { get; set; }
    }

    public class Visiblecell
    {
        [JsonProperty("Item1")]
        public Point Hex { get; set; }

        [JsonProperty("Item2")]
        public HexType HexType { get; set; }
    }
}
