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
            Start,
            Exit
        }

        private const string IS_OPEN = "isOpen";
        private const float OPEN_TIMER = 5f;

        [SerializeField] private ElevatorType _type;

        private Animator _animator;
        private AudioSource _audioSource;

        public ElevatorType Type => _type;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();

            if (_type == ElevatorType.Start)
                Open();
        }

        private IEnumerator KeepDoorsOpen()
        {
            yield return new WaitForSeconds(OPEN_TIMER);
            CloseDoors();
        }

        public void OpenAndCloseAfterDelay() 
        {
            _animator.SetBool(IS_OPEN, true);
            StartCoroutine(KeepDoorsOpen());
            _audioSource.Play();
        }

        public void Open()
        {
            _animator.SetBool(IS_OPEN, true);
            _audioSource.Play();
        }

        public void CloseDoors() => _animator.SetBool(IS_OPEN, false);
    }
}