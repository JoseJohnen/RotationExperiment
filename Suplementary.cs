using System;

namespace RotationExperiment
{
    public static class Suplementary
    {
        public static float RadiansToDegrees(double radians)
        {
            //degrees
            return Convert.ToSingle(radians * (180 / Math.PI));

        }

        public static float DegreesToRadians(double degrees)
        {
            //radians
            return Convert.ToSingle(degrees * (Math.PI / 180));
        }

        // Returns the length of this vector (RO).
        public static float Magnitude(this Stride.Core.Mathematics.Vector3 v3)
        {
            return MathF.Sqrt(v3.X * v3.X + v3.Y * v3.Y + v3.Z * v3.Z);
        }
    }
}
