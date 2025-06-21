using UnityEngine;
using LibreFracture;
using System.Collections.Generic;
using System;

namespace OfficeBreak.DestructionSystem
{
    [Serializable]
    public static class FractureHandler
    {
        public static GameObject Fracture(GameObject target, int pieceCount = 10, Material insideMaterial = null)
        {
            VoronoiParameters voronoiParameters = new VoronoiParameters();
            voronoiParameters.insideMaterial = insideMaterial;
            voronoiParameters.jointBreakForce = 100f;
            voronoiParameters.totalChunks = pieceCount;
            GameObject fracturedObject = LibreFracture.LibreFracture.CreateFracturedCopyOf(target, voronoiParameters);

            fracturedObject.GetComponent<Rigidbody>().isKinematic = true;

            ClearComponents(fracturedObject);

            List<Rigidbody> rigidbodies = new List<Rigidbody>();
            fracturedObject.GetComponentsInChildren(rigidbodies);
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.mass = 1f;
                rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }

            return fracturedObject;
        }
        private static void ClearComponents(GameObject obj)
        {
            DamageEffect damageEffect = obj.GetComponent<DamageEffect>();
            Destructable destructable = obj.GetComponent<Destructable>();
            Collider[] colliders = obj.GetComponents<Collider>();

            if (damageEffect != null)
                UnityEngine.Object.Destroy(damageEffect);

            if (destructable != null)
                UnityEngine.Object.Destroy(destructable);

            if (colliders != null)
            {
                foreach (Collider collider in colliders)
                {
                    UnityEngine.Object.Destroy(collider);
                }
            }
        }
    }
}