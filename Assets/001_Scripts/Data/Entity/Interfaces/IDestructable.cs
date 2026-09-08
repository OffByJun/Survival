using System;

namespace AstraNope.Data.Entities
{
    public interface IDestroyable
    {
        void Destroy();
    }

    [Obsolete("Use IDestroyable. This interface is kept for source compatibility.")]
    public interface IDestructable : IDestroyable { }
}
