using OfficeBreak.Characters;
using System;
using UnityEngine;

namespace OfficeBreak.Core 
{
    [RequireComponent(typeof(BoxCollider))]
    public class ExitTrigger : MonoBehaviour
    {
        private const string PLAYER_LAYER_MASK = "Player";

        private ElevatorDoors _elevator;
        private BoxCollider _collider;

        public event Action PlayerEnteredExit;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _elevator = GetComponentInParent<ElevatorDoors>();

            _collider.includeLayers = LayerMask.GetMask(PLAYER_LAYER_MASK);
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Player>() == null)
                return;

            other.transform.parent = _elevator.transform;
            PlayerEnteredExit?.Invoke();
        }
    }
}