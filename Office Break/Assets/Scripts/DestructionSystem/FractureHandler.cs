using UnityEngine;
using LibreFracture;
using System.Collections.Generic;
using System;

namespace OfficeBreak
{
    [Serializable]
    public static class FractureHandler
    {
        public static GameObject Fracture(GameObject target, int pieceCount = 5, Material insideMaterial = null)
        {
            VoronoiParameters voronoiParameters = new VoronoiParameters();
            voronoiParameters.insideMaterial = insideMaterial;
            voronoiParameters.jointBreakForce = 0f;
            voronoiParameters.totalChunks = pieceCount;
            GameObject fracturedObject = LibreFracture.LibreFracture.CreateFracturedCopyOf(target, voronoiParameters);

            fracturedObject.GetComponent<Rigidbody>().isKinematic = true;

            List<Rigidbody> rigidbodies = new List<Rigidbody>();
            fracturedObject.GetComponentsInChildren(rigidbodies);
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }

            return fracturedObject;
        }
    }
}