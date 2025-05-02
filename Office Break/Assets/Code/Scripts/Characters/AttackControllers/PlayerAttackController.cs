using FabroGames.PlayerControlls;
using OfficeBreak.Core.DamageSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.Characters.FightingSystem
{
    public class PlayerAttackController : AttackController
    {
        private const float ATTACK_SPHERE_RADIUS = 0.3f;

        [SerializeField] private LayerMask _hitablesLayer;

        private PlayerInputActions _playerInputActions;

        private Vector3 AttackPosition => Camera.main.transform.position;

        public override bool IsBlocking { get ; protected set; }

        #region MONO

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Attack.performed += OnAttackButtonPress;
            _playerInputActions.Player.AlternativeAttack.performed += OnAltAttackButtonPress;
            _playerInputActions.Player.Block.performed += OnBlockButtonPress;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Attack.performed -= OnAttackButtonPress;
            _playerInputActions.Player.AlternativeAttack.performed -= OnAltAttackButtonPress;
            _playerInputActions.Player.Block.performed -= OnBlockButtonPress;
            _playerInputActions.Player.Disable();
        }

        #endregion

        #region CALLBACKS
        private void OnAttackButtonPress(InputAction.CallbackContext context) => PrimaryAttack();

        private void OnAltAttackButtonPress(InputAction.CallbackContext context) => AlternativeAttack();

        private void OnBlockButtonPress(InputAction.CallbackContext context)
        {
            StartCoroutine(HoldBlock());
        }

        #endregion

        protected override void FistAttack()
        {
            Physics.SphereCast(AttackPosition, ATTACK_SPHERE_RADIUS, Camera.main.transform.forward, out RaycastHit hit, AttackRange, _hitablesLayer);

            if (hit.collider == null)
                return;

            IHitable target = hit.collider.gameObject.GetComponentInParent<IHitable>();

            if (target == null)
                return;

            HitData data = new HitData
            {
                Damage = Damage,
                HitDirection = Camera.main.transform.forward,
                AttackForce = AttackForce
            };

            target.TakeHit(data);
        }

        private IEnumerator HoldBlock()
        {
            yield return new WaitUntil(() => IsAbleToAttackLeftHand && IsAbleToAttackRightHand);

            IsBlocking = true;
            BlockStateChanged?.Invoke(IsBlocking);

            yield return new WaitUntil(() => _playerInputActions.Player.Block.IsPressed() == false);

            IsBlocking = false;
            BlockStateChanged?.Invoke(IsBlocking);
        }

        protected override void PrimaryAttack()
        {
            if (!IsAbleToAttackLeftHand || IsBlocking)
                return;

            IsAbleToAttackRightHand = false;
            IsAbleToAttackLeftHand = false;
            _animatorController.AttackAnimationEnded += OnLeftAttackCooldownEnd;

            AttackPerformed?.Invoke();
        }

        protected override void AlternativeAttack()
        {
            if (!IsAbleToAttackRightHand || IsBlocking)
                return;

            IsAbleToAttackLeftHand = false;
            IsAbleToAttackRightHand = false;
            _animatorController.AttackAnimationEnded += OnRightAttackCooldownEnd;

            AlternativeAttackPerformed?.Invoke();
        }
    }
}