using AstraNope.Data.Entities;
using UnityEngine;

namespace AstraNope.Data.Structures
{
    public class DoorInteractable : InteractableComponentBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string openParam = "IsOpen";

        private bool isOpen;

        public override void Interact()
        {
            isOpen = !isOpen;
            animator.SetBool(openParam, isOpen);
        }

        public override string GetLabel() => isOpen ? "Close Door" : "Open Door";
    }
}
