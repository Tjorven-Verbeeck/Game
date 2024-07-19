using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
        protected Dictionary<Vector2, int> _tilemap;
        protected List<Rectangle> _textureStore;
        private Texture2D tilesTexture;

        public GameState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            sprites = new List<Sprite>();
            bulletManager = new BulletManager();
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

        protected Dictionary<Vector2, int> LoadMap(string filePath)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filePath);

            int y = 0;
            string line;
            while((line= reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > 0)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
        }

        public override void LoadContent()
        {
            _Textures = new List<Texture2D>();
            tilesTexture = _content.Load<Texture2D>("Tiles/tiles");
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
            foreach (var item in _tilemap)
            {
                Rectangle dest = new(
                    (int)item.Key.X * 64,
                    (int)item.Key.Y * 64,
                    64,
                    64
                    );

                Rectangle src = _textureStore[item.Value - 1];

                spriteBatch.Draw(tilesTexture, dest, src, Color.White);
            }

            bulletManager.Draw(spriteBatch, _Textures, sprites);
            hero.Draw(spriteBatch, _Textures);

            spriteBatch.End();
        }
    }
}
