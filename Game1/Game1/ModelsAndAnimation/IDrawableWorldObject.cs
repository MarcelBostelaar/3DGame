using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.ModelsAndAnimation
{
    interface IDrawableWorldObject
    {
        void Draw(GraphicsDevice GraphDevice, GameTime gameTime, Matrix View, Matrix Projection);

    }
}
