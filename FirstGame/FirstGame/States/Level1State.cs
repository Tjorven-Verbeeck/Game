using FirstGame.Controls;
using FirstGame.Enemies;
using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using FirstGame.Sprites.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.States
{
    internal class Level1State : GameState
    {
        private Trap trap1;
        private Trap trap2;
        private Trap trap3;
        private Trap trap4;
        private Trap trap5;
        private Turret turret1;
        private Kamikaze kamikaze1;


        public Level1State(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            _tilemap = LoadMap("../../../Content/Backgrounds/Map1.csv");
        }
        public override void LoadContent()
        {
            base.LoadContent();
            trap1 = new Trap(_trapTexture, _window);
            trap2 = new Trap(_trapTexture, _window);
            trap3 = new Trap(_trapTexture, _window);
            trap4 = new Trap(_trapTexture, _window);
            trap5 = new Trap(_trapTexture, _window);
            turret1 = new Turret(_turretTexture, _window, bulletTemplate);
            kamikaze1 = new Kamikaze(_kamikazeTexture, _window);

            sprites.Add(hero);
            sprites.Add(trap1);
            sprites.Add(trap2);
            sprites.Add(trap3);
            sprites.Add(trap4);
            sprites.Add(trap5);
            sprites.Add(turret1);
            sprites.Add(kamikaze1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            hero.Update(gameTime, sprites);
            trap1.Update(gameTime, sprites);
            trap2.Update(gameTime, sprites);
            trap3.Update(gameTime, sprites);
            trap4.Update(gameTime, sprites);
            trap5.Update(gameTime, sprites);
            turret1.Update(gameTime, sprites);
            kamikaze1.Update(gameTime, sprites);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            tileManager.Draw(spriteBatch, _tileTextures, _tilemap);
            bulletManager.Draw(spriteBatch, _Textures, sprites);
            foreach (Button button in _levelButtons)
                button.Draw(spriteBatch);
            trap1.Draw(spriteBatch, _Textures);
            trap2.Draw(spriteBatch, _Textures);
            trap3.Draw(spriteBatch, _Textures);
            trap4.Draw(spriteBatch, _Textures);
            trap5.Draw(spriteBatch, _Textures);
            turret1.Draw(spriteBatch, _Textures);
            kamikaze1.Draw(spriteBatch, _Textures);

            hero.Draw(spriteBatch, _Textures);

            spriteBatch.End();
        }
    }
}
