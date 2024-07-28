using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Managers
{
    internal class TileManager
    {
        private List<Rectangle> _textureStore;

        public TileManager()
        {
            _textureStore = new()
            {
                new Rectangle(1,0,32,32), // hoek links boven
                new Rectangle(206,0,32,32), // hoek rechts boven
                new Rectangle(1,205,32,32), // hoek links onder
                new Rectangle(206,205,32,32), // hoek rechts onder
                new Rectangle(103,37,32,32), // connector boven gesloten
                new Rectangle(106,171,32,32), // connector onder gesloten
                new Rectangle(35,106,32,32), // connector links gesloten
                new Rectangle(171,104,32,32), // connector rechts gesloten
                new Rectangle(172,0,32,32), // muur boven links
                new Rectangle(36,0,32,32), // muur boven rechts
                new Rectangle(172,205,32,32), // muur onder links
                new Rectangle(36,205,32,32), // muur onder rechts
                new Rectangle(1,34,32,32), // muur links boven
                new Rectangle(1,171,32,32), // muur links onder
                new Rectangle(206,34,32,32), // muur rechts boven
                new Rectangle(206,171,32,32), // muur rechts onder
                new Rectangle(106,110,32,32), // grond
            };
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tileTextures, Dictionary<Vector2, int> tilemap)
        {
            foreach (var item in tilemap)
            {
                Rectangle dest = new(
                    (int)item.Key.X * 64,
                    (int)item.Key.Y * 64,
                    64,
                    64
                    );

                Rectangle src = _textureStore[item.Value - 1];

                spriteBatch.Draw(tileTextures, dest, src, Color.White);
            }
        }
    }
}
