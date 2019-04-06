namespace Common
{
    public interface IMovingAlgorithm
    {
        void Init(Point finish, int radius);
        
        void AddHexes(Visiblecell[] cells);

        (Direction, int) WhereToGo(Point currentLocation, Direction currentDirection, int currentVelocity);
    }
}
