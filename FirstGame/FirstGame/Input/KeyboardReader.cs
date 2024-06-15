using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Input
{
    public class KeyboardReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            KeyboardState currentState = Keyboard.GetState();
            var direction = Vector2.Zero;
            if (currentState.IsKeyDown(Keys.Q))
            {
                direction.X = -1;
            }
            if (currentState.IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }
            if (currentState.IsKeyDown(Keys.Z))
            {
                direction.Y = -1;
            }
            if (currentState.IsKeyDown(Keys.S))
            {
                direction.Y = 1;
            }
            KeyboardState previousState = currentState;
            return direction;
        }
    }
}
