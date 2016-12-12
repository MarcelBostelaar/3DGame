using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.CameraSystems
{
    class ThirdPersonCamera
    {
        float DistanceFromTarget;
        float Yaw;
        float Pitch;
        float MaxPitch, Minpitch, MaxDistance, MinDistance;

        /// <summary>
        /// setter and getter for the distance to the camera, within allowed bounds.
        /// </summary>
        public float distanceFromTarget { get { return DistanceFromTarget; } set {
                if (value > MaxDistance)
                {
                    DistanceFromTarget = MaxDistance;
                    return;
                }
                if(value < MinDistance)
                {
                    DistanceFromTarget = MinDistance;
                    return;
                }
                DistanceFromTarget = value;
            } }
        /// <summary>
        /// Stter and getter for the yaw of the camera, within allowed bounds.
        /// </summary>
        public float yaw { get { return Yaw; }
            set
            {
                Yaw = value;
                Yaw %= (float)Math.PI * 2;//ensures the value of yaw is always between 0 and 2 pi.
                Yaw += (float)Math.PI * 2;
                Yaw %= (float)Math.PI * 2;
            } }
        /// <summary>
        /// Setter and getter for the pitch of the camera, within allowed bounds.
        /// </summary>
        public float pitch { get { return Pitch; } set
            {
                var temp = value;

                //temp %= (float)Math.PI * 2;//ensures the value of temp is between 0 and 2 pi.
                //temp += (float)Math.PI * 2;
                //temp %= (float)Math.PI * 2;

                if(temp > MaxPitch)
                {
                    Pitch = MaxPitch;
                    return;
                }
                if(temp < Minpitch)
                {
                    Pitch = Minpitch;
                    return;
                }
                Pitch = temp;
            } }

        /// <summary>
        /// Constructor of a third person camera view.
        /// </summary>
        /// <param name="DistanceFromTarget">The distance the camera is from the target</param>
        /// <param name="Yaw">The initial Yaw of the camera, which is the rotation around the y axis(</param>
        /// <param name="Pitch">The initial Pitch value of the camera, which is the angle of shaking yes</param>
        /// <param name="MaxPitch">Maximum allowed Pitch</param>
        /// <param name="Minpitch">Minimum allowed Pitch</param>
        /// <param name="MaxDistance">The maximum distance the camera can be from the target</param>
        public ThirdPersonCamera(float DistanceFromTarget, float Yaw, float Pitch, float MaxPitch, float Minpitch, float MaxDistance, float MinDistance)
        {
            this.DistanceFromTarget = DistanceFromTarget;
            this.Yaw = Yaw;
            this.Pitch = Pitch;
            this.MaxPitch = MaxPitch;
            this.Minpitch = Minpitch;
            this.MaxDistance = MaxDistance;
            this.MinDistance = MinDistance;
        }

        public static ThirdPersonCamera CreateUnlimitedTurnable(float DistanceFromTarget, float Yaw, float Pitch, float MaxDistance, float MinDistance)
        {
            return new ThirdPersonCamera(DistanceFromTarget, Yaw, Pitch, float.MaxValue, float.MinValue, MaxDistance, MinDistance);
        }

        /// <summary>
        /// Gets a viewing matrix for a specific target.
        /// </summary>
        /// <param name="Target">A vector 3 describing the targets position.</param>
        /// <returns></returns>
        public Matrix GetCamera(Vector3 Target)
        {
            Vector3 camera = new Vector3(0, 0, DistanceFromTarget);
            var yawmatrix = Matrix.CreateRotationY(Yaw);
            var pitchmatrix = Matrix.CreateRotationX(Pitch);
            camera = Vector3.Transform(camera, pitchmatrix);
            camera = Vector3.Transform(camera, yawmatrix);
            camera += Target;
            var up = Vector3.Up;
            up = Vector3.Transform(up, pitchmatrix);
            up = Vector3.Transform(up, yawmatrix);
            return Matrix.CreateLookAt(camera, Target, up);
        }

        /// <summary>
        /// A method to rotate a 2d vector so that its value is relative to the screen, rather than the world;
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>Rotated vector2</returns>
        public Vector2 RotateFlatVector(Vector2 vector)
        {
            return Vector2.Transform(vector, Matrix.CreateRotationZ(Yaw));
        }
        /// <summary>
        /// A method to rotate a 3d vector so that its value is relative to the screen, rather than the world;
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>Rotated vector3</returns>
        public Vector3 Rotate3DVector(Vector3 vector)
        {
            vector = Vector3.Transform(vector, Matrix.CreateRotationX(Pitch));
            vector = Vector3.Transform(vector, Matrix.CreateRotationY(Yaw));
            return vector;
        }
    }
}
