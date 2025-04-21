using System.Collections;
using UnityEngine;

namespace OfficeBreak
{
    [RequireComponent (typeof(Animator))]
    public class ElevatorDoorsController : MonoBehaviour
    {
        private const string IS_OPEN = "isOpen";
        private const float OPEN_TIMER = 5f;

        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        private IEnumerator KeepDoorsOpen()
        {
            yield return new WaitForSeconds(OPEN_TIMER);
            CloseDoors();
        }

        public void OpenDoors() 
        {
            _animator.SetBool(IS_OPEN, true);
            StartCoroutine(KeepDoorsOpen());
        }

        public void CloseDoors() => _animator.SetBool(IS_OPEN, false);
    }
}