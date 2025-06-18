using UnityEngine;

namespace OfficeBreak.Core.Configs
{
    [CreateAssetMenu(fileName = "Difficulty Config", menuName = "Game/Difficulty")]
    public class DifficultyConfig : ScriptableObject
    {
        [field: SerializeField] public float EnemySpawnDelay { get; private set; }
        [field: SerializeField] public int MaxAttackingEnemies { get; private set; }
        [field: SerializeField] public int MaxEnemySpawnCountPerWave { get; private set; }
    }
}