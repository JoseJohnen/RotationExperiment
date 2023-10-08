using Stride.Core.Mathematics;

namespace RotationExperiment
{
    public class Matrix3x3
    {
        public float M11 = 0;
        public float M12 = 0;
        public float M13 = 0;
        public float M21 = 0;
        public float M22 = 0;
        public float M23 = 0;
        public float M31 = 0;
        public float M32 = 0;
        public float M33 = 0;

        public Matrix3x3()
        {
        }

        public Matrix3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
        }

        public Matrix3x3(Matrix matrix)
        {
            this.M11 = matrix.M11;
            this.M12 = matrix.M12;
            this.M13 = matrix.M13;
            this.M21 = matrix.M21;
            this.M22 = matrix.M22;
            this.M23 = matrix.M23;
            this.M31 = matrix.M31;
            this.M32 = matrix.M32;
            this.M33 = matrix.M33;
        }

        public Matrix3x3(Matrix2x2 matrix)
        {
            this.M11 = matrix.M11;
            this.M12 = matrix.M12;
            this.M13 = 0;
            this.M21 = matrix.M21;
            this.M22 = matrix.M22;
            this.M23 = 0;
            this.M31 = 0;
            this.M32 = 0;
            this.M33 = 1;
        }

        public static System.Numerics.Vector3 operator *(Matrix3x3 f, System.Numerics.Vector3 g)
        {
            float x = (f.M11 * g.X) + (f.M12 * g.Y) + (f.M13 * g.Z);
            float y = (f.M21 * g.X) + (f.M22 * g.Y) + (f.M23 * g.Z);
            float z = (f.M31 * g.X) + (f.M32 * g.Y) + (f.M33 * g.Z);
            return new System.Numerics.Vector3(x, y, z);
        }

        public static Stride.Core.Mathematics.Vector3 operator *(Matrix3x3 f, Stride.Core.Mathematics.Vector3 g)
        {
            float x = (f.M11 * g.X) + (f.M12 * g.Y) + (f.M13 * g.Z);
            float y = (f.M21 * g.X) + (f.M22 * g.Y) + (f.M23 * g.Z);
            float z = (f.M31 * g.X) + (f.M32 * g.Y) + (f.M33 * g.Z);
            return new Stride.Core.Mathematics.Vector3(x, y, z);
        }

        public static Matrix3x3 operator *(Matrix3x3 matrixA, Matrix3x3 matrixB)
        {
            //Matrix3x3 result = new Matrix3x3(
            return new Matrix3x3(
                (matrixA.M11 * matrixB.M11) + (matrixA.M12 * matrixB.M21) + (matrixA.M13 * matrixB.M31),
                (matrixA.M11 * matrixB.M12) + (matrixA.M12 * matrixB.M22) + (matrixA.M13 * matrixB.M32),
                (matrixA.M11 * matrixB.M13) + (matrixA.M12 * matrixB.M23) + (matrixA.M13 * matrixB.M33),

                (matrixA.M21 * matrixB.M11) + (matrixA.M22 * matrixB.M21) + (matrixA.M23 * matrixB.M31),
                (matrixA.M21 * matrixB.M12) + (matrixA.M22 * matrixB.M22) + (matrixA.M23 * matrixB.M32),
                (matrixA.M21 * matrixB.M13) + (matrixA.M22 * matrixB.M23) + (matrixA.M23 * matrixB.M33),

                (matrixA.M31 * matrixB.M11) + (matrixA.M32 * matrixB.M21) + (matrixA.M33 * matrixB.M31),
                (matrixA.M31 * matrixB.M12) + (matrixA.M32 * matrixB.M22) + (matrixA.M33 * matrixB.M32),
                (matrixA.M31 * matrixB.M13) + (matrixA.M32 * matrixB.M23) + (matrixA.M33 * matrixB.M33)
                );
            //return result;
        }

    }
}
