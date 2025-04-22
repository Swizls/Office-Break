using OfficeBreak.Core.DamageSystem;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Health _health;

        public Health Health => _health;

        private void Awake() => _health.Initialize();
    }
}