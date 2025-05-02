using FabroGames.PlayerControlls;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace FabroGames.Player.Movement.EditorExtension 
{
    [CustomEditor(typeof(FPSMovement))]
    public class PlayerMovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            FPSMovement playerMovement = (FPSMovement)target;

            GUIStyle succesfulStyle = new GUIStyle { normal = { textColor = Color.green } };
            GUIStyle unsuccefulStyle = new GUIStyle { normal = { textColor = Color.red } };

            EditorGUILayout.LabelField($"Current Mover: {playerMovement.CurrentMover.GetType().Name}");
            EditorGUILayout.LabelField($"Velocity: {playerMovement.Velocity}");
            EditorGUILayout.LabelField($"Velocity magnitude: {playerMovement.Velocity.magnitude}");
            EditorGUILayout.LabelField($"Velocity normalized: {playerMovement.Velocity.normalized}");
            EditorGUILayout.LabelField($"Movement Direction: {playerMovement.CurrentMover.MovementDirection}");
            EditorGUILayout.LabelField($"IsMoving: {playerMovement.IsMoving}", playerMovement.IsMoving? succesfulStyle : unsuccefulStyle);
            EditorGUILayout.LabelField($"IsGrounded: {playerMovement.IsGrounded}", playerMovement.IsGrounded ? succesfulStyle : unsuccefulStyle);
            EditorGUILayout.LabelField($"IsFlying: {playerMovement.IsFlying}", playerMovement.IsFlying ? succesfulStyle : unsuccefulStyle);
            EditorGUILayout.LabelField($"IsSliding: {playerMovement.IsSliding}", playerMovement.IsSliding ? succesfulStyle : unsuccefulStyle);
        }
    }

}
#endif