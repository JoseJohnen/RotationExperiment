using Stride.Core.Mathematics;
using Stride.Engine;

namespace RotationExperiment
{
    public class DebugController : SyncScript
    {
        public Entity Core;
        public Entity Weapon;
        public Entity LeftShoulder;
        public Entity RightShoulder;

        public override void Update()
        {
            DebugText.Print("Core.Transform.Position: " + Core.Transform.Position, new Int2(200, 490));
            DebugText.Print("Weapon.Transform.Position: " + Weapon.Transform.Position, new Int2(200, 470));
            DebugText.Print("LeftShoulder.Transform.Position: " + LeftShoulder.Transform.Position, new Int2(200, 450));
            DebugText.Print("RightShoulder.Transform.Position: " + RightShoulder.Transform.Position, new Int2(200, 430));

            DebugText.Print("RotationController.angleInRadians: " + RotationController.angleInRadians, new Int2(200, 390));
        }
    }
}
