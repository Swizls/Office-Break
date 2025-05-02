#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI.EditorExtensions
{
    [CustomEditor(typeof(EnemiesController))]
    public class EnemiesControllerEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            EnemiesController enemiesController = (EnemiesController)target;

            if (!Application.isPlaying)
                return;

            GUILayout.Label($"Total enemies count: {enemiesController.FollowingEnemiesCount + enemiesController.AttackingEnemiesCount}");
            GUILayout.Label($"Following enemies count: {enemiesController.FollowingEnemiesCount}");
            GUILayout.Label($"Attacking enemies count: {enemiesController.AttackingEnemiesCount}");
        }
    }
}
#endif