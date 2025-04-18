using UnityEditor;
using UnityEngine;

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

            EditorGUILayout.LabelField($"Velocity: {enemyMover.AgentVelocity}");
            EditorGUILayout.LabelField($"Velocity normalized: {enemyMover.AgentVelocity.normalized}");
        }
    }

}
