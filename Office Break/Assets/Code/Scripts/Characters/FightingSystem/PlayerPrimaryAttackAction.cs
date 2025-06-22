using OfficeBreak.Characters.Animations;
using OfficeBreak.Core;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace OfficeBreak.Characters.FightingSystem
{
    [CreateAssetMenu(fileName = "Fist Attack Action", menuName = "Attack Actions/Fist Attack")]
    public class PlayerPrimaryAttackAction : PlayerAttackAction
    {
        private const float ATTACK_SPHERE_RADIUS = 0.3f;
        private const float ATTACK_RANGE = 1.5f;

        private Transform _cameraTransform;
        private AnimatorController _animatorController;

        public override void Initialize(GameObject player)
        {
            _cameraTransform = Camera.main.transform;
            _animatorController = player.GetComponentInChildren<AnimatorController>();
        }

        public override void PerformAction()
        {
            _animatorController.PlayAnimation(AnimationClip);

            _animatorController.AttackAnimationEnded += OnAttackAnimationEnd;
        }

        private void OnAttackAnimationEnd()
        {
            _animatorController.AttackAnimationEnded -= OnAttackAnimationEnd;

            Physics.SphereCast(_cameraTransform.position, ATTACK_SPHERE_RADIUS, Camera.main.transform.forward, out RaycastHit hit, ATTACK_RANGE, HitablesLayer);

            if (hit.collider == null)
                return;

            IHitable target = hit.collider.gameObject.GetComponentInParent<IHitable>();

            if (target == null)
                return;

            HitData data = new HitData
            {
                Damage = AttackDamage,
                HitDirection = _cameraTransform.forward,
                AttackForce = AttackForce
            };

            target.TakeHit(data);
        }
    }
}