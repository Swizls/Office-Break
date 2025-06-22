using OfficeBreak.Core;
using UnityEditor;
using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem.EditorExtension
{
    [CustomEditor(typeof(PlayerAttackController))]
    public class PlayerAttackControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            PlayerAttackController attackController = (PlayerAttackController)target;

            string lastPressedActionName = attackController.LastPressedAction != null ? attackController.LastPressedAction.name : "null";

            GUILayout.Label($"Is Able to Attack: {attackController.IsAbleToAttack}");
            GUILayout.Label($"Last Pressed Action: {lastPressedActionName}");
        }
    }
}