using System;
using AstraNope.Core.World.Water;

namespace AstraNope.Mechanics.Player
{
    /// <summary>Legacy serialized alias. New scenes should use LakeWaterBody.</summary>
    [Obsolete("Use LakeWaterBody for water volumes.")]
    public class WaterVolume : LakeWaterBody
    {
    }
}
