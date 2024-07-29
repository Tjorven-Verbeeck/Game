using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame.Managers
{
    public class BulletManager
    {
        public BulletManager()
        {
        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (Sprite sprite in sprites)
            {
                if (sprite is Bullet bullet)
                {
                    if (!bullet.IsActive)
                    {
                        sprites.Remove(sprite);
                        continue;
                    }
                    bullet.Update(gameTime, sprites);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is Bullet bullet)
                {
                    bullet.Draw(spriteBatch, textures);
                }
            }
        }
    }
}
