namespace Common
{
    public interface IMovingAlgorithm
    {
        void AddHexes(HexType[] hexes);

        (Direction, int) WhereToGo(Direction currentDirection, int currentVelocity);
    }
}
