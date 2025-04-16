using UnityEngine;

namespace OfficeBreak
{
    [RequireComponent (typeof(Rigidbody))]
    public class Destructable : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;

        private Rigidbody _rigidbody;
        private Collider _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            _health.Died += OnDeath;
        }

        private void OnDisable()
        {
            _health.Died -= OnDeath;
        }

        private void OnDeath()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;

            FractureHandler.Fracture(gameObject);

            Destroy(gameObject);
        }

        public void TakeHit(HitData hitData)
        {
            _health.TakeDamage(hitData.Damage);
            _rigidbody.AddForce(hitData.HitDirection * hitData.AttackForce);
        }
    }
}