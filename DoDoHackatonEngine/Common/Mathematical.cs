using System;

namespace Common
{
    public class DirectionAngleAttribute : Attribute
    {
        public int Angle { get; set; }
    }

    public enum Direction
    {
        [DirectionAngle(Angle = 240)]
        NorthWest = 1,

        [DirectionAngle(Angle = 180)]
        West = 2,

        [DirectionAngle(Angle = 120)]
        SouthWest = 3,

        [DirectionAngle(Angle = 60)]
        SouthEast = 4,

        [DirectionAngle(Angle = 0)]
        East = 5,

        [DirectionAngle(Angle = 300)]
        NorthEast = 6
    }

    public static class DirectionExtensions
    {
        public static int GetAngleBetweenDirections(this Direction from, Direction to)
        {
            var angleFrom = ((DirectionAngleAttribute[])from.GetType()
                .GetCustomAttributes(typeof(DirectionAngleAttribute), false))[0].Angle;

            var angleTo = ((DirectionAngleAttribute[])to.GetType()
                .GetCustomAttributes(typeof(DirectionAngleAttribute), false))[0].Angle;

            var minAngle = Math.Min(angleFrom, angleTo);
            var maxAngle = Math.Min(angleFrom, angleTo);

            var oneVal = minAngle - maxAngle;
            var secVal = oneVal;

            if (minAngle == 0)
                secVal = 360 - maxAngle;

            return Math.Min(oneVal, secVal);
        }
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
