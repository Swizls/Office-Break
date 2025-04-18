using UnityEngine;

namespace OfficeBreak
{
    public class SpineIK : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _yOffset;

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(_targetTransform.localEulerAngles.x, _targetTransform.localEulerAngles.y + _yOffset, _targetTransform.localEulerAngles.z);
        }
    }
}
