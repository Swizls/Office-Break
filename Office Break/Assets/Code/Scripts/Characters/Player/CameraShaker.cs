using System.Collections;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _rotationX;
        [SerializeField] private AnimationCurve _rotationY;
        [SerializeField] private float _shakeSpeed;

        private Coroutine _shakingCoroutine;

        public void StartShake()
        {
            if(_shakingCoroutine == null )
                _shakingCoroutine = StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            float progress = 0;
            float animationLength = _rotationX.keys[_rotationX.length - 1].time > _rotationY.keys[_rotationY.length - 1].time ? _rotationX.keys[_rotationX.length - 1].time : _rotationY.keys[_rotationY.length - 1].time;

            while (progress < animationLength)
            {
                float shakeX = _rotationX.Evaluate(progress);
                float shakeY = _rotationY.Evaluate(progress);
                Quaternion position = Quaternion.Euler(shakeX, shakeY, transform.localRotation.z);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, position, _shakeSpeed * Time.deltaTime);;
                progress += Time.deltaTime;
                yield return null;
            }

            _shakingCoroutine = null;
        }
    }
}
