using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _playerTransform;

        public Vector3 AgentVelocity => _agent.velocity;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _playerTransform.position) > 2f)
                FollowPlayer();
            else
                _agent.isStopped = true;
        }

        private void FollowPlayer()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_playerTransform.position);
        }

        public void Initialize(Transform playerTransform) => _playerTransform = playerTransform;
    }
}