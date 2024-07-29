using FirstGame.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame.Sprites
{
    public class Bullet : Sprite
    {
        private TileManager tileManager = new TileManager();

        public Bullet(Texture2D texture) : base(texture) 
        {
            TextureName = texture.Name;
            Speed = new Vector2(500, 500);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!IsOnScreen(Position))
            {
                IsActive = false;
            }

            // Check for collisions with other sprites
            foreach (var sprite in sprites)
            {
                if (sprite == this.parent || sprite is Bullet)
                {
                    continue;
                } // Skip the parent to prevent self-hit
                //!BART
                if (sprite is Hero && this.parent is Enemy || sprite is Enemy && this.parent is Hero)
                {
                    if (bulletRectangle.Intersects(new Rectangle(sprite.TextureRectangle.X, sprite.TextureRectangle.Y, 64, 64)))
                    {
                        GetHit(sprite);
                        IsActive = false; // Deactivate bullet after collision
                    }
                }
            }
        }

        private void GetHit(Sprite sprite)
        {
            // Example: Apply damage or any other interaction logic
            if (sprite is Hero)
            {
                // Handle hero being hit by this bullet
                (sprite as Hero).TakeDamage(Damage); // Example damage amount
            }
            else if (sprite is Enemy)
            {
                // Handle enemy being hit by this bullet
                (sprite as Enemy).TakeDamage(Damage); // Example damage amount
            }
        }

        public override void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0)
        {
            float rotate = (float)Math.Atan2(Direction.Y, Direction.X) + MathHelper.PiOver2;
            foreach (Texture2D texture in textures)
            {
                if (texture.Name == TextureName)
                {
                    if (IsActive)
                    {
                        spriteBatch.Draw(texture, Position, null, Color.White, rotate, new Vector2( TextureRectangle.Width / 2, TextureRectangle.Height / 2), 0.05f, SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}
