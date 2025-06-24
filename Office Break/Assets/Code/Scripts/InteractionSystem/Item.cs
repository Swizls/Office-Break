using OfficeBreak.Core;
using UnityEngine;

namespace OfficeBreak.InteractionSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _throwingDamageMultiplier = 3f;

        private IHitable _hitable;
        private bool _isThrown = false;

        [field: SerializeField] public bool IsThrowable { get; private set; }
        [field: SerializeField] public bool IsWeapon { get; private set; }
        [field: SerializeField] public bool IsDualHandItem { get; private set; }

        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            _hitable = GetComponent<IHitable>();
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_isThrown)
                return;

            _isThrown = false;

            HitData hitData = new HitData()
            {
                Damage = Rigidbody.linearVelocity.magnitude * _throwingDamageMultiplier,
                HitDirection = Rigidbody.linearVelocity.normalized,
                AttackDirection = HitData.AttackDirections.Center,
                AttackForce = 500f
            };

            _hitable.TakeHit(hitData);

            IHitable otherHitable = collision.gameObject.GetComponentInParent<IHitable>();

            otherHitable?.TakeHit(hitData);
        }

        public void Throw(Vector3 force)
        {
            _isThrown = true;
            Rigidbody.linearVelocity = force;
            Rigidbody.angularVelocity = Vector3.one * Random.Range(-10, 10);
        }

        public InteractionStrategy Interact(Interactor interactor) => new PickupStrategy(interactor, this);
    }
}
