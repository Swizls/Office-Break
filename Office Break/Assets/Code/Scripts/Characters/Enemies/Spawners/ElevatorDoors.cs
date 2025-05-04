using System.Collections;
using UnityEngine;

namespace OfficeBreak
{
    [RequireComponent (typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class ElevatorDoors : MonoBehaviour
    {
        public enum ElevatorType
        {
            EnemySpawner,
            Exit
        }

        private const string IS_OPEN = "isOpen";
        private const float OPEN_TIMER = 5f;

        private Animator _animator;
        private AudioSource _audioSource;

        [field: SerializeField] public ElevatorType Type { get; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private IEnumerator KeepDoorsOpen()
        {
            yield return new WaitForSeconds(OPEN_TIMER);
            CloseDoors();
        }

        public void OpenDoors() 
        {
            _animator.SetBool(IS_OPEN, true);
            StartCoroutine(KeepDoorsOpen());
            _audioSource.Play();
        }

        public void CloseDoors() => _animator.SetBool(IS_OPEN, false);
    }
}