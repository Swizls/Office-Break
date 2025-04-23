using OfficeBreak.Core.DamageSystem;
using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem
{
    public class EnemyAttackController : AttackController
    {
        private Player _player;
        private PlayerAttackController _playerAttackcontroller;

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

        public void Initialize(Player player)
        {
            _player = player;
            _playerAttackcontroller = _player.GetComponent<PlayerAttackController>();
        }

        private void DealDamage()
        {
            if (_playerAttackcontroller.IsBlocking)
                return;

            _player.TakeHit(HitData);
        }

        protected override void PrimaryAttack()
        {
            AlternativeAttackPerformed?.Invoke();
            DealDamage();
            StartCoroutine(CooldownTimer(AttackType.LeftHand, LeftHandCooldownTime));
            PlayAttackSFX();
        }

        protected override void AlternativeAttack()
        {
            AttackPerformed?.Invoke();
            DealDamage();
            StartCoroutine(CooldownTimer(AttackType.LeftHand, RightHandCooldownTime));
            PlayAttackSFX();
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