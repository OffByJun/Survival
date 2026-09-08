namespace AstraNope.Data.Structures
{
    public interface IPlaceable
    {
        bool IsPlaced { get; }
        bool CanRotate { get; }
        void Place();
        void Remove();
    }
}
