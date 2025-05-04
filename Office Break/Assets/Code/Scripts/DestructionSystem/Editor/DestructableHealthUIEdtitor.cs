using OfficeBreak.DestructionSystem.UI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace OfficeBreak.DestructionSystem.Editors
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
#endif