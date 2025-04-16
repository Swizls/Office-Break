using OfficeBreak;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestructionTracker : MonoBehaviour
{
    private List<Destructable> _destructables = new List<Destructable>();

    public float DestructionLevelByPercent => (float)_destructables.Where(destructable => destructable.IsDestroyed).ToList().Count / _destructables.Count * 100;
    public IReadOnlyList<Destructable> Destructables => _destructables;

    public Action DestructablesUpdated;

    private void Start()
    {
        _destructables = FindObjectsByType<Destructable>(FindObjectsSortMode.None).ToList();

        foreach(Destructable destructable in _destructables)
            destructable.Destroyed += () => DestructablesUpdated?.Invoke();
    }
}
