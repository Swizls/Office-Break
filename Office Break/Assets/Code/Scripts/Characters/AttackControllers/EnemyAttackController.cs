using OfficeBreak.Core.DamageSystem;
using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem
{
    public class EnemyAttackController : AttackController
    {
        private const float MIN_COOLDOWN_TIME = 0.5f;
        private const float MAX_COOLDOWN_TIME = 3f;

        private Player _player;
        private float _cooldownTime = 0f;

        private HitData HitData 
        {
            get
            {
                return new HitData()
                {
                    Damage = Damage,
                    HitDirection = transform.forward,
                    AttackForce = AttackForce,
                };
            }
        }

        public override bool IsBlocking { get ; protected set; }

        private void Update() => UpdateCooldownTime();

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

            if (_cooldownTime > 0)
                return;

            bool randomAttack = Random.Range(0, 2) == 0;

            _cooldownTime = UnityEngine.Random.Range(MIN_COOLDOWN_TIME, MAX_COOLDOWN_TIME);

            if (randomAttack && IsAbleToAttackLeftHand)
                PrimaryAttack();
            else if (IsAbleToAttackRightHand)
                AlternativeAttack();
        }

        private void UpdateCooldownTime()
        {
            if (_cooldownTime > 0)
                _cooldownTime -= Time.deltaTime;
        }
    }
}