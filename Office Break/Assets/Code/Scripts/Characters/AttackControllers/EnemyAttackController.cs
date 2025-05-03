using OfficeBreak.Core.DamageSystem;
using UnityEngine;
using static OfficeBreak.Core.DamageSystem.HitData;

namespace OfficeBreak.Characters.FightingSystem
{
    public class EnemyAttackController : AttackController
    {
        private const float MIN_COOLDOWN_TIME = 0.5f;
        private const float MAX_COOLDOWN_TIME = 3f;

        private Player _player;
        private AttackDirections _attackDirection;
        private float _cooldownTime = 0f;

        public override bool IsBlocking { get ; protected set; }

        private void Update() => UpdateCooldownTime();

        public void Initialize(Player player) => _player = player;

        protected override void PrimaryAttack()
        {
            AttackPerformed?.Invoke();
            _attackDirection = AttackDirections.Right;
            IsAbleToAttackRightHand = false;
            IsAbleToAttackLeftHand = false;
            _animatorController.AttackAnimationEnded += OnLeftAttackCooldownEnd;
        }

        protected override void AlternativeAttack()
        {
            AlternativeAttackPerformed?.Invoke();
            _attackDirection = AttackDirections.Left;
            IsAbleToAttackLeftHand = false;
            IsAbleToAttackRightHand = false;
            _animatorController.AttackAnimationEnded += OnRightAttackCooldownEnd;
        }

        protected override void FistAttack()
        {
            HitData data = new HitData()
            {
                Damage = Damage,
                HitDirection = (_player.transform.position - transform.position).normalized,
                AttackDirection = _attackDirection,
                AttackForce = AttackForce,
            };
            _player.TakeHit(data);
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

            PlayAttackSFX();
        }

        private void UpdateCooldownTime()
        {
            if (_cooldownTime > 0)
                _cooldownTime -= Time.deltaTime;
        }
    }
}