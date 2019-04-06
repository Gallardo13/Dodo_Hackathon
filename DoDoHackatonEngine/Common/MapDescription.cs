using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{

    public class MapDescription
    {
        public string SessionId { get; set; }
        public string PlayerId { get; set; }
        public Direction CurrentDirection { get; set; }
        public Currentlocation CurrentLocation { get; set; }
        public Finish Finish { get; set; }
        public int Radius { get; set; }
        public int CurrentSpeed { get; set; }
        public string PlayerStatus { get; set; }
        public Neighbourcell[] NeighbourCells { get; set; }
        public int Fuel { get; set; }
    }

    public class Currentlocation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class Finish
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class Neighbourcell
    {
        [JsonProperty("Item1")]
        public Hex Hex { get; set; }

        [JsonProperty("Item2")]
        public HexType HexType { get; set; }
    }

    public class Hex
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
