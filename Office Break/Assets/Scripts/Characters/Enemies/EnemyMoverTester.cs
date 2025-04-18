using OfficeBreak.Characters.Enemies;
using System;
using System.Collections;
using UnityEngine;

namespace OfficeBreak.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyMoverTester : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private bool _followPlayer;

        private Transform _playerTransform;
        private EnemyMover _enemyMover;
        private Coroutine _movementRoutine;

        private void Awake() => _enemyMover = GetComponent<EnemyMover>();

        private void Update()
        {
            if (_followPlayer)
                _enemyMover.SetDestination(_playerTransform.position);
            else if (_movementRoutine == null)
                _movementRoutine = StartCoroutine(MoveToPoint());
        }

        private IEnumerator MoveToPoint()
        {
            Vector3 point = _points[UnityEngine.Random.Range(0, _points.Length)].position;
            _enemyMover.SetDestination(point);
            yield return new WaitUntil(() => _enemyMover.RemainingDistance < 0.1f);
            _movementRoutine = null;
        }

        public void Initialize(Transform playerTransform, Transform[] points)
        {
            _playerTransform = playerTransform;
            _points = points;
        }
    }
}