using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FirstGame.Input
{
    public class MouseReader : IInputReader
    {
        GameWindow _window;
        
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;

        public MouseReader(GameWindow window)
        {
            _window = window;
        }
        public Vector2 ReadInput()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState(_window);

            Vector2 directionMouse = new Vector2(_currentMouseState.X, _currentMouseState.Y);
            if (directionMouse != Vector2.Zero)
            {
                directionMouse.Normalize();
            }
            return directionMouse;
        }

        public void UpdateMouseState()
        {
            var newMouseState = Mouse.GetState(_window);
            if (newMouseState.X >= 0 && newMouseState.Y >= 0 && newMouseState.X <= _window.ClientBounds.Width && _currentMouseState.Y <= _window.ClientBounds.Height)
            {
                _previousMouseState = _currentMouseState;
                _currentMouseState = newMouseState;
            }
        }

        public Rectangle GetMouseCursor()
        {
            return new(_currentMouseState.X, _currentMouseState.Y, 1, 1);
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
