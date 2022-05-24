using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class InputHandler
    {
        private static InputHandler instance;
        private Dictionary<KeyInfo, ICommand> keybinds = new Dictionary<KeyInfo, ICommand>();
        private readonly ButtonEvent buttonEvent = new ButtonEvent();

        //private Player player;

        private InputHandler()
        {
            keybinds.Add(new KeyInfo(Keys.D), new MoveCommand(new Vector2(1, 0)));
            keybinds.Add(new KeyInfo(Keys.A), new MoveCommand(new Vector2(-1, 0)));
            keybinds.Add(new KeyInfo(Keys.W), new MoveCommand(new Vector2(0, -1)));
            keybinds.Add(new KeyInfo(Keys.S), new MoveCommand(new Vector2(0, 1)));
            keybinds.Add(new KeyInfo(Keys.T), new MoveCommand(new Vector2(0, 0)));
        }

        public void Execute(Player player)
        {
            KeyboardState keyState = Keyboard.GetState();

            foreach (KeyInfo keyInfo in keybinds.Keys)
            {
                if (keyState.IsKeyDown(keyInfo.Key))
                {
                    keybinds[keyInfo].Execute(player);
                    buttonEvent.Notify(keyInfo.Key, BUTTONSTATE.DOWN);
                    keyInfo.IsDown = true;
                }

                if (!keyState.IsKeyDown(keyInfo.Key) && keyInfo.IsDown == true)
                {
                    buttonEvent.Notify(keyInfo.Key, BUTTONSTATE.UP);
                }
            }
        }
        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }
    }

    public class KeyInfo
    {
        public bool IsDown { get; set; }
        public Keys Key { get; set; }

        public KeyInfo(Keys key)
        {
            this.Key = key;
        }
    }
}
