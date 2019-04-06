using Newtonsoft.Json;

namespace Common
{
    public class MapDescription
    {
        public string SessionId { get; set; }
        public string PlayerId { get; set; }
        public Direction CurrentDirection { get; set; }
        public Point CurrentLocation { get; set; }
        public Point Finish { get; set; }
        public int Radius { get; set; }
        public int CurrentSpeed { get; set; }
        public string PlayerStatus { get; set; }
        public Visiblecell[] NeighbourCells { get; set; }
        public int Fuel { get; set; }
    }
}
