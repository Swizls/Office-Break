using FabroGames.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.Player 
{ 
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _attackForce;

        private PlayerInputActions _playerInputActions;

        public Action AttackPerformed;

        #region MONO
        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Attack.performed += Attack;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Attack.performed -= Attack;
            _playerInputActions.Player.Disable();
        }

        #endregion

        private void Attack(InputAction.CallbackContext context)
        {
            AttackPerformed?.Invoke();

            Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit);

            if (hit.collider == null)
                return;

            if (hit.collider.gameObject.TryGetComponent(out IHitable target))
            {
                HitData data = new HitData
                {
                    Damage = _damage,
                    HitDirection = Camera.main.transform.forward,
                    AttackForce = _attackForce
                };

                target.TakeHit(data);
            }
        }
    }
}