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
            AttackPerformed?.Invoke();
            IsAbleToAttackRightHand = false;
            IsAbleToAttackLeftHand = false;
            _animatorController.AttackAnimationEnded += OnLeftAttackCooldownEnd;
        }

        protected override void AlternativeAttack()
        {
            AlternativeAttackPerformed?.Invoke();
            IsAbleToAttackLeftHand = false;
            IsAbleToAttackRightHand = false;
            _animatorController.AttackAnimationEnded += OnRightAttackCooldownEnd;
        }

        protected override void FistAttack()
        {
            _player.TakeHit(HitData);
        }

        public void PerformAttack()
        {
            if (!IsAbleToAttack)
                return;

            bool randomAttack = Random.Range(0, 2) == 0;

            if (randomAttack && IsAbleToAttackLeftHand)
                PrimaryAttack();
            else if (IsAbleToAttackRightHand)
                AlternativeAttack();
        }
    }
}