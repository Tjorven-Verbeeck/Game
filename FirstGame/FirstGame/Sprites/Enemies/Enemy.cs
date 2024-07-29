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
            Health = 5; // initial health
            Damage = 1; // damage dealt by this enemy
        }

        public override abstract void Update(GameTime gameTime, List<Sprite> sprites);

        public override abstract void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0);

        
    }
}
