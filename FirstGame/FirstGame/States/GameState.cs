using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using FirstGame.Sprites.Enemies;
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
        protected List<Sprite> sprites;
        protected Texture2D _heroTexture;
        protected Texture2D _bulletTexture;
        protected Texture2D _trapTexture;
        protected List<Texture2D> _Textures;
        protected Bullet bulletTemplate;
        protected BulletManager bulletManager;
        protected TileManager tileManager;
        public static Dictionary<Rectangle, int> _tilemap;

        protected Texture2D _tileTextures;

        public GameState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            sprites = new List<Sprite>();
            bulletManager = new BulletManager();
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_window, _game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
