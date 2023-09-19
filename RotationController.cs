using Stride.Engine;
using System.Numerics;
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
                if (Input.IsKeyPressed(Stride.Input.Keys.Z))
                {
                    Weapon.Transform.Position = RotationController.ChangePositionYAxis(90, Weapon.Transform.Position, TypeOfRotationY.CounterClockwise);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionYAxis(90, LeftShoulder.Transform.Position, TypeOfRotationY.CounterClockwise);
                    RightShoulder.Transform.Position = RotationController.ChangePositionYAxis(90, RightShoulder.Transform.Position, TypeOfRotationY.CounterClockwise);
                }
                else if (Input.IsKeyPressed(Stride.Input.Keys.X))
                {
                    Weapon.Transform.Position = RotationController.ChangePositionYAxis(90, Weapon.Transform.Position);
                    LeftShoulder.Transform.Position = RotationController.ChangePositionYAxis(90, LeftShoulder.Transform.Position);
                    RightShoulder.Transform.Position = RotationController.ChangePositionYAxis(90, RightShoulder.Transform.Position);
                }
            }
        }

        static Vector3 ChangePositionYAxis(float angle, Vector3 v3, TypeOfRotationY typeOfRotationY = TypeOfRotationY.Clockwise)
        {
            /*Matrix3x3 rotationMatrix = new Matrix3x3(
                cosTheta, 0, sinTheta,
                0, 1, 0,
                -sinTheta, 0, cosTheta
            );*/

            Matrix2x2 rotationMatrix = new Matrix2x2(angle ,typeOfRotationY);
            Vector2 vec2 = new Vector2(v3.X, v3.Z);
            Vector2 v2result = rotationMatrix * vec2;
            return new Vector3(v2result.X, 0, v2result.Y);
        }

    }
}
