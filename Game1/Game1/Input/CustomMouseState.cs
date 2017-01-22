using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Input
{
    public enum MouseButtons
    {
        LeftButton,
        MiddleButton, 
        RightButton,
        XButton1,
        XButton2
    }

    public struct CustomMouseState
    {
        MouseState state;

        public static CustomMouseState Create()
        {
            var newone = new CustomMouseState();
            newone.state = Mouse.GetState();
            return newone;
        }

        public MouseButtons[] GetPressedButtons()
        {
            List<MouseButtons> pressedbuttons = new List<MouseButtons>();
            if (state.LeftButton == ButtonState.Pressed)
            {
                pressedbuttons.Add(MouseButtons.LeftButton);
            }
            if (state.RightButton == ButtonState.Pressed)
            {
                pressedbuttons.Add(MouseButtons.RightButton);
            }
            if (state.MiddleButton == ButtonState.Pressed)
            {
                pressedbuttons.Add(MouseButtons.MiddleButton);
            }
            if (state.XButton1 == ButtonState.Pressed)
            {
                pressedbuttons.Add(MouseButtons.XButton1);
            }
            if (state.XButton2 == ButtonState.Pressed)
            {
                pressedbuttons.Add(MouseButtons.XButton2);
            }
            return pressedbuttons.ToArray();
        }
        public bool IsButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    if (state.LeftButton == ButtonState.Pressed) return true;
                    break;
                case MouseButtons.RightButton:
                    if (state.RightButton == ButtonState.Pressed) return true;
                    break;
                case MouseButtons.MiddleButton:
                    if (state.MiddleButton == ButtonState.Pressed) return true;
                    break;
                case MouseButtons.XButton1:
                    if (state.XButton1 == ButtonState.Pressed) return true;
                    break;
                case MouseButtons.XButton2:
                    if (state.XButton2 == ButtonState.Pressed) return true;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return false;
        }
        public bool isButtonUp(MouseButtons button)
        {
            return !IsButtonDown(button);
        }
        //
        // Summary:
        //     Gets cursor position.
        public Point Position { get {return state.Position;} }
        //
        // Summary:
        //     Returns cumulative scroll wheel value since the game start.
        public int ScrollWheelValue { get { return state.ScrollWheelValue; } }
        //
        // Summary:
        //     Gets horizontal position of the cursor.
        public int X { get { return state.X; } }
        //
        // Summary:
        //     Gets vertical position of the cursor.
        public int Y { get { return state.Y; } }

    }
}
