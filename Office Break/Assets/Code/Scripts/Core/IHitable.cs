using System;
using UnityEngine;

namespace OfficeBreak.Core
{
    public interface IHitable
    {
        public event Action<HitData> GotHit;
        public void TakeHit(HitData hitData);
    }

    public struct HitData
    {
        public enum AttackDirections
        {
            Left,
            Right,
            Center
        }

        public GameObject Sender;
        public IHitable Target;
        public float Damage;
        public Vector3 HitDirection;
        public AttackDirections AttackDirection;
        public float AttackForce;
    }
}