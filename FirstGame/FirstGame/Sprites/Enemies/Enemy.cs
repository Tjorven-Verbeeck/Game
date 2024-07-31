using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FirstGame.Enemies
{
    public abstract class Enemy : Sprite
    {
        protected Random spawn = new Random();
        public Enemy(Texture2D texture) : base(texture)
        {
            IsActive = true;
            Health = 1; // initial health
            Damage = 1; // damage dealt by this enemy
        }

        public override abstract void Update(GameTime gameTime, List<Sprite> sprites);

        public override abstract void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0);

        public string RotateTowards(Vector2 targetPosition, Vector2 ownPosition)
        {
            string directionString;
            
            Vector2 direction = targetPosition - ownPosition;
            direction.Normalize();

            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                if (direction.X > 0)
                {
                    directionString = "Right";
                }
                else
                {
                    directionString = "Left";
                }
            }
            else
            {
                if (direction.Y > 0)
                {
                    directionString = "Forwards";
                }
                else
                {
                    directionString = "Backwards";
                }
            }
            return directionString;
        }

        public void CheckSpriteCollision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is Hero hero)
                {
                    if (new Rectangle(this.TextureRectangle.X, this.TextureRectangle.Y, 64, 64).Intersects(new Rectangle(sprite.TextureRectangle.X, sprite.TextureRectangle.Y, 64, 64)) && IsActive)
                    {
                        hero.TakeDamage(Damage, hero);
                        IsActive = false; // Deactivate enemy after collision
                    }
                }
            }
        }
    }
}
