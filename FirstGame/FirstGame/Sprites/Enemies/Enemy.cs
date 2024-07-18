using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FirstGame.Enemies
{
    public abstract class Enemy : Sprite
    {
        public bool IsActive { get; set; } = true;
        public int Health { get; set; }
        public int Damage { get; set; }

        public Enemy(Texture2D texture) : base(texture)
        {
            Health = 100; // Example initial health
            Damage = 10; // Example damage dealt by this enemy
        }

        public abstract void Update(GameTime gameTime, List<Sprite> sprites);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                IsActive = false; // Deactivate enemy when health drops to zero
            }
        }
    }
}
