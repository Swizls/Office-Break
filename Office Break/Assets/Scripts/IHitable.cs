using UnityEngine;

namespace OfficeBreak
{
    public interface IHitable
    {
        public void TakeHit(HitData hitData);
    }

    public struct HitData
    {
        public float Damage;
        public Vector3 HitDirection;
        public float AttackForce;
    }
}