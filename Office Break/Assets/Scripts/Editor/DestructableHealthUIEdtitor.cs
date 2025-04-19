using OfficeBreak.Characters.Enemies;
using UnityEditor;
using UnityEngine;

namespace OfficeBreak.UI.EditorExtensions
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