using Microsoft.Xna.Framework.Input;

namespace Asteroids {
    public class ControlManager {

        // Control states
        public KeyboardState prevKBState;
        public KeyboardState currKBState;

        public GamePadState prevGPState;
        public GamePadState currGPState;

        public MouseState prevMouseState;
        public MouseState currMouseState;

        public void Update(KeyboardState keyboardState, GamePadState gamePadState, MouseState mouseState) {
            prevKBState = currKBState;
            prevGPState = currGPState;
            prevMouseState = currMouseState;

            currKBState = keyboardState;
            currGPState = gamePadState;
            currMouseState = mouseState;
        }

        public float ScalarPressed(PlayerCommand command) {
            if (command == PlayerCommand.MoveLeftRight) {    
                if (currKBState.IsKeyDown(Keys.Left) || currGPState.DPad.Left == ButtonState.Pressed)
                    return -1.0f;
                if (currKBState.IsKeyDown(Keys.Right) || currGPState.DPad.Right == ButtonState.Pressed)
                    return 1.0f;
                return currGPState.ThumbSticks.Left.X;
            } else if (command == PlayerCommand.MoveUpDown) {
                if (currKBState.IsKeyDown(Keys.Up) || currGPState.DPad.Up == ButtonState.Pressed)
                    return -1f;
                else if (currKBState.IsKeyDown(Keys.Down) || currGPState.DPad.Down == ButtonState.Pressed)
                    return 1f;
                return currGPState.ThumbSticks.Left.Y;
            }
            return 0f;
        }

        public bool Pressed(PlayerCommand command) {
            if (command == PlayerCommand.Fire) {
                return currGPState.Buttons.A == ButtonState.Pressed || currKBState.IsKeyDown(Keys.Space);
            }
            return false;
        }
    }

    public enum PlayerCommand {
        MoveLeftRight,
        MoveUpDown,
        Fire,
    }
}