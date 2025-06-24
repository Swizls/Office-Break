using UnityEngine;

namespace OfficeBreak.InteractionSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool IsThrowable { get; private set; }
        [field: SerializeField] public bool IsWeapon { get; private set; }
        [field: SerializeField] public bool IsDualHandItem { get; private set; }

        public Rigidbody Rigidbody { get; private set; }

        private void Awake() => Rigidbody = GetComponent<Rigidbody>();

        public InteractionStrategy Interact(Interactor interactor) => new PickupStrategy(interactor, this);
    }
}
