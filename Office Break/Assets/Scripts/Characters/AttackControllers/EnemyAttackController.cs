using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem
{
    public class EnemyAttackController : AttackController
    {
        private Health _playerHealth;

        public override bool IsBlocking { get ; protected set; }

        protected override void PrimaryAttack()
        {
            AlternativeAttackPerformed?.Invoke();
            _playerHealth.TakeDamage(Damage);
            StartCoroutine(CooldownTimer(AttackType.LeftHand, LeftHandCooldownTime));
            PlayAttackSFX();
        }

        protected override void AlternativeAttack()
        {
            AttackPerformed?.Invoke();
            _playerHealth.TakeDamage(Damage);
            StartCoroutine(CooldownTimer(AttackType.LeftHand, RightHandCooldownTime));
            PlayAttackSFX();
        }

        public void Initialize(Health playerHealth) => _playerHealth = playerHealth;

        public void PerformAttack()
        {
            if (!IsAbleToAttackRightHand && !IsAbleToAttackRightHand)
                return;

            bool randomAttack = Random.Range(0, 2) == 0;

            if(randomAttack && IsAbleToAttackLeftHand) 
                PrimaryAttack();
            else if(IsAbleToAttackRightHand)
                AlternativeAttack();
        }
    }
}