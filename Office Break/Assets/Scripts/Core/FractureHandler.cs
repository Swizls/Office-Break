using UnityEngine;
using LibreFracture;
using System.Collections.Generic;
using System;

namespace OfficeBreak
{
    [Serializable]
    public static class FractureHandler
    {
        private const int PIECE_COUNT = 10;

        public static void Fracture(GameObject target, Material insideMaterial = null)
        {
            VoronoiParameters voronoiParameters = new VoronoiParameters();
            voronoiParameters.insideMaterial = insideMaterial;
            voronoiParameters.jointBreakForce = 5f;
            voronoiParameters.totalChunks = PIECE_COUNT;
            GameObject fracturedObject = LibreFracture.LibreFracture.CreateFracturedCopyOf(target, voronoiParameters);

            List<Rigidbody> rigidbodies = new List<Rigidbody>();
            fracturedObject.GetComponentsInChildren(rigidbodies);
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }
    }
}