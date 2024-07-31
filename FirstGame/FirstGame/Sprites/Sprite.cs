using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace FirstGame.Sprites
{
    public class Sprite : IGameObject, IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction;
        public string TextureName { get; set; }
        public Rectangle textureSize { get; set; }
        public bool IsActive { get; set; }
        public Vector2 Speed { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }

        public Rectangle TextureRectangle => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public GameWindow window;
        public Sprite parent;
        public bool IsRemoved = false;

        // Health points
        public int Health { get; set; }

        protected int Damage { get; set; }

        // Fields for invulnerability and flickering
        protected bool isInvulnerable = false;
        protected float invulnerabilityTime = 1f; // Half a second
        protected float invulnerabilityTimer = 0f;
        protected bool isFlickering = false;
        protected float flickerRate = 0.1f; // Flicker every 0.1 seconds
        protected float flickerTimer = 0f;

        public Sprite(Texture2D texture)
        {
            TextureName = texture.Name;
            Width = texture.Width;
            Height = texture.Height;
            IsActive = true;
            Speed = Vector2.Zero;
            Health = 1; // Default health points
            Damage = 1; // Default damage points
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // Implement sprite update logic here
        }

        public virtual void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0)
        {

        }

        public void TakeDamage(int damage, Sprite target)
        {
            if (!target.isInvulnerable)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    IsActive = false; // Deactivate enemy when health drops to zero
                }
                if (target is Hero hero)
                {
                    isInvulnerable = true;
                    isFlickering = true;
                }
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        // Additional methods as needed
    }
}
