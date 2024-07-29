using FirstGame.Managers;
using FirstGame.Sprites;
using FirstGame.Sprites.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace FirstGame.States
{
    internal class Level1State : GameState
    {
        private Hero hero;
        private Trap trap1;
        private Trap trap2;
        public Level1State(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            _tilemap = LoadMap("../../../Content/Backgrounds/Map1.csv");
        }
        public override void LoadContent()
        {
            _Textures = new List<Texture2D>();
            _tileTextures = _content.Load<Texture2D>("Tiles/tiles");
            _heroTexture = _content.Load<Texture2D>("Sprites/Hero");
            _bulletTexture = _content.Load<Texture2D>("Sprites/bullet");
            _trapTexture = _content.Load<Texture2D>("Sprites/Enemy_Muschroom(Trap)");
            _Textures.Add(_heroTexture);
            _Textures.Add(_bulletTexture);
            _Textures.Add(_trapTexture);
            bulletTemplate = new Bullet(_bulletTexture);
            hero = new Hero(_heroTexture, _window, bulletTemplate);
            trap1 = new Trap(_trapTexture, _window);
            trap2 = new Trap(_trapTexture, _window);

            sprites.Add(hero);
            sprites.Add(trap1);
            sprites.Add(trap2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            hero.Update(gameTime, sprites);
            trap1.Update(gameTime, sprites);
            trap2.Update(gameTime, sprites);

            bulletManager.Update(gameTime, sprites);
            // Remove inactive bullets
            sprites.RemoveAll(sprite => !sprite.IsActive);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            tileManager.Draw(spriteBatch, _tileTextures, _tilemap);
            bulletManager.Draw(spriteBatch, _Textures, sprites);
            trap1.Draw(spriteBatch, _Textures);
            trap2.Draw(spriteBatch, _Textures);
            hero.Draw(spriteBatch, _Textures);

            spriteBatch.End();
        }
    }
}
