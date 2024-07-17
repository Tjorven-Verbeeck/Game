using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame.States
{
    internal class GameState : State
    {
        private List<Sprite> sprites;
        private Texture2D _heroTexture;
        private Texture2D _bulletTexture;
        private List<Texture2D> _Textures;
        private Hero hero;
        private Bullet bulletTemplate;
        private BulletManager bulletManager;

        public GameState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            sprites = new List<Sprite>();
            bulletManager = new BulletManager();
        }

        public override void LoadContent()
        {
            _Textures = new List<Texture2D>();
            _heroTexture = _content.Load<Texture2D>("Sprites/Hero");
            _bulletTexture = _content.Load<Texture2D>("Sprites/bullet");
            _Textures.Add(_heroTexture);
            _Textures.Add(_bulletTexture);
            bulletTemplate = new Bullet(_bulletTexture);
            hero = new Hero(_heroTexture, _window, bulletTemplate);

            sprites.Add(hero);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_window, _game, _graphicsDevice, _content));

            hero.Update(gameTime, sprites);
            bulletManager.Update(gameTime, sprites);
            // Remove inactive bullets
            sprites.RemoveAll(sprite => sprite is Bullet bullet && !bullet.IsActive);
        }

        public override void PostUpdate()
        {
            // Implement any necessary post-update logic
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            bulletManager.Draw(spriteBatch, _Textures, sprites);
            hero.Draw(spriteBatch, _Textures);

            spriteBatch.End();
        }
    }
}
