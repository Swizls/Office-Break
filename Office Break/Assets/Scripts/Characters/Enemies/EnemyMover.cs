using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        private const float MIN_DISTANCE_TO_START_RUN = 4f;

        [SerializeField] private float _walkingSpeed = 3.5f;
        [SerializeField] private float _runningSpeed = 8f;

        private NavMeshAgent _agent;

        public Vector3 AgentVelocity => _agent.velocity;
        public float RemainingDistance => _agent.remainingDistance;
        public bool IsRunning => _agent.speed > _walkingSpeed;

        private void Awake() => _agent = GetComponent<NavMeshAgent>();

        private void OnDisable() => _agent.isStopped = true;

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
            if (_agent.remainingDistance > MIN_DISTANCE_TO_START_RUN)
                _agent.speed = _runningSpeed;
            else
                _agent.speed = _walkingSpeed;
        }
    }
}