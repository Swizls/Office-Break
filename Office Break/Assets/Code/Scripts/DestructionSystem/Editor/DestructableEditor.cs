using OfficeBreak.Core;
using UnityEditor;
using UnityEngine;

namespace OfficeBreak.DestructionSystem.Editors
{
    [CustomEditor(typeof(Destructable))]
    public class DestructableEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            Destructable destructable = (Destructable)target;

            if (GUILayout.Button("Destroy"))
            {
                HitData hitData = new HitData()
                {
                    Damage = 1000000,
                    HitDirection = Vector3.zero,
                    AttackForce = 0
                };

                destructable.TakeHit(hitData);
            }
        }
    }

}