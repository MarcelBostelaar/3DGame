using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Button
    {
        private int x, y, width, height;
        string text;
        Color color, textcolor;
        public event EventHandler OnClick;
        MouseState prevstate;
        public Button(int xpos, int ypos, int width, int height, string text, Color color, Color Textcolor)
        {
            x = xpos;
            y = ypos;
            this.width = width;
            this.height = height;
            this.text = text;
            this.color = color;
            this.textcolor = Textcolor;
        }

        public void draw(SpriteBatch sprbatch, SpriteFont font)
        {
            sprbatch.Draw(Game1.SingleWhitePixel, new Rectangle(x, y, width, height), color);
            var lol= font.MeasureString(text);
            sprbatch.DrawString(font, text, new Vector2((width / 2 - lol.X / 2) + x, (height/2 - lol.Y/2) + y), textcolor);
        }

        public void CheckClick()
        {
            var currstate = Mouse.GetState();
            if(currstate.LeftButton == ButtonState.Pressed && prevstate.LeftButton == ButtonState.Released)
            {
                if(currstate.X >= x && currstate.X <= x+width && currstate.Y >= y && currstate.Y <= y + height)
                {
                    OnClick.Invoke(null, null);
                }
            }
        }
    }
}
