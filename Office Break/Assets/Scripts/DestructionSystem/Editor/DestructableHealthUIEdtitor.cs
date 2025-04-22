using OfficeBreak.DustructionSystem.UI;
using UnityEditor;
using UnityEngine;

namespace OfficeBreak.DustructionSystem.Editors
{
    [CustomEditor(typeof(DestructableHealthUI))]
    public class DestructableHealthUIEdtitor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            DestructableHealthUI destructableHealthUI = (DestructableHealthUI)target;

            if (GUILayout.Button("Play Shake Animation"))
                destructableHealthUI.StartShakeAnimation();
        }
    }
}