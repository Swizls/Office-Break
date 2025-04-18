using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        private NavMeshAgent _agent;

        public Vector3 AgentVelocity => _agent.velocity;
        public float RemainingDistance => _agent.remainingDistance;

        private void Awake() => _agent = GetComponent<NavMeshAgent>();

        public void SetDestination(Vector3 destination) => _agent.SetDestination(destination);
    }
}