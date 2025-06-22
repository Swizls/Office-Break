using FabroGames.PlayerControlls;
using OfficeBreak.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.Characters.FightingSystem
{
    public class PlayerAttackController : AttackController
    {
        public const float BLOCKING_ANGLE = 120f;

        [SerializeField] private List<PlayerAttackAction> _attackActions;

        private PlayerInputActions _playerInputActions;
        private PlayerAttackAction _lastPressedAction;
        private Transform _cameraTransform;

        public override bool IsBlocking { get ; protected set; }

        public PlayerAttackAction LastPressedAction => _lastPressedAction;

        #region MONO

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
            Initialize();

            foreach(var action in _attackActions)
                action.Initialize(gameObject);
        }

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Block.performed += OnBlockButtonPress;
        }

        private void Update()
        {
            if (!IsAbleToAttack)
                return;

            foreach (var action in _attackActions)
            {
                if (action.IsRequiredKeysPressed())
                    _lastPressedAction = action;
            }
        }

        private void FixedUpdate()
        {
            if (_lastPressedAction == null)
                return;

            Debug.Log("Performing Attack Action");

            _lastPressedAction.PerformAction();
            IsAbleToAttack = false;
            _lastPressedAction = null;

            _animatorController.AttackAnimationEnded += OnAttackAnimationEnd;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Block.performed -= OnBlockButtonPress;
            _playerInputActions.Player.Disable();

            _animatorController.AttackAnimationEnded -= OnAttackAnimationEnd;
        }

        #endregion

        #region CALLBACKS

        private void OnBlockButtonPress(InputAction.CallbackContext context) => StartCoroutine(HoldBlock());

        private void OnLeftBlockButtonPress(InputAction.CallbackContext context)
        {
            if (BlockDirection == HitData.AttackDirections.Left)
                return;

            BlockDirection = HitData.AttackDirections.Left;
            BlockStateChanged?.Invoke(IsBlocking);
        }

        private void OnRightBlockButtonPress(InputAction.CallbackContext context)
        {
            if (BlockDirection == HitData.AttackDirections.Right)
                return;

            BlockDirection = HitData.AttackDirections.Right;
            BlockStateChanged?.Invoke(IsBlocking);
        }

        #endregion

        public bool IsHitBlocked(HitData hitData)
        {
            float hitAngle = Vector3.Angle(_cameraTransform.forward, hitData.HitDirection);

            if (BlockDirection != hitData.AttackDirection)
                return false;

            if (hitAngle < BLOCKING_ANGLE)
                return false;

            return true;
        }

        private IEnumerator HoldBlock()
        {
            yield return new WaitUntil(() => IsAbleToAttack);

            IsBlocking = true;
            BlockStateChanged?.Invoke(IsBlocking);

            EnableBlockInput();

            yield return new WaitUntil(() => _playerInputActions.Player.Block.IsPressed() == false);

            DisableBlockInput();

            IsBlocking = false;
            BlockDirection = HitData.AttackDirections.Center;
            BlockStateChanged?.Invoke(IsBlocking);

            void EnableBlockInput()
            {
                _playerInputActions.Player.Attack.performed += OnLeftBlockButtonPress;
                _playerInputActions.Player.AlternativeAttack.performed += OnRightBlockButtonPress;
            }

            void DisableBlockInput()
            {
                _playerInputActions.Player.Attack.performed -= OnLeftBlockButtonPress;
                _playerInputActions.Player.AlternativeAttack.performed -= OnRightBlockButtonPress;
            }
        }
    }
}