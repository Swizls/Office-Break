using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        private const float WALKING_SPEED = 3.5f;
        private const float RUNNING_SPEED = 6f;
        private const float MIN_DISTANCE_TO_START_RUN = 4f;

        private NavMeshAgent _agent;

        public Vector3 AgentVelocity => _agent.velocity;
        public float RemainingDistance => _agent.remainingDistance;
        public bool IsRunning => _agent.speed > WALKING_SPEED;

        private void Awake() => _agent = GetComponent<NavMeshAgent>();

        private void OnDisable() => _agent.isStopped = true;

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
            if (_agent.remainingDistance > MIN_DISTANCE_TO_START_RUN)
                _agent.speed = RUNNING_SPEED;
            else
                _agent.speed = WALKING_SPEED;
        }
    }
}