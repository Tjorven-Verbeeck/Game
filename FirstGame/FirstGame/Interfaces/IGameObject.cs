using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Interfaces
{
    public interface IGameObject
    {
        void Update(GameTime gameTime, List<Sprite> sprites);
        void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0);
    }
}
