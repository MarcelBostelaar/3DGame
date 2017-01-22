using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Input
{

    abstract class KeyInput
    {
        public string KeyBindingName { get; private set; }
        public KeyInput(Keys key, string KeyBindingName)
        {
            this.KeyBindingName = KeyBindingName;
            Key = key;
        }
        public KeyInput(MouseButtons button, string KeyBindingName)
        {
            this.KeyBindingName = KeyBindingName;
            Button = button;
        }
        public void Rebind(Keys key)
        {
            Key = key;
            Keyboard = true;
        }
        public void Rebind(MouseButtons button)
        {
            Button = button;
            Keyboard = false;
        }
        protected bool Keyboard;
        protected Keys Key;
        protected MouseButtons Button;
        public abstract bool Get();
        public abstract void Update(CustomMouseState currmousestate, KeyboardState currkeyboardstate, CustomMouseState prevmousestate, KeyboardState prevkeyboardstate);
    }

    class KeyWentDown : KeyInput
    {
        public KeyWentDown(MouseButtons button, string KeyBindingName) : base(button, KeyBindingName) { }
        public KeyWentDown(Keys key, string KeyBindingName) : base(key, KeyBindingName) { }

        bool WentDown = false;
        public override bool Get()
        {
            return WentDown;
        }

        public override void Update(CustomMouseState currmousestate, KeyboardState currkeyboardstate, CustomMouseState prevmousestate, KeyboardState prevkeyboardstate)
        {
            switch (Keyboard)
            {
                case true:
                    if (prevkeyboardstate.IsKeyUp(Key) && currkeyboardstate.IsKeyDown(Key))
                    {
                        WentDown = true;
                    }
                    else
                    {
                        WentDown = false;
                    }
                    break;
                case false:
                    if (prevmousestate.isButtonUp(Button) && currmousestate.IsButtonDown(Button))
                    {
                        WentDown = true;
                    }
                    else
                    {
                        WentDown = false;
                    }
                    break;
            }
        }
    }

    class KeyWentUp : KeyInput
    {
        public KeyWentUp(Keys key, string KeyBindingName) : base(key, KeyBindingName) { }
        public KeyWentUp(MouseButtons button, string KeyBindingName) : base(button, KeyBindingName) { }

        bool WentUp = false;
        public override bool Get()
        {
            return WentUp;
        }

        public override void Update(CustomMouseState currmousestate, KeyboardState currkeyboardstate, CustomMouseState prevmousestate, KeyboardState prevkeyboardstate)
        {
            switch (Keyboard)
            {
                case true:
                    if (prevkeyboardstate.IsKeyDown(Key) && currkeyboardstate.IsKeyUp(Key))
                    {
                        WentUp = true;
                    }
                    else
                    {
                        WentUp = false;
                    }
                    break;
                case false:
                    if (prevmousestate.IsButtonDown(Button) && currmousestate.isButtonUp(Button))
                    {
                        WentUp = true;
                    }
                    else
                    {
                        WentUp = false;
                    }
                    break;
            }
        }
    }

    class KeyState : KeyInput
    {
        bool isDown = false;

        public KeyState(Keys key, string KeyBindingName) : base(key, KeyBindingName) { }
        public KeyState(MouseButtons button, string KeyBindingName) : base(button, KeyBindingName) { }

        public override bool Get()
        {
            return isDown;
        }

        public override void Update(CustomMouseState currmousestate, KeyboardState currkeyboardstate, CustomMouseState prevmousestate, KeyboardState prevkeyboardstate)
        {
            switch (Keyboard)
            {
                case true:
                    if (currkeyboardstate.IsKeyDown(Key))
                    {
                        isDown = true;
                    }
                    else
                    {
                        isDown = false;
                    }
                    break;
                case false:
                    if (currmousestate.IsButtonDown(Button))
                    {
                        isDown = true;
                    }
                    else
                    {
                        isDown = false;
                    }
                    break;
            }
        }
    }

}
