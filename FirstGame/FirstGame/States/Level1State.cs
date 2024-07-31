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
        private Hero hero;
        private Trap trap1;
        private Trap trap2;
        private Trap trap3;
        private Trap trap4;
        private Trap trap5;
        private Turret turret1;
        private Kamikaze kamikaze1;
        private Button healthButton;
        private List<Button> _level1Buttons;
        private GameWindow window;
        public Level1State(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            _tilemap = LoadMap("../../../Content/Backgrounds/Map1.csv");

            this.window = window;
        }
        public override void LoadContent()
        {
            _Textures = new List<Texture2D>();
            _tileTextures = _content.Load<Texture2D>("Tiles/tiles");
            _buttonTexture = _content.Load<Texture2D>("Controls/GUI_Button");
            _buttonfont = _content.Load<SpriteFont>("Fonts/File");
            _heroTexture = _content.Load<Texture2D>("Sprites/Hero");
            _bulletTexture = _content.Load<Texture2D>("Sprites/bullet");
            _trapTexture = _content.Load<Texture2D>("Sprites/Enemy_Muschroom(Trap)");
            _turretTexture = _content.Load<Texture2D>("Sprites/Enemy_Mushroom(Turret)");
            _kamikazeTexture = _content.Load<Texture2D>("Sprites/Enemy_Skull(Kamikaze)");
            _Textures.Add(_heroTexture);
            _Textures.Add(_bulletTexture);
            _Textures.Add(_trapTexture);
            _Textures.Add(_turretTexture);
            _Textures.Add(_kamikazeTexture);
            bulletTemplate = new Bullet(_bulletTexture);
            hero = new Hero(_heroTexture, _window, bulletTemplate);
            trap1 = new Trap(_trapTexture, _window);
            trap2 = new Trap(_trapTexture, _window);
            trap3 = new Trap(_trapTexture, _window);
            trap4 = new Trap(_trapTexture, _window);
            trap5 = new Trap(_trapTexture, _window);
            turret1 = new Turret(_turretTexture, _window, bulletTemplate);
            kamikaze1 = new Kamikaze(_kamikazeTexture, _window);

            healthButton = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth - 200, 100),
                Text = hero.Health + " HP",
            };
            _level1Buttons = new List<Button>()
            {
                healthButton
            };

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

            bulletManager.Update(gameTime, sprites);
            // Remove inactive bullets
            sprites.RemoveAll(sprite => !sprite.IsActive);

            foreach (Button button in _level1Buttons)
            {
                if(button == healthButton)
                {
                    button.SetText(hero.Health + " HP");
                }
            }
            if(hero.IsActive == false)
            {
                // lost state
                _game.ChangeState(new GameOverState(_window, _game, _graphicsDevice, _content));
            }
            bool enemyFound = false;
            foreach (var sprite in sprites)
            {
                if(sprite is Enemy)
                {
                    enemyFound = true;
                }
            }
            if (!enemyFound)
            {
                // won state
                _game.ChangeState(new MenuState(_window, _game, _graphicsDevice, _content));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            tileManager.Draw(spriteBatch, _tileTextures, _tilemap);
            bulletManager.Draw(spriteBatch, _Textures, sprites);
            foreach (Button button in _level1Buttons)
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
