namespace Common
{
    public interface IMovingAlgorithm
    {
        void Init(Point start, Point finish, int radius);
        
        void AddHexes(Visiblecell[] cells);

        (Direction, int) WhereToGo(Direction currentDirection, int currentVelocity);
    }
}
