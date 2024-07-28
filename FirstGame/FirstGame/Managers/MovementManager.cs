using FirstGame.Interfaces;
using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Managers
{
    public class MovementManager
    {
        private Game1 game1Instance = new Game1();
        private TileManager tileManager = new TileManager();
        public void Move(IMovable movable)
        {
            // Use InputReader and SpeedUp if movable is Hero
            if (movable is Hero hero)
            {
                var direction = hero.keyboardReader.ReadInput();
                float maxSpeed = 10;

                hero.Speed = Limit(new Vector2(Math.Abs(hero.Speed.X), Math.Abs(hero.Speed.Y)), maxSpeed) * direction;
                if (direction.Equals(Vector2.Zero))
                {
                    hero.Speed = new Vector2(0, 0);
                }
                else
                {
                    hero.Speed += hero.SpeedUp * direction;
                }

                Rectangle heroRectangle = new Rectangle((int)hero.Position.X + (int)maxSpeed * (int)direction.X, (int)hero.Position.Y + (int)maxSpeed * (int)direction.Y, hero.textureSize.Width + 64, hero.textureSize.Height + 64);

                if (tileManager.IsCollidingWithTile(heroRectangle))
                {
                    hero.Speed = new Vector2(0, 0);
                }
                hero.Position += hero.Speed;
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
    }
}
