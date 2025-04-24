#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace OfficeBreak.Characters.EditorExtensions
{
    [CustomEditor(typeof(CameraShaker))]
    public class CameraShakerEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            CameraShaker shaker = (CameraShaker)target;

            if (GUILayout.Button("Shake"))
                shaker.StartShake();
        }
    }

}
#endif