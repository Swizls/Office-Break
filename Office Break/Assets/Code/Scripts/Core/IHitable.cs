using System;
using UnityEngine;

namespace OfficeBreak.Core
{
    public interface IHitable
    {
        public event Action<IHitable> GotHit;
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

        public float Damage;
        public Vector3 HitDirection;
        public AttackDirections AttackDirection;
        public float AttackForce;
    }
}