using AstraNope.Contracts;
using UnityEngine;

namespace AstraNope.Mechanics.Player
{
    public class PlayerContext : MonoBehaviour, IPlayerContext
    {
        [SerializeField] private Transform playerTrs;
        public Transform PlayerTrs => playerTrs;
    }
}
