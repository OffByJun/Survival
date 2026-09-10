namespace AstraNope.Mechanics.Movement
{
    public interface IMovementMode
    {
        void Tick(MovementContext ctx);
    }
}
