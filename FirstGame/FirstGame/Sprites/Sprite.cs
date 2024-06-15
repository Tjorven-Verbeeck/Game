using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Sprites
{
    public class Sprite : IGameObject, IMovable
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsVisible { get; set; }
        public Vector2 Speed { get; set; }

        // Health points
        protected int HP { get; set; }

        // Alive status
        protected bool IsDead { get; set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            IsVisible = true;
            Speed = Vector2.Zero;
            HP = 1; // Default health points
            IsDead = false; // Default alive status
        }

        public virtual void Update(GameTime gameTime)
        {
            // Implement sprite update logic here
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }

        // Additional methods as needed
    }
}
