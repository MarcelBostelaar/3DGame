using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Input
{
    class KeyboardMouseInput : InputInterface
    {
        


        KeyboardState PreviousKeyboardState = Keyboard.GetState();
        CustomMouseState PreviousMouseState = CustomMouseState.Create();
        KeyboardState CurrentKeyboardState = Keyboard.GetState();
        CustomMouseState CurrentMouseState = CustomMouseState.Create();

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = CustomMouseState.Create();
            CurrentKeyboardState = Keyboard.GetState();
        }

        public float Rotate()
        {
            throw new NotImplementedException();
        }

        public bool ZoomIn()
        {
            throw new NotImplementedException();
        }

        public bool ZoomOut()
        {
            throw new NotImplementedException();
        }

    }
}
