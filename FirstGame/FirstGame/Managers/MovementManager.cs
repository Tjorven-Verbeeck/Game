using FirstGame.Interfaces;
using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Managers
{
    public class MovementManager
    {
        private Game1 game1Instance = new Game1();
        public void Move(IMovable movable)
        {
            // Use InputReader and SpeedUp if movable is Hero
            if (movable is Hero hero)
            {
                var direction = hero.InputReader.ReadInput();

                float maxSpeed = 10;

                var distance = direction * hero.Speed;
                hero.Speed = Limit(hero.Speed, maxSpeed);
                var futurePosition = hero.Position + distance;
                if (direction.Equals(new Vector2(0, 0)))
                {
                    hero.Speed = new Vector2(1, 1);
                }
                else
                {
                    hero.Speed += hero.SpeedUp;
                }

                if (futurePosition.X + (32 * game1Instance.scale) < 1920 && futurePosition.X > 0 && futurePosition.Y + (32 * game1Instance.scale) < 1080 && futurePosition.Y > 0)
                {
                    hero.Position = futurePosition;
                }
            }
        }


        private Vector2 Limit(Vector2 speed, float maxSpeed)
        {
            if (speed.Length() > maxSpeed)
            {
                var ratio = maxSpeed / speed.Length();
                speed.X *= ratio;
                speed.Y *= ratio;
            }
            return speed;
        }





        // Move with no input
        /*q
        private void Move()
        {
            position += speed;
            speed += speedUp;
            float maxSpeed = 10;
            speed = Limit(speed, maxSpeed);
            if (position.X + 140 > 800 || position.X < 0)
            {
                speed.X *= -1;
                speedUp.X *= -1;
            }
            if (position.Y + 108 > 480 || position.Y < 0)
            {
                speed.Y *= -1;
                speedUp.Y *= -1;
            }
        }
        */

        // Move with mousetracking
        /*
        private void MoveWithMouse()
        {
            MouseState state = Mouse.GetState();
            Vector2 mouseVector = new Vector2(state.X, state.Y);

            position += speed;
            var direction = mouseVector - position;
            direction.Normalize();
            direction = Vector2.Multiply(direction, 0.1f);
            speed += direction;
            speed = Limit(speed, 10);
        }
        */
    }
}
