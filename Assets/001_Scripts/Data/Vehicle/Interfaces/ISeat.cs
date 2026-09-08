using UnityEngine;

namespace AstraNope.Data.Vehicles
{
    public interface IOccupancy
    {
        bool IsOccupied { get; }
    }

    public interface ISeat : IOccupancy, ICameraAnchored
    {
        IVehicleControllable Controller { get; }
        void Sit(Transform player);
        void Stand(Transform player, Transform standSpawnPoint, Transform reparentTo);
    }
}
