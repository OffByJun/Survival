using AstraNope.Contracts;
using UnityEngine;
using VContainer.Unity;

namespace AstraNope.Managers
{
    public class GameManager : MonoBehaviour, IGameService, IInitializable
    {
        public void Initialize()
        {
            Debug.Log("GameManager Initialize");
        }
    }
}
