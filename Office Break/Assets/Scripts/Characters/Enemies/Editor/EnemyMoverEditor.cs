using OfficeBreak.Characters.Enemies;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace OfficeBreak.Enemies.EditorExtension
{
    [CustomEditor(typeof(EnemyMover))]
    public class EnemyMoverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            EnemyMover enemyMover = (EnemyMover)target;

            GUIStyle succesfulStyle = new GUIStyle { normal = { textColor = Color.green } };
            GUIStyle unsuccefulStyle = new GUIStyle { normal = { textColor = Color.red } };

            EditorGUILayout.LabelField($"Velocity: {enemyMover.Velocity}");
            EditorGUILayout.LabelField($"Velocity normalized: {enemyMover.Velocity.normalized}");
            EditorGUILayout.LabelField($"Speed: {enemyMover.Velocity.magnitude}");
            EditorGUILayout.LabelField($"Remaining distance to target: {enemyMover.RemainingDistance}");
            EditorGUILayout.LabelField($"Is Running: {enemyMover.IsRunning}", enemyMover.IsRunning ? succesfulStyle : unsuccefulStyle);
        }
    }

}
#endif