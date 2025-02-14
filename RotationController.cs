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

        public Entity Target;

        public static float angleInRadians = 0;

        public override void Start()
        {
            base.Start();
            CoreBase = Core.Transform.Position;
            WeaponBase = Weapon.Transform.Position;
            LeftShoulderBase = LeftShoulder.Transform.Position;
            RightShoulderBase = RightShoulder.Transform.Position;
        }

        float angleRotatedInMatrix = 0;
        public override void Update()
        {
            if (Input.HasKeyboard)
            {
                //float result = CalculateRotationAngleAround(Core.Transform.Position, Target.Transform.Position);
                //DebugText.Print("Atan2: " + result, new Int2(200,200));

                //float angled = Suplementary.DegreesToRadians(0.4f);
                ////DebugText.Print("Angle: (Degrees)" + Suplementary.RadiansToDegrees(angleRotatedInMatrix), new Int2(200, 220));
                ////DebugText.Print("Angle: (Degrees)" + angleRotatedInMatrix, new Int2(200, 240));
                ////float rotationPointX = Core.Transform.Position.X;
                ////float rotationPointY = Core.Transform.Position.Y;

                if (Input.IsKeyDown(Stride.Input.Keys.Z))
                {
                    RotatePlayerPerAngleHorizontal(rotDir: RotationDirectionX.Left);
                }
                else if (Input.IsKeyDown(Stride.Input.Keys.X))
                {
                    RotatePlayerPerAngleHorizontal(rotDir: RotationDirectionX.Right);
                }

                if(Input.IsKeyDown(Stride.Input.Keys.C))
                {
                    RotatePlayerPerAngleVertical(rotDir: RotationDirectionY.Up);
                }
                else if (Input.IsKeyDown(Stride.Input.Keys.V))
                {
                    RotatePlayerPerAngleVertical(rotDir: RotationDirectionY.Down);
                }

                if(Input.IsKeyDown (Stride.Input.Keys.B))
                {
                    RotatePlayerPerAngleDeep(rotDir: RotationDirectionZ.Left);
                }
                else if(Input.IsKeyDown(Stride.Input.Keys.N))
                {
                    RotatePlayerPerAngleDeep(rotDir: RotationDirectionZ.Right);
                }
            }
        }

        public enum RotationDirectionY { Up = 0, Down = 1 }
        /// <summary>
        /// It rotates the player vertically by the X axis, the weapon that is (up to down or down to up) [PITCH]
        /// </summary>
        /// <param name="floatAngle">(float) default is 0.4f, the angle of the rotation happening</param>
        /// <param name="rotDir">(Enum) the direction of the rotation, being up or down</param>
        void RotatePlayerPerAngleVertical(float floatAngle = 0.4f, RotationDirectionY rotDir = RotationDirectionY.Up)
        {
            float angleInRadians = Suplementary.DegreesToRadians(floatAngle);
            if (rotDir == RotationDirectionY.Down) angleInRadians = -angleInRadians;

            // Get the rotated right direction of the Core (taking previous horizontal rotation into account)
            Vector3 rightDir = GetRightDirection(Core.Transform.Rotation);

            // Compute rotation matrix for pitch (rotation around the right axis)
            Matrix3x3 rotation3x3Vertical = Matrix3x3.CreateRotationMatrix(angleInRadians, rightDir);

            // Rotate weapon relative to the core's orientation
            Vector3 localWeaponPos = Weapon.Transform.Position - Core.Transform.Position;
            localWeaponPos = ChangePositionMatrixBased(rotation3x3Vertical, localWeaponPos);
            Weapon.Transform.Position = localWeaponPos + Core.Transform.Position;
        }

        public enum RotationDirectionZ { Left = 0, Right = 1 }
        /// <summary>
        /// It rotates the player by the Z axis (Deep) (left to right or right to left) [ROLL]
        /// </summary>
        /// <param name="floatAngle">(float) default is 0.4f, the angle of the rotation happening</param>
        /// <param name="rotDir">(Enum) the direction of the rotation, being left or right</param>
        void RotatePlayerPerAngleDeep(float floatAngle = 0.4f, RotationDirectionZ rotDir = RotationDirectionZ.Left)
        {
            float angleInRadians = Suplementary.DegreesToRadians(floatAngle);
            if (rotDir == RotationDirectionZ.Right) angleInRadians = -angleInRadians;

            // Get the rotated forward direction of the Core (taking previous horizontal rotation into account)
            Vector3 forwardDir = GetForwardDirection(Core.Transform.Rotation);

            // Compute rotation matrix for roll (rotation around the forward axis)
            Matrix3x3 rotation3x3Roll = Matrix3x3.CreateRotationMatrix(angleInRadians, forwardDir);

            Vector3 ApplyRoll(Vector3 shoulderPosition)
            {
                Vector3 localPos = shoulderPosition - Core.Transform.Position;
                localPos = ChangePositionMatrixBased(rotation3x3Roll, localPos);
                return localPos + Core.Transform.Position;
            }

            LeftShoulder.Transform.Position = ApplyRoll(LeftShoulder.Transform.Position);
            RightShoulder.Transform.Position = ApplyRoll(RightShoulder.Transform.Position);
        }

        public enum RotationDirectionX { Left = 0, Right = 1 }
        /// <summary>
        /// It rotates the player horizontally by the Y axis (left to right or right to left) [YAW]
        /// </summary>
        /// <param name="floatAngle">(float) default is 0.4f, the angle of the rotation happening</param>
        /// <param name="rotDir">(Enum) the direction of the rotation, being left or right</param>
        void RotatePlayerPerAngleHorizontal(float floatAngle = 0.4f, RotationDirectionX rotDir = RotationDirectionX.Left)
        {
            float angleInRadians = 0;
            float angled = Suplementary.DegreesToRadians(floatAngle);

            if (rotDir == RotationDirectionX.Left)
            {
                angleInRadians = -angled; // Assuming 'angled' is in radians
                Core.Transform.Rotation *= Quaternion.RotationY(angled);
            }
            else
            {
                angleInRadians = angled; // Assuming 'angled' is in radians
                Core.Transform.Rotation *= Quaternion.RotationY(-angled);
            }


            // Apply the 2D rotation to the objects
            Matrix2x2 rotation2x2 = new Matrix2x2(
                (float)Math.Cos(angleInRadians), -(float)Math.Sin(angleInRadians),
                (float)Math.Sin(angleInRadians), (float)Math.Cos(angleInRadians)
            );

            // Convert it to a 3x3 equivalent
            Matrix3x3 rotation3x3Horizontal = new Matrix3x3(
                rotation2x2.M11, 0f, rotation2x2.M12,
                0f, 1f, 0f,
                rotation2x2.M21, 0f, rotation2x2.M22
            );

            Vector3 mod = Core.Transform.Position;
            if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
            {
                Weapon.Transform.Position -= mod;
                LeftShoulder.Transform.Position -= mod;
                RightShoulder.Transform.Position -= mod;
            }

            Weapon.Transform.Position = ChangePositionMatrixBased(rotation3x3Horizontal, Weapon.Transform.Position);
            LeftShoulder.Transform.Position = ChangePositionMatrixBased(rotation3x3Horizontal, LeftShoulder.Transform.Position);
            RightShoulder.Transform.Position = ChangePositionMatrixBased(rotation3x3Horizontal, RightShoulder.Transform.Position);

            if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
            {
                Weapon.Transform.Position += mod;
                LeftShoulder.Transform.Position += mod;
                RightShoulder.Transform.Position += mod;
            }
        }

        #region Rotate Weapon with No Core Area
        void RotateWeaponPerAngleVerticalWithNoCore(float floatAngle = 0.4f, RotationDirectionY rotDir = RotationDirectionY.Up)
        {
            float angleInRadians = 0;
            float angled = Suplementary.DegreesToRadians(floatAngle);

            if (rotDir == RotationDirectionY.Up)
            {
                angleInRadians = -angled; // Assuming 'angled' is in radians
                Core.Transform.Rotation *= Quaternion.RotationX(angled);
            }
            else
            {
                angleInRadians = angled; // Assuming 'angled' is in radians
                Core.Transform.Rotation *= Quaternion.RotationX(-angled);
            }


            // Apply the 2D rotation to the objects
            //Matrix2x2 rotation2x2 = new Matrix2x2(
            //    (float)Math.Cos(angleInRadians), -(float)Math.Sin(angleInRadians),
            //    (float)Math.Sin(angleInRadians), (float)Math.Cos(angleInRadians)
            //);

            // Convert it to a 3x3 equivalent
            Matrix3x3 rotation3x3Vertical = new Matrix3x3(
                1f, 0f, 0f,
                0f, (float)Math.Cos(angleInRadians), (float)Math.Sin(angleInRadians),
                0f, -(float)Math.Sin(angleInRadians), (float)Math.Cos(angleInRadians)
            );

            Vector3 mod = Core.Transform.Position;
            if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
            {
                Weapon.Transform.Position -= mod;
                //LeftShoulder.Transform.Position -= mod;
                //RightShoulder.Transform.Position -= mod;
            }

            Weapon.Transform.Position = ChangePositionMatrixBased(rotation3x3Vertical, Weapon.Transform.Position);
            //LeftShoulder.Transform.Position = ChangePositionMatrixBased(rotation3x3Vertical, LeftShoulder.Transform.Position);
            //RightShoulder.Transform.Position = ChangePositionMatrixBased(rotation3x3Vertical, RightShoulder.Transform.Position);

            if (Core.Transform.Position != new Vector3(0f, 0.5f, 0f))
            {
                Weapon.Transform.Position += mod;
                //LeftShoulder.Transform.Position += mod;
                //RightShoulder.Transform.Position += mod;
            }
        }
        #endregion

        Vector3 GetRightDirection(Quaternion rotation)
        {
            return rotation * Vector3.UnitX; // Rotated right direction
        }

        Vector3 GetForwardDirection(Quaternion rotation)
        {
            return rotation * Vector3.UnitZ; // Rotated forward direction
        }

        //// Convert it to a 3x3 equivalent
        public static float CalculateRotationAngleAround(Vector3 pointA, Vector3 pointB, Axis axis = Axis.X)
        {
            // Calculate the difference vector between point A and point B
            Vector3 positionDifference = pointB - pointA;

            float rotationAngle = 0;
            if (axis == Axis.X)
            {
                // Calculate the rotation angle around the X-axis
                rotationAngle = MathF.Atan2(positionDifference.Y, positionDifference.Z);
            }
            else if (axis == Axis.Y)
            {
                // Calculate the rotation angle around the Y-axis
                rotationAngle = MathF.Atan2(positionDifference.X, positionDifference.Z);
            }
            else
            {
                // Calculate the rotation angle around the Z-axis
                rotationAngle = MathF.Atan2(positionDifference.X, positionDifference.Y);
            }

            // Convert the angle to degrees if needed
            rotationAngle = MathHelper.ToDegrees(rotationAngle);

            return rotationAngle;
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
