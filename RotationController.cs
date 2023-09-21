using Stride.Core.Mathematics;
using Stride.Engine;
using System;
using System.Numerics;
using Quaternion = Stride.Core.Mathematics.Quaternion;
using Vector2 = Stride.Core.Mathematics.Vector2;
using Vector3 = Stride.Core.Mathematics.Vector3;

namespace RotationExperiment
{
    public class RotationController : SyncScript
    {
        public Entity Core;
        public Entity Weapon;
        public Entity LeftShoulder;
        public Entity RightShoulder;

        public override void Update()
        {
            if (Input.HasKeyboard)
            {
                float angle = 0.4f;//90f;
                Core.Transform.Rotation = LookAtWithOnlyOneFreeAxis(Core, Weapon.Transform.Position);
                if (Input.IsKeyDown(Stride.Input.Keys.Z))
                {
                    //Core.Transform.Rotation *= RotationY(0.0069f);
                    Weapon.Transform.Position = RotationController.ChangePositionYAxis(angle, Weapon.Transform.Position, TypeOfRotationY.CounterClockwise);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionYAxis(angle, LeftShoulder.Transform.Position, TypeOfRotationY.CounterClockwise);
                    RightShoulder.Transform.Position = RotationController.ChangePositionYAxis(angle, RightShoulder.Transform.Position, TypeOfRotationY.CounterClockwise);
                }
                else if (Input.IsKeyDown(Stride.Input.Keys.X))
                {
                    //Core.Transform.Rotation *= RotationY(-0.0069f);
                    Weapon.Transform.Position = RotationController.ChangePositionYAxis(angle, Weapon.Transform.Position);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionYAxis(angle, LeftShoulder.Transform.Position);
                    RightShoulder.Transform.Position = RotationController.ChangePositionYAxis(angle, RightShoulder.Transform.Position);
                }
            }
        }

        public enum Axis { X, Y, Z }

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
        /// Rotate the position of the entity in the direction of the destination - NO FUNCIONA -
        /// </summary>
        /// <param name="obj">The entity than is going to be rotated</param>
        /// <param name="destination">The direction than the entity should be rotated to</param>
        //public static Stride.Core.Mathematics.Quaternion RotateToDirection(Entity obj, Vector3 destination)
        //{
        //    Vector3 direction = destination - obj.Transform.Position;
        //    Stride.Core.Mathematics.Quaternion rotation = Stride.Core.Mathematics.Quaternion.BetweenDirections(obj.Transform.WorldMatrix.TranslationVector, direction);
        //    obj.Transform.Rotation = Stride.Core.Mathematics.Quaternion.Lerp(obj.Transform.Rotation, rotation, 1);
        //    return Stride.Core.Mathematics.Quaternion.Lerp(obj.Transform.Rotation, rotation, 1);
        //}

        //static void TidalLocking(Entity entityA, Entity entityB)
        //{
        //    if (entityA != null && entityB != null)
        //    {
        //        // Calculate the direction from Entity A to Entity B
        //        var direction = entityB.Transform.WorldMatrix.TranslationVector - entityA.Transform.WorldMatrix.TranslationVector;

        //        // Ensure the direction is not zero (to avoid division by zero)
        //        if (direction.LengthSquared() > 0.001f)
        //        {
        //            // Calculate the rotation to face Entity B
        //            var rotation = Quaternion.RotationMatrix(Matrix.RotationLookAtLH(Vector3.UnitZ, direction, Vector3.UnitY));

        //            // Apply the rotation to Entity A
        //            entityA.Transform.Rotation = rotation;
        //        }
        //    }
        //}

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

    }
}
