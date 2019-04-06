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

    interface IMovingAlgorithm
    {
        void AddHexes(HexType[] hexes);

        (Direction, int) WhereToGo(Direction currentDirection, int currentVelocity);
    }
}
