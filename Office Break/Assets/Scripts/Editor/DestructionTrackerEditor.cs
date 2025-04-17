using UnityEditor;
using UnityEngine;

namespace OfficeBreak.Core.EditorExtension 
{
    [CustomEditor(typeof(DestructionTracker))]
    public class DestructionTrackerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            DestructionTracker destructionTracker = (DestructionTracker)target;

            EditorGUILayout.LabelField("Destruction Level: " + destructionTracker.DestructionLevelByPercent + "%");
        }
    }

}
