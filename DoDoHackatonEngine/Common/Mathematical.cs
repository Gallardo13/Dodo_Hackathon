namespace Common
{
    public enum Direction
    {
        NorthWest = 1,
        West = 2,
        SouthWest = 3,
        SouthEast = 4,
        East = 5,
        NorthEast = 6
    }

    public enum HexType
    {
        Unknown = 0,
        Empty = 1,
        Rock = 2,
        DangerousArea = 3,
        Pit = 4
    }

    public struct DriftAngle
    {
        public int Angle { get; set; }
        public int MaxSpeed { get; set; }
        public int SpeedDownShift { get; set; }
    }

    public struct Angle
    {
        public Direction Direction { get; set; }
        public int AngleDegree { get; set; }
    }

    public struct Delta
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Dz { get; set; }
    }

    public struct LocationDelta
    {
        public Direction Direction { get; set; }
        public Delta Delta { get; set; }
    }

    public class Mathematical
    {
        public static Mathematical Instance { get; set; }

        public int MaxSpeed { get; set; }
        public int MinSpeed { get; set; }
        public int MaxAcceleration { get; set; }

        public DriftAngle[] DriftsAngles { get; set; }

        public int MinCanyonSpeed { get; set; }
        public int MaxDuneSpeed { get; set; }
        public int BaseTurnFuelWaste { get; set; }
        public int DriftFuelMultiplier { get; set; }
        public int FullSpeedFuelMultiplier { get; set; }

        public Angle[] Angles { get; set; }

        public LocationDelta[] LocationDeltas { get; set; }
    }
}
