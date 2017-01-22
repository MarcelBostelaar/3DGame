using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.CameraSystems
{
    class Orthographic3rdPerson
    {
        Matrix Projection;
        bool ProjectionHasChanged;
        float MaxPitch, Minpitch;
        float DistanceFromTarget;
        float Yaw, Pitch;
        float AspectRatio;
        float MinZoom, MaxZoom;
        float NearPlane, FarPlane;
        float Zoom;

        /// <summary>
        /// Constructor for the Orthographic third person camera class.
        /// </summary>
        /// <param name="DistanceFromTarget">The distance the camera is from the target</param>
        /// <param name="Yaw">The initial Yaw of the camera, which is the rotation around the y axis(</param>
        /// <param name="Pitch">The initial Pitch value of the camera, which is the angle of shaking yes</param>
        /// <param name="MaxPitch">Maximum allowed Pitch, 1.75 * PI for best result</param>
        /// <param name="Minpitch">Minimum allowed Pitch, 1.5 * PI for best result</param>
        /// <param name="MaxDistance">The maximum distance the camera can be from the target</param>
        /// <param name="MinDistance">The minimum distance the camera can be from the target</param>
        /// <param name="AspectRatio">The aspect ratio of the game window</param>
        /// <param name="NearPlane">The near plane of the camera</param>
        /// <param name="FarPlane">The far plane of the camera</param>
        public Orthographic3rdPerson(float DistanceFromTarget, float Yaw, float Pitch, float MaxPitch, float Minpitch, float AspectRatio, float NearPlane, float FarPlane, float MaxZoom, float MinZoom)
        {
            this.DistanceFromTarget = DistanceFromTarget;
            this.Yaw = Yaw;
            this.Pitch = Pitch;
            this.MaxPitch = MaxPitch;
            this.Minpitch = Minpitch;
            this.AspectRatio = AspectRatio;
            this.NearPlane = NearPlane;
            this.FarPlane = FarPlane;
            this.MaxZoom = MaxZoom;
            this.MinZoom = MinZoom;
            this.zoom = (MinZoom + MaxZoom) / 2;
            ProjectionHasChanged = true;
        }

        /// <summary>
        /// A method to rotate a 2d vector so that its value is relative to the screen, rather than the world;
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>Rotated vector2</returns>
        public Vector2 RotateFlatVector(Vector2 vector)
        {
            return Vector2.Transform(vector, Matrix.CreateRotationZ(yaw));
        }

        /// <summary>
        /// A method to rotate a 3d vector so that its value is relative to the screen, rather than the world;
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>Rotated vector3</returns>
        public Vector3 Rotate3DVector(Vector3 vector)
        {
            vector = Vector3.Transform(vector, Matrix.CreateRotationX(pitch));
            vector = Vector3.Transform(vector, Matrix.CreateRotationY(yaw));
            return vector;
        }

        /// <summary>
        /// Getter and setter for the zoom of this camera, within set bounds.
        /// </summary>
        public float zoom
        {
            get
            {
                return Zoom;
            }
            set
            {
                ProjectionHasChanged = true;
                if (value > MaxZoom)
                {
                    Zoom = MaxZoom;
                }
                else
                {
                    if (value < MinZoom)
                    {
                        Zoom = MinZoom;
                    }
                    else
                    {
                        Zoom = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a viewing matrix for a specific target.
        /// </summary>
        /// <param name="Target">A vector 3 describing the targets position.</param>
        /// <returns></returns>
        virtual public Matrix GetCamera(Vector3 Target)
        {
            Vector3 camera = new Vector3(0, 0, distanceFromTarget);
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
        /// Stter and getter for the yaw of the camera, within allowed bounds.
        /// </summary>
        public float yaw
        {
            get { return Yaw; }
            set
            {
                Yaw = value;
                Yaw %= (float)Math.PI * 2;//ensures the value of yaw is always between 0 and 2 pi.
                Yaw += (float)Math.PI * 2;
                Yaw %= (float)Math.PI * 2;
            }
        }

        /// <summary>
        /// Setter and getter for the pitch of the camera, within allowed bounds.
        /// </summary>
        public float pitch
        {
            get { return Pitch; }
            set
            {
                var temp = value;

                if (temp > MaxPitch)
                {
                    Pitch = MaxPitch;
                    return;
                }
                if (temp < Minpitch)
                {
                    Pitch = Minpitch;
                    return;
                }
                Pitch = temp;
            }
        }

        /// <summary>
        /// Getter and setter for the nearplane. Sets the "projection has changed" to true;
        /// </summary>
        public float nearPlane
        {
            get { return NearPlane; }
            set
            {
                ProjectionHasChanged = true;
                NearPlane = value;
            }
        }
        /// <summary>
        /// Getter and setter for the farplane. Sets the "projection has changed" to true;
        /// </summary>
        public float farPlane
        {
            get { return FarPlane; }
            set
            {
                ProjectionHasChanged = true;
                FarPlane = value;
            }
        }
        
        /// <summary>
        /// Getter and Setter for the aspect ratio of this camera.
        /// </summary>
        public float aspectRatio
        {
            get
            {
                return AspectRatio;
            }

            set
            {
                ProjectionHasChanged = true;
                AspectRatio = value;
            }
        }

        /// <summary>
        /// setter and getter for the distance to the camera, within allowed bounds.
        /// </summary>
        public float distanceFromTarget
        {
            get
            {
                return DistanceFromTarget;
            }

            set
            {
                ProjectionHasChanged = true;
                DistanceFromTarget = value;
            }
        }

        public Matrix projection
        {
            get
            {
                if (!ProjectionHasChanged)
                {
                    return Projection;
                }
                ProjectionHasChanged = false;
                Projection = Matrix.CreateOrthographic(1 * aspectRatio * zoom, 1 * zoom, nearPlane, farPlane);
                return Projection;
            }
        }
    }
}
