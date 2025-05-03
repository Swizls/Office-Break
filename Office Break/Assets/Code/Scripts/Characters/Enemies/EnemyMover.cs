using FabroGames.PlayerControlls;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour, IMovable
    {
        private const float MIN_DISTANCE_TO_START_RUN = 4f;
        private const float BODY_ROTATION_SPEED = 10f;
        private const float RIGIDBODIES_PUSH_FORCE = 105f;

        [SerializeField] private float _walkingSpeed = 3.5f;
        [SerializeField] private float _runningSpeed = 8f;

        [SerializeField] private Transform _enemyModelTransform;

        private NavMeshAgent _agent;
        private Transform _playerTransform;

        private Coroutine _bodyRotationCoroutine;

        public Vector3 Velocity => _agent.velocity;
        public float RemainingDistance => _agent.remainingDistance;
        public bool IsRunning => _agent.speed > _walkingSpeed;
        public bool IsMoving => _agent.velocity.magnitude > 0;
        public bool IsGrounded => true;
        public bool Enabled { get => enabled; set => enabled = value; }

        #region MONO

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerTransform = FindAnyObjectByType<Player>().transform;
        }

        private void OnDisable()
        {
            _agent.enabled = false;
            if(_bodyRotationCoroutine != null)
            {
                StopCoroutine(_bodyRotationCoroutine);
                _bodyRotationCoroutine = null;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce((collision.transform.position - transform.position).normalized * RIGIDBODIES_PUSH_FORCE * Time.deltaTime);
            }
        }

        #endregion

        private IEnumerator RotateBodyToPlayerDirection()
        {
            while (!IsRunning)
            {
                Vector3 lookDirection = new Vector3(_playerTransform.position.x - transform.position.x, 0, _playerTransform.position.z - transform.position.z);
                _enemyModelTransform.rotation = Quaternion.Lerp(_enemyModelTransform.rotation, Quaternion.LookRotation(lookDirection), BODY_ROTATION_SPEED * Time.deltaTime);
                yield return null;
            }

            while(Mathf.Abs(_enemyModelTransform.localRotation.y - Quaternion.identity.y) > 0.001f)
            {
                _enemyModelTransform.localRotation = Quaternion.Lerp(_enemyModelTransform.localRotation, Quaternion.identity, BODY_ROTATION_SPEED * Time.deltaTime);
                yield return null;
            }

            _bodyRotationCoroutine = null;
        }

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
            if (_agent.remainingDistance > MIN_DISTANCE_TO_START_RUN)
            {
                _agent.speed = _runningSpeed;
            }
            else
            {
                _agent.speed = _walkingSpeed;
                if(_bodyRotationCoroutine == null)
                    _bodyRotationCoroutine = StartCoroutine(RotateBodyToPlayerDirection());
            }
        }

        public bool IsPositionReachable(Vector3 point)
        {
            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(point, path);

            return path.status == NavMeshPathStatus.PathComplete;
        }
    }
}