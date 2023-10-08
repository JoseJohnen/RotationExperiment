using Stride.Engine;
using Vector3 = Stride.Core.Mathematics.Vector3;

namespace RotationExperiment
{
    public class MovementController : SyncScript
    {
        public Entity Core;
        public Entity Weapon;
        public Entity LeftShoulder;
        public Entity RightShoulder;

        public static Vector3 weaponOffSet = new Vector3(0, 0, -2.5f);
        public static Vector3 leftShoulderOffSet = new Vector3(-2.5f, 0, 0);
        public static Vector3 rightShoulderOffSet = new Vector3(2.5f, 0, 0);

        public static Vector3 front;
        public static Vector3 left;
        public static Vector3 right;

        public override void Start()
        {
            base.Start();
            //front = Weapon.Transform.Position - Core.Transform.Position;
            //left = LeftShoulder.Transform.Position - Core.Transform.Position;
            //right = RightShoulder.Transform.Position - Core.Transform.Position;
        }

        public override void Update()
        {
            if (Input.HasKeyboard)
            {
                if (Input.IsKeyDown(Stride.Input.Keys.O))
                {
                    //Core.Transform.Position += WalkFront();
                    WalkFront();
                }
                else if (Input.IsKeyDown(Stride.Input.Keys.L))
                {
                    //Core.Transform.Position = WalkBack();
                    WalkBack();
                }

                /*if (Input.IsKeyDown(Stride.Input.Keys.O))
                {
                    Core.Transform.Position += front;
                    Weapon.Transform.Position = Core.Transform.Position + front + weaponOffSet;
                    LeftShoulder.Transform.Position = Core.Transform.Position + front + leftShoulderOffSet;
                    RightShoulder.Transform.Position = Core.Transform.Position + front + rightShoulderOffSet;
                }
                else if (Input.IsKeyDown(Stride.Input.Keys.L))
                {
                    Core.Transform.Position += -front;
                    Weapon.Transform.Position = Core.Transform.Position + front + weaponOffSet;
                    LeftShoulder.Transform.Position = Core.Transform.Position + left + leftShoulderOffSet;
                    RightShoulder.Transform.Position = Core.Transform.Position + right + rightShoulderOffSet;
                }*/
            }
        }

        #region Funciones Movimiento
        /// <summary>
        /// Move the player to the location forward according to the rotation
        /// </summary>
        /// <returns>Vector3 with the MODIFIER produced to induce the movement</returns>
        public Vector3 WalkFront()
        {
            Vector3 modifier = this.Weapon.Transform.Position - this.Core.Transform.Position;
            this.UpdatePosition = modifier;
            return modifier;
        }

        /// <summary>
        /// Move the player to the location forward according to the rotation
        /// </summary>
        /// <returns>Vector3 with the MODIFIER produced to induce the movement</returns>
        public Vector3 WalkBack()
        {
            Vector3 modifier = this.Core.Transform.Position - this.Weapon.Transform.Position;
            this.UpdatePosition = modifier;
            return modifier;
        }

        /// <summary>
        /// Move the player to the location Left according to the rotation
        /// </summary>
        /// <returns>Vector3 with the MODIFIER produced to induce the movement</returns>
        public Vector3 StrafeLeft()
        {
            Vector3 modifier = this.LeftShoulder.Transform.Position - this.Core.Transform.Position;
            this.UpdatePosition = modifier;
            return modifier;
        }

        /// <summary>
        /// Move the player to the location Right according to the rotation
        /// </summary>
        /// <returns>Vector3 with the MODIFIER produced to induce the movement</returns>
        public Vector3 StrafeRight()
        {
            Vector3 modifier = this.RightShoulder.Transform.Position - this.Core.Transform.Position;
            this.UpdatePosition = modifier;
            return modifier;
        }
        #endregion

        #region Modifier
        private Vector3 lastModifierUpdatePosition = Vector3.Zero;
        public Vector3 UpdatePosition
        {
            get
            {
                return lastModifierUpdatePosition;
            }
            set
            {
                //float y1 = Core.Transform.Position.Y;
                Core.Transform.Position += value;
                LeftShoulder.Transform.Position += value;
                RightShoulder.Transform.Position += value;
                Weapon.Transform.Position += value;
                //Y = y1;
                lastModifierUpdatePosition = value;
            }

        }

        /*public float X
        {
            get
            {
                return Core.Transform.Position.X;
            }
            set
            {
                float newX = value;
                Core.Transform.Position.X = newX;
                Weapon.Transform.Position.X = newX;
                LeftShoulder.Transform.Position.X = newX;
                RightShoulder.Transform.Position.X = newX;
            }
        }

        public float Y
        {
            get
            {
                return Core.Transform.Position.Y;
            }
            set
            {
                float newY = value;
                Core.Transform.Position.Y = newY;
                Weapon.Transform.Position.Y = newY;
                LeftShoulder.Transform.Position.Y = newY;
                RightShoulder.Transform.Position.Y = newY;
            }
        }

        public float Z
        {
            get
            {
                return Core.Transform.Position.Z;
            }
            set
            {
                float newZ = value;
                Core.Transform.Position.Z = newZ;
                Weapon.Transform.Position.Z = newZ;
                LeftShoulder.Transform.Position.Z = newZ;
                RightShoulder.Transform.Position.Z = newZ;
            }
        }*/
        #endregion


    }
}
