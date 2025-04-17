using FabroGames.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.Player 
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _attackForce;

        private AudioSource _audioSource;
        private PlayerInputActions _playerInputActions;

        public Action AttackPerformed;
        public Action AlternativeAttackPerformed;

        #region MONO

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Attack.performed += Attack;
            _playerInputActions.Player.AlternativeAttack.performed += AlternativeAttack;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Attack.performed -= Attack;
            _playerInputActions.Player.AlternativeAttack.performed -= AlternativeAttack;
            _playerInputActions.Player.Disable();
        }

        #endregion

        private void Attack(InputAction.CallbackContext context)
        {
            AttackPerformed?.Invoke();
            FistAttack();
        }

        private void AlternativeAttack(InputAction.CallbackContext context)
        {
            AlternativeAttackPerformed?.Invoke();
            FistAttack();
        }

        private void FistAttack()
        {
            Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit);

            if (hit.collider == null)
                return;

            if (!hit.collider.gameObject.TryGetComponent(out IHitable target))
                return;

            HitData data = new HitData
            {
                Damage = _damage,
                HitDirection = Camera.main.transform.forward,
                AttackForce = _attackForce
            };

            target.TakeHit(data);

            _audioSource.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
            _audioSource.Play();
        }
    }
}