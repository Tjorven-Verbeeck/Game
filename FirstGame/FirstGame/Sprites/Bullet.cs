using FirstGame.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame.Sprites
{
    public class Bullet : Sprite
    {
        public bool IsActive { get; set; } = true; // Initial state is active

        public Bullet(Texture2D texture) : base(texture) { }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Speed = new Vector2(500, 500);

            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!IsOnScreen(Position))
            {
                IsActive = false; // Deactivate if out of bounds
            }

            // Check for collisions with other sprites
            foreach (var sprite in sprites)
            {
                if (sprite == this.parent) continue; // Skip the parent to prevent self-hit

                if (sprite is Hero && this.parent is Enemy || sprite is Enemy && this.parent is Hero)
                {
                    if (this.Rectangle.Intersects(sprite.Rectangle))
                    {
                        HandleCollision(sprite);
                        IsActive = false; // Deactivate bullet after collision
                    }
                }
            }
        }

        private void HandleCollision(Sprite sprite)
        {
            // Example: Apply damage or any other interaction logic
            if (sprite is Hero)
            {
                // Handle hero being hit by this bullet
                (sprite as Hero).takeDamage(10); // Example damage amount
            }
            else if (sprite is Enemy)
            {
                // Handle enemy being hit by this bullet
                (sprite as Enemy).TakeDamage(10); // Example damage amount
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float rotation = (float)Math.Atan2(Direction.Y, Direction.X) + MathHelper.PiOver2;
            spriteBatch.Draw(Texture, Position, null, Color.White, rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), 0.05f, SpriteEffects.None, 0f);
        }

        private bool IsOnScreen(Vector2 position)
        {
            int screenWidth = 1920;
            int screenHeight = 1080;

            return position.X >= 0 && position.X <= screenWidth && position.Y >= 0 && position.Y <= screenHeight;
        }
    }
}
