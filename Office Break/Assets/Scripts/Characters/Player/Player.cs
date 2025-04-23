using OfficeBreak.Core.DamageSystem;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class Player : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;

        public Health Health => _health;

        private void Awake() => _health.Initialize();

        public void TakeHit(HitData hitData) => _health.TakeDamage(hitData.Damage);
    }
}