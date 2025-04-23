using FabroGames.PlayerControlls;
using OfficeBreak.Core.DamageSystem;
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

        private void OnDrawGizmos() => Gizmos.DrawSphere(AttackPosition, ATTACK_SPHERE_RADIUS);

        #endregion

        #region CALLBACKS
        private void OnAttackButtonPress(InputAction.CallbackContext context) => PrimaryAttack();

        private void OnAltAttackButtonPress(InputAction.CallbackContext context) => AlternativeAttack();

        private void OnBlockButtonPress(InputAction.CallbackContext context) => ToggleBlock();

        #endregion

        private void FistAttack()
        {
            Physics.SphereCast(AttackPosition, ATTACK_SPHERE_RADIUS, Camera.main.transform.forward, out RaycastHit hit, AttackRange, _hitablesLayer);

            if (hit.collider == null)
                return;

            Debug.Log(hit.collider.gameObject.name);

            if (!hit.collider.gameObject.TryGetComponent(out IHitable target))
                return;

            HitData data = new HitData
            {
                Damage = Damage,
                HitDirection = Camera.main.transform.forward,
                AttackForce = AttackForce
            };

            target.TakeHit(data);

            PlayAttackSFX();
        }

        private void ToggleBlock()
        {
            IsBlocking = !IsBlocking;
            BlockStateChanged?.Invoke(IsBlocking);
        }

        protected override void PrimaryAttack()
        {
            if (!IsAbleToAttackLeftHand || IsBlocking)
                return;

            StartCoroutine(CooldownTimer(AttackType.LeftHand, LeftHandCooldownTime));

            AttackPerformed?.Invoke();
            FistAttack();
        }

        protected override void AlternativeAttack()
        {
            if (!IsAbleToAttackRightHand || IsBlocking)
                return;

            StartCoroutine(CooldownTimer(AttackType.RightHand, RightHandCooldownTime));

            AlternativeAttackPerformed?.Invoke();
            FistAttack();
        }
    }
}