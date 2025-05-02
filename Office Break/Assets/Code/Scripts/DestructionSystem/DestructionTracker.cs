using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OfficeBreak.DustructionSystem
{
    public class DestructionTracker : MonoBehaviour
    {
        private List<Destructable> _destructableObjects = new List<Destructable>();
        private int _destructablesStartCount = 0;
        private int _destroyedObjectsCount = 0;

        public Action DestructablesUpdated;
        public Action LevelDestroyed;

        public IReadOnlyList<Destructable> Destructables => _destructableObjects;
        public float DestructionLevelByPercent => (float)_destroyedObjectsCount / _destructablesStartCount * 100;

        public void Initialzie()
        {
            _destructableObjects = FindObjectsByType<Destructable>(FindObjectsSortMode.None).ToList();

            _destructablesStartCount = _destructableObjects.Count;

            foreach (Destructable destructable in _destructableObjects)
            {
                destructable.Destroyed += () =>
                {
                    UpdateAvailableDestructableObjects();
                    DestructablesUpdated?.Invoke();

                    if (DestructionLevelByPercent == 100)
                        LevelDestroyed?.Invoke();
                };
            }
        }

        private void UpdateAvailableDestructableObjects()
        {
            Destructable[] destroyedObjects = _destructableObjects.Where(destructable => destructable.IsDestroyed).ToArray();
            foreach (var item in destroyedObjects)
                _destructableObjects.Remove(item);

            _destroyedObjectsCount++;
        }
    }
}