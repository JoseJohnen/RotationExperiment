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
    }
}
