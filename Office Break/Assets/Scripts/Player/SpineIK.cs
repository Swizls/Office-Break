using UnityEngine;

namespace OfficeBreak
{
    public class SpineIK : MonoBehaviour
    {
        [SerializeField] private Transform _playerCameraTransform;
        [SerializeField] private float _yOffset;

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(_playerCameraTransform.localEulerAngles.x, _playerCameraTransform.localEulerAngles.y + _yOffset, _playerCameraTransform.localEulerAngles.z);
        }
    }
}
