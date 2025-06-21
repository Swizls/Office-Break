using UnityEngine;

namespace OfficeBreak
{
    public class SpineIK : MonoBehaviour
    {
        private enum Direction { X, Y, Z }

        [SerializeField] private Direction _direction;
        [SerializeField] private Transform _targetTransform;

        [SerializeField] private float _xOffset;
        [SerializeField] private float _yOffset;
        [SerializeField] private float _zOffset;

        [SerializeField] private bool _useInvertedValue = false;

        private void LateUpdate()
        {
            float primaryDirectionAngle = _useInvertedValue ? -_targetTransform.eulerAngles.x : _targetTransform.eulerAngles.x;

            float x = transform.eulerAngles.x;
            float y = transform.eulerAngles.y;
            float z = transform.eulerAngles.z;

            switch (_direction)
            {
                case Direction.Y:
                    y = primaryDirectionAngle;
                    break;
                case Direction.Z:
                    z = primaryDirectionAngle; 
                    break;
                default:
                    x = primaryDirectionAngle;
                    break;
            }

            transform.rotation = Quaternion.Euler(x + _xOffset, y + _yOffset, z + _zOffset);
        }
    }
}
