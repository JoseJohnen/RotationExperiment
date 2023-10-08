using Stride.Core.Mathematics;
using Stride.Engine;
using System;
using System.Numerics;
using Vortice.Mathematics;
using Quaternion = Stride.Core.Mathematics.Quaternion;
using Vector2 = Stride.Core.Mathematics.Vector2;
using Vector3 = Stride.Core.Mathematics.Vector3;

namespace RotationExperiment
{
    public class RotationController : SyncScript
    {
        public Entity Core;
        public Vector3 CorePos;
        public Vector3 CoreBase;

        public Entity Weapon;
        public Vector3 WeaponPos;
        public Vector3 WeaponBase;

        public Entity LeftShoulder;
        public Vector3 LeftShoulderPos;
        public Vector3 LeftShoulderBase;

        public Entity RightShoulder;
        public Vector3 RightShoulderPos;
        public Vector3 RightShoulderBase;

        public static float angleInRadians = 0;

        public override void Start()
        {
            base.Start();
            CoreBase = Core.Transform.Position;
            WeaponBase = Weapon.Transform.Position;
            LeftShoulderBase = LeftShoulder.Transform.Position;
            RightShoulderBase = RightShoulder.Transform.Position;
        }

        public override void Update()
        {
            if (Input.HasKeyboard)
            {
                float angled = Suplementary.DegreesToRadians(0.4f);
                //float rotationPointX = Core.Transform.Position.X;
                //float rotationPointY = Core.Transform.Position.Y;

                if (Input.IsKeyDown(Stride.Input.Keys.X))
                {
                    angleInRadians = angled; // Assuming 'angled' is in radians
                    Core.Transform.Rotation *= Quaternion.RotationY(-angled);

                    // Apply the 2D rotation to the objects
                    Matrix2x2 rotation2x2 = new Matrix2x2(
                        (float)Math.Cos(angleInRadians), -(float)Math.Sin(angleInRadians),
                        (float)Math.Sin(angleInRadians), (float)Math.Cos(angleInRadians)
                    );

                    // Convert it to a 3x3 equivalent
                    Matrix3x3 rotation3x3 = new Matrix3x3(
                        rotation2x2.M11, 0f, rotation2x2.M12,
                        0f, 1f, 0f,
                        rotation2x2.M21, 0f, rotation2x2.M22
                    );

                    // Calculate the required translation matrices
                    /*Matrix3x3 translateToOrigin = new Matrix3x3(
                        1f, 0f, -rotationPointX,
                        0f, 1f, -rotationPointY,
                        0f, 0f, 1f
                    );

                    Matrix3x3 translateBack = new Matrix3x3(
                        1f, 0f, rotationPointX,
                        0f, 1f, rotationPointY,
                        0f, 0f, 1f
                    );*/

                    // Combine the matrices to apply translation, rotation, and then translation back
                    //Matrix3x3 finalTransformation = translateBack * rotation3x3 * translateToOrigin;

                    //Vector3 positionOffset = new Vector3(0, 0, 0); // Adjust as needed

                    /*Weapon.Transform.Position = RotationController.ChangePositionMatrixBased(rotation2x2, Weapon.Transform.Position);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation2x2, LeftShoulder.Transform.Position);
                    RightShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation2x2, RightShoulder.Transform.Position);*/

                    //Iniciate translation
                    CorePos = Core.Transform.Position;
                    WeaponPos = Weapon.Transform.Position;
                    LeftShoulderPos = LeftShoulder.Transform.Position;
                    RightShoulderPos = RightShoulder.Transform.Position;

                    Vector3 mod = Core.Transform.Position;
                    if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
                    {
                        Weapon.Transform.Position -= mod;
                        LeftShoulder.Transform.Position -= mod;
                        RightShoulder.Transform.Position -= mod;
                    }

                    Weapon.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, Weapon.Transform.Position);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, LeftShoulder.Transform.Position);
                    RightShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, RightShoulder.Transform.Position);

                    /*Weapon.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, WeaponPos);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, LeftShoulderPos);
                    RightShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, RightShoulderPos);*/

                    //Core.Transform.Position += CorePos;
                    //Weapon.Transform.Position += WeaponPos;
                    //LeftShoulder.Transform.Position += LeftShoulderPos;
                    //RightShoulder.Transform.Position += RightShoulderPos;

