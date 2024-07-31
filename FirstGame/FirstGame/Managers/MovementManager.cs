using FirstGame.Enemies;
using FirstGame.Interfaces;
using FirstGame.Sprites;
using FirstGame.Sprites.Enemies;
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
        public void MoveHero(IMovable movable)
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
            } }
        public void MoveEnemy(IMovable movable, Hero hero)
        {
            if (movable is Kamikaze kamikaze)
            {
                var direction = hero.Position - kamikaze.Position;
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }

                // Set the Kamikaze's speed based on the direction
                float maxSpeed = 5f; // You can adjust this value for faster or slower movement
                kamikaze.Speed = direction * maxSpeed;

                // Calculate the new position with collision detection
                Rectangle kamikazeRectangle = new Rectangle(
                    (int)(kamikaze.Position.X + kamikaze.Speed.X),
                    (int)(kamikaze.Position.Y + kamikaze.Speed.Y),
                    kamikaze.textureSize.Width + 64,
                    kamikaze.textureSize.Height + 64
                );

                // Check for collision with tiles
                if (tileManager.IsCollidingWithTile(kamikazeRectangle))
                {
                    // Stop the Kamikaze if there's a collision
                    kamikaze.Speed = Vector2.Zero;
                }
                else
                {
                    // Update Kamikaze's position
                    kamikaze.Position += kamikaze.Speed;
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
    }
}
