using UnityEngine;

namespace OfficeBreak.Characters
{
    public class DeathCamera : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _circleRadius;
        [SerializeField] private float _floatingHeight;
        [SerializeField] private float _floatingSpeed;

        private float _rotationAngle = 0;

        private void Awake() => enabled = false;


        private void Update() => RotateAroundPlayer();

        private void RotateAroundPlayer()
        {
            _rotationAngle += Time.deltaTime;

            transform.LookAt(_playerTransform);

            float x = _playerTransform.position.x + _circleRadius * Mathf.Cos(_rotationAngle);
            float z = _playerTransform.position.z + _circleRadius * Mathf.Sin(_rotationAngle);
            Vector3 calculatedPos = new Vector3(x, _floatingHeight, z);
            transform.position = Vector3.Lerp(transform.position, calculatedPos, _floatingSpeed);
        }
    }
}