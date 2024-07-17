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
        public bool IsVisible { get; set; }
        public Vector2 Speed { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }

        public Rectangle TextureRectangle => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public GameWindow window;
        public Sprite parent;
        public bool IsRemoved = false;

        // Health points
        protected int HP { get; set; }

        // Alive status
        protected bool IsDead { get; set; }

        public Sprite(Texture2D texture)
        {
            TextureName = texture.Name;
            Width = texture.Width;
            Height = texture.Height;
            IsVisible = true;
            Speed = Vector2.Zero;
            HP = 1; // Default health points
            IsDead = false; // Default alive status
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // Implement sprite update logic here
        }

        public virtual void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0)
        {
            foreach (Texture2D texture in textures)
            {
                if (texture.Name == TextureName)
                {
                    if (IsVisible)
                    {
                        spriteBatch.Draw(texture, Position, null, Color.White, rotation, new Vector2(Width / 2, Height / 2), 0.05f, SpriteEffects.None, 0f);
                    }
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
