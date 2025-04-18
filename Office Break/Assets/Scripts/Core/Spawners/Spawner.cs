using UnityEngine;

namespace OfficeBreak.Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameObject Prefab;
        public abstract void Spawn();
    }
}