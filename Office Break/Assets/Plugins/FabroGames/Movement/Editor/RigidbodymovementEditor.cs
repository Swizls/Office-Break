using FabroGames.PlayerControlls;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace FabroGames.Player.Movement.EditorExtension 
{
    [CustomEditor(typeof(RigidbodyMovement))]
    public class RigidbodyMovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            RigidbodyMovement playerMovement = (RigidbodyMovement)target;

            GUIStyle succesfulStyle = new GUIStyle { normal = { textColor = Color.green } };
            GUIStyle unsuccefulStyle = new GUIStyle { normal = { textColor = Color.red } };

            EditorGUILayout.LabelField($"Velocity: {playerMovement.Velocity}");
            EditorGUILayout.LabelField($"Velocity magnitude: {playerMovement.Velocity.magnitude}");
            EditorGUILayout.LabelField($"Velocity normalized: {playerMovement.Velocity.normalized}");
            EditorGUILayout.LabelField($"IsMoving: {playerMovement.IsMoving}", playerMovement.IsMoving? succesfulStyle : unsuccefulStyle);
            EditorGUILayout.LabelField($"IsGrounded: {playerMovement.IsGrounded}", playerMovement.IsGrounded ? succesfulStyle : unsuccefulStyle);
        }
    }

}
#endif