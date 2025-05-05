using FabroGames.Helpers;
using FabroGames.PlayerControlls;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class CameraSway : MonoBehaviour
    {
        private const float SWAY_SPEED = 5f;

        [SerializeField] private float _swayStrength = 5f;
        [SerializeField] private bool _useForwardSway = true;
        [SerializeField] private bool _useSideSway = true;

        private IMovable _movable;
        private Transform _cameraTransform;

        private void Awake()
        {
            _movable = GetComponentInParent<IMovable>();
            _cameraTransform = Camera.main.transform;
        }

        private void Update() => Sway();

        private void Sway()
        {
            Vector2 realativeMovement = Helpers.CalculateRelativeVector(_movable.Velocity, _cameraTransform.forward).normalized;

            float forwardSwayValue = _useForwardSway ? realativeMovement.y * _swayStrength : 0;
            float sideSwayValue = _useSideSway ? realativeMovement.x * _swayStrength : 0;

            Quaternion targetRotation = Quaternion.Euler(forwardSwayValue, 0, sideSwayValue);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, SWAY_SPEED * Time.deltaTime);
        }
    }
}