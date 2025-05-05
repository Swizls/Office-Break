using UnityEngine;

namespace FabroGames.Helpers 
{ 
    public static class Helpers
    {
        public static Vector2 CalculateRelativeVector(Vector3 vectorA, Vector3 vectorB)
        {
            if (vectorA.magnitude == 0)
                return Vector2.zero;

            float dotProduct = Vector3.Dot(vectorA.normalized, vectorB);
            float crossProductMagnitude = Vector3.Cross(vectorA, vectorB).y;
            float clockwiseAngle = Mathf.Atan2(crossProductMagnitude, dotProduct) * Mathf.Rad2Deg;

            clockwiseAngle = (clockwiseAngle + 360) % 360;

            float radians = clockwiseAngle * Mathf.Deg2Rad;

            float x = Mathf.Sin(radians);
            float y = Mathf.Cos(radians);

            return new Vector2(x, y);
        }
    }
}