using FabroGames.Characters.Animations;
using FabroGames.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.Characters.FightingSystem
{
    public class PlayerAttackController : AttackController
    {
        private enum AttackType
        {
            RightHand,
            LeftHand
        }

        private const float ATTACK_SPHERE_RADIUS = 0.3f;

        [SerializeField] private LayerMask _hitablesLayer;
        private PlayerInputActions _playerInputActions;

        private bool _isAbleToAttackLeftHand = true;        
        private bool _isAbleToAttackRightHand = true;

        private float _leftHandCooldownTime;
        private float _rightHandCooldownTime;

        private Vector3 AttackPosition => Camera.main.transform.position;

        public override bool IsBlocking { get ; protected set; }

        #region MONO

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            var animatorController = GetComponentInChildren<AnimatorController>();

            _leftHandCooldownTime = animatorController.PrimaryAttackAnimationLength;
            _rightHandCooldownTime = animatorController.SecondaryAttackAnimationLength;
        }

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Attack.performed += OnAttackButtonPress;
            _playerInputActions.Player.AlternativeAttack.performed += OnAltAttackButtonPress;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Attack.performed -= OnAttackButtonPress;
            _playerInputActions.Player.AlternativeAttack.performed -= OnAltAttackButtonPress;
            _playerInputActions.Player.Disable();
        }

        private void OnDrawGizmos() => Gizmos.DrawSphere(AttackPosition, ATTACK_SPHERE_RADIUS);

        #endregion

        #region CALLBACKS
        private void OnAttackButtonPress(InputAction.CallbackContext context) => PrimaryAttack();

        private void OnAltAttackButtonPress(InputAction.CallbackContext context) => AlternativeAttack();
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

        private IEnumerator CooldownTimer(AttackType attackType, float cooldownTimer)
        {
            switch (attackType)
            {
                case AttackType.LeftHand:
                    _isAbleToAttackLeftHand = false;
                    break;
                case AttackType.RightHand:
                    _isAbleToAttackRightHand = false;
                    break;
            }

            while(cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            switch (attackType)
            {
                case AttackType.LeftHand:
                    _isAbleToAttackLeftHand = true;
                    break;
                case AttackType.RightHand:
                    _isAbleToAttackRightHand = true;
                    break;
            }
        }

        protected override void PrimaryAttack()
        {
            if (!_isAbleToAttackLeftHand)
                return;

            StartCoroutine(CooldownTimer(AttackType.LeftHand, _leftHandCooldownTime - CooldownReductionTime));

            AttackPerformed?.Invoke();
            FistAttack();
        }

        protected override void AlternativeAttack()
        {
            if (!_isAbleToAttackRightHand)
                return;

            StartCoroutine(CooldownTimer(AttackType.RightHand, _rightHandCooldownTime - CooldownReductionTime));

            AlternativeAttackPerformed?.Invoke();
            FistAttack();
        }
    }
}