using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.ModelsAndAnimation
{
    class StaticWorldObject : IDrawableWorldObject
    {
        public AlphaTestEffect Effect { get; set; }
        public Vector3 Position { get; set; }
        private Texture2D Texture;
        private ModelClass Model;
        Matrix WorldMatrix;

        public StaticWorldObject(string ModelName, string TextureName, Vector3 Position, CustomContentManager manager, GraphicsDevice GraphDevice)
        {
            this.Position = Position;
            Model = manager.Load<ModelClass>(ModelName);
            Texture = manager.Load<Texture2D>(TextureName);
            
            Effect = new AlphaTestEffect(GraphDevice);
            Effect.ReferenceAlpha = 255;
            Effect.AlphaFunction = CompareFunction.GreaterEqual;
            Effect.VertexColorEnabled = false;
            Effect.Texture = Texture;
            WorldMatrix = Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up);
            Effect.World = WorldMatrix;
        }

        public void Draw(GraphicsDevice GraphDevice, GameTime gameTime, Matrix View, Matrix Projection)
        {
            Effect.View = View;
            Effect.Projection = Projection;
            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    Model.Vertices, 0, Model.Vertices.Length,
                    Model.Indices, 0, Model.PrimitiveCount
                    );
            }
        }
    }
}
