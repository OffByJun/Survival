namespace AstraNope.Data.Structures
{
    public interface IPowerState
    {
        bool IsPowered { get; }
    }

    public interface IPowerable : IPowerState
    {
        void PowerUp();
        void PowerDown();
    }
}
