using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Rootobject
    {
        public Command Command { get; set; }
        public Visiblecell[] VisibleCells { get; set; }
        public Location1 Location { get; set; }
        public int ShortestWayLength { get; set; }
        public int Speed { get; set; }
        public string Status { get; set; }
        public string Heading { get; set; }
        public int FuelWaste { get; set; }
    }

    public class Command
    {
        public Currentlocation Location { get; set; }
        public int Acceleration { get; set; }
        public string MovementDirection { get; set; }
        public string Heading { get; set; }
        public int Speed { get; set; }
        public int Fuel { get; set; }
    }

    public class Location1
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class Visiblecell
    {
        public Item1 Item1 { get; set; }
        public string Item2 { get; set; }
    }

    public class Item1
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

}
