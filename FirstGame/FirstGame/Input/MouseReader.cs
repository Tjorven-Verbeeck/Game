using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FirstGame.Input
{
    public class MouseReader : IInputReader
    {
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;

        public Vector2 ReadInput()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            Vector2 directionMouse = new Vector2(_currentMouseState.X, _currentMouseState.Y);
            if (directionMouse != Vector2.Zero)
            {
                directionMouse.Normalize();
            }
            return directionMouse;
        }

        public MouseState GetMouseState()
        {
            return Mouse.GetState();
        }

        public bool IsLeftButtonPressed()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool WasLeftButtonPressed()
        {
            return _previousMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool WasLeftButtonReleased()
        {
            return _previousMouseState.LeftButton == ButtonState.Pressed && _currentMouseState.LeftButton == ButtonState.Released;
        }

        // Add other methods as needed for right button, middle button, etc.
    }
}