                    if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
                    {
                        Weapon.Transform.Position += mod;
                        LeftShoulder.Transform.Position += mod;
                        RightShoulder.Transform.Position += mod;
                    }
                }
                else if (Input.IsKeyDown(Stride.Input.Keys.Z))
                {
                    angleInRadians = -angled; // Assuming 'angled' is in radians
                    Core.Transform.Rotation *= Quaternion.RotationY(angled);

                    // Apply the 2D rotation to the objects
                    Matrix2x2 rotation2x2 = new Matrix2x2(
                        (float)Math.Cos(angleInRadians), -(float)Math.Sin(angleInRadians),
                        (float)Math.Sin(angleInRadians), (float)Math.Cos(angleInRadians)
                    );

                    // Convert it to a 3x3 equivalent
                    Matrix3x3 rotation3x3 = new Matrix3x3(
                        rotation2x2.M11, 0f, rotation2x2.M12,
                        0f, 1f, 0f,
                        rotation2x2.M21, 0f, rotation2x2.M22
                    );

                    // Calculate the required translation matrices
                    /*Matrix3x3 translateToOrigin = new Matrix3x3(
                        1f, 0f, -rotationPointX,
                        0f, 1f, -rotationPointY,
                        0f, 0f, 1f
                    );

                    Matrix3x3 translateBack = new Matrix3x3(
                        1f, 0f, rotationPointX,
                        0f, 1f, rotationPointY,
                        0f, 0f, 1f
                    );*/

                    // Combine the matrices to apply translation, rotation, and then translation back
                    //Matrix3x3 finalTransformation = translateBack * rotation3x3 * translateToOrigin;

                    //Vector3 positionOffset = new Vector3(0, 0, 0); // Adjust as needed

                    /*Weapon.Transform.Position = RotationController.ChangePositionMatrixBased(rotation2x2, Weapon.Transform.Position);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation2x2, LeftShoulder.Transform.Position);
                    RightShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation2x2, RightShoulder.Transform.Position);*/

                    //Iniciate translation
                    CorePos = Core.Transform.Position;
                    WeaponPos = Weapon.Transform.Position;
                    LeftShoulderPos = LeftShoulder.Transform.Position;
                    RightShoulderPos = RightShoulder.Transform.Position;

                    Vector3 mod = Core.Transform.Position;
                    if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
                    {
                        Weapon.Transform.Position -= mod;
                        LeftShoulder.Transform.Position -= mod;
                        RightShoulder.Transform.Position -= mod;
                    }

                    Weapon.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, Weapon.Transform.Position);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, LeftShoulder.Transform.Position);
                    RightShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, RightShoulder.Transform.Position);

                    /*Weapon.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, WeaponPos);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, LeftShoulderPos);
                    RightShoulder.Transform.Position = RotationController.ChangePositionMatrixBased(rotation3x3, RightShoulderPos);*/

                    //Core.Transform.Position += CorePos;
                    //Weapon.Transform.Position += WeaponPos;
                    //LeftShoulder.Transform.Position += LeftShoulderPos;
                    //RightShoulder.Transform.Position += RightShoulderPos;

                    if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
                    {
                        Weapon.Transform.Position += mod;
                        LeftShoulder.Transform.Position += mod;
                        RightShoulder.Transform.Position += mod;
                    }
                }
            }
        }

        /// <summary>
        /// This enum represent each of the 3 dimensions, X, Y and Z
        /// </summary>
        public enum Axis { X, Y, Z }

        /// <summary>
        /// Tidal lock the Entity obj in the direction of the Vector3 called destination, the tidal lock is in the Axis determined by the parameter axis.
        /// </summary>
        /// <param name="obj">the entity to be tidal locked</param>
        /// <param name="destination">the point at the which obj is tidal locked</param>
        /// <param name="axis">the axis by which the obj is tidal lock</param>
        /// <returns></returns>
        public static Quaternion LookAtWithOnlyOneFreeAxis(Entity obj, Vector3 destination, Axis axis = Axis.Y)
        {
            try
            {
                Vector3 objPos = obj.Transform.Position;

                if (axis == Axis.X)
                {
                    objPos.X = obj.Transform.Position.X;
                }
                if (axis == Axis.Y)
                {
                    objPos.Y = obj.Transform.Position.Y;
                }
                if (axis == Axis.Z)
                {
                    objPos.Z = obj.Transform.Position.Z;
                }

                //objPos.Y = destination.Y;
                Matrix lookAt = Matrix.LookAtRH(objPos, destination, Vector3.UnitY);
                Matrix resultMatrix = Matrix.Transpose(lookAt);
                //Quaternion result = Quaternion.RotationMatrix(resultMatrix);
                Quaternion result = Quaternion.Zero;
                Vector3 scale = Vector3.Zero;
                Vector3 translation = Vector3.Zero;
                resultMatrix.Decompose(out scale, out result, out translation);
                obj.Transform.Rotation = result;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: LookAtAlt(Entity, Vector3): " + ex.Message);
                return Quaternion.Zero;
            }
        }

        /// <summary>
        /// This method returns a vector3 with the result position of the vector3 'v3' transformed by the rotation matrix
        /// </summary>
        /// <param name="m2x2">the 2x2 matrix which define the rotation</param>
        /// <param name="v3">the vector3 to be transformed by the rotation matrix m2x2</param>
        /// <returns>a new vector3 which is the v3 already transformde by the rotation matrix m2x2</returns>
        static Vector3 ChangePositionMatrixBased(Matrix2x2 m2x2, Vector3 v3)
        {
            Vector2 vec2 = new Vector2(v3.X, v3.Z);
            Vector2 v2result = m2x2 * vec2;
            return new Vector3(v2result.X, 0, v2result.Y);
        }

        /// <summary>
        /// This method returns a vector3 with the result position of the vector3 'v3' transformed by the rotation matrix
        /// </summary>
        /// <param name="m3x3">the 3x3 matrix which define the rotation</param>
        /// <param name="v3">the vector3 to be transformed by the rotation matrix m3x3</param>
        /// <returns>a new vector3 which is the v3 already transformde by the rotation matrix m3x3</returns>
        static Vector3 ChangePositionMatrixBased(Matrix3x3 m3x3, Vector3 v3)
        {
            return m3x3 * v3;
        }

        /// <summary>
        /// It rotates the vector3 v3 by the internal rotation matrix according to the angle of the number received
        /// </summary>
        /// <param name="angle">the angle of rotation of the internal rotation matrix of the function (in degrees)</param>
        /// <param name="v3">the vector3 to be transformed by a rotation matrix rotated by the number of  the angle (in degrees)</param>
        /// <param name="typeOfRotationY">NOT IN USE: It defines what direction is the rotation</param>
        /// <returns></returns>
        static Vector3 ChangePositionYAxis(float angle, Vector3 v3, TypeOfRotationY typeOfRotationY = TypeOfRotationY.Clockwise)
        {
            /*Matrix3x3 rotationMatrix = new Matrix3x3(
                cosTheta, 0, sinTheta,
                0, 1, 0,
                -sinTheta, 0, cosTheta
            );*/

            Matrix2x2 rotationMatrix = new Matrix2x2(angle, typeOfRotationY);
            Vector2 vec2 = new Vector2(v3.X, v3.Z);
            Vector2 v2result = rotationMatrix * vec2;
            return new Vector3(v2result.X, 0, v2result.Y);
        }

        /// <summary>
        /// Creates a quaternion that rotates around the y-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians.</param>
        /// <param name="result">When the method completes, contains the newly created quaternion.</param>
        public static void RotationY(float angle, out Stride.Core.Mathematics.Quaternion result)
        {
            float halfAngle = angle * 0.5f;
            result = new Stride.Core.Mathematics.Quaternion(0.0f, MathF.Sin(halfAngle), 0.0f, MathF.Cos(halfAngle));
        }

        /// <summary>
        /// Creates a quaternion that rotates around the y-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians.</param>
        /// <returns>The created rotation quaternion.</returns>
        public static Stride.Core.Mathematics.Quaternion RotationY(float angle)
        {
            Stride.Core.Mathematics.Quaternion result;
            RotationY(angle, out result);
            return result;
        }

        //UNUSUED
        public static Matrix4x4 RotationMatrix(Vector3 forward, Vector3 up)
        {
            forward.Normalize();
            up.Normalize();

            // Calculate the right vector by taking the cross product of forward and up
            Vector3 right = Vector3.Cross(up, forward);
            right.Normalize();

            // Recalculate the up vector to ensure it's orthogonal to forward and right
            up = Vector3.Cross(forward, right);
            up.Normalize();

            // Create the rotation matrix
            Matrix4x4 rotationMatrix = new Matrix4x4
            {
                M11 = right.X,
                M12 = right.Y,
                M13 = right.Z,
                M14 = 0,

                M21 = up.X,
                M22 = up.Y,
                M23 = up.Z,
                M24 = 0,

                M31 = forward.X,
                M32 = forward.Y,
                M33 = forward.Z,
                M34 = 0,

                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1
            };

            return rotationMatrix;
        }
    }
}
