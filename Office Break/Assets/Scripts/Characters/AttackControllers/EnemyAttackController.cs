using OfficeBreak.Core.DamageSystem;
using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem
{
    public class EnemyAttackController : AttackController
    {
        private Player _player;

        private HitData HitData 
        {
            get
            {
                return new HitData()
                {
                    Damage = Damage,
                    HitDirection = Vector3.zero,
                    AttackForce = AttackForce,
                };
            }
        }

        public override bool IsBlocking { get ; protected set; }

        public void Initialize(Player player) => _player = player;

        protected override void PrimaryAttack()
        {
            AlternativeAttackPerformed?.Invoke();
            _player.TakeHit(HitData);
            StartCoroutine(CooldownTimer(AttackType.LeftHand, LeftHandCooldownTime));
        }

        protected override void AlternativeAttack()
        {
            AttackPerformed?.Invoke();
            _player.TakeHit(HitData);
            StartCoroutine(CooldownTimer(AttackType.LeftHand, RightHandCooldownTime));
        }

        public void PerformAttack()
        {
            if (!IsAbleToAttack)
                return;

            bool randomAttack = Random.Range(0, 2) == 0;

            if(randomAttack && IsAbleToAttackLeftHand) 
                PrimaryAttack();
            else if(IsAbleToAttackRightHand)
                AlternativeAttack();
        }
    }
}