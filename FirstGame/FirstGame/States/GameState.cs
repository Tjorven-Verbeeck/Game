using FirstGame.Controls;
using FirstGame.Enemies;
using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using FirstGame.Sprites.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        protected List<SoundEffect> _Sounds;
        protected SoundEffect _winSound;
        protected List<Sprite> sprites;
        protected Texture2D _heroTexture;
        protected Texture2D _bulletTexture;
        protected Texture2D _trapTexture;
        protected Texture2D _turretTexture;
        protected Texture2D _kamikazeTexture;
        protected List<Texture2D> _Textures;
        protected Hero hero;
        protected Bullet bulletTemplate;
        protected BulletManager bulletManager;
        protected TileManager tileManager;
        protected Texture2D _buttonTexture;
        protected SpriteFont _buttonfont;
        protected Button healthButton;
        protected List<Button> _levelButtons;

        public static Dictionary<Rectangle, int> _tilemap;

        protected Texture2D _tileTextures;

        private GameWindow window;

        public GameState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            sprites = new List<Sprite>();
            bulletManager = new BulletManager();
            _tilemap = new Dictionary<Rectangle, int>();
            tileManager = new TileManager();
            this.window = window;
        }

        protected Dictionary<Rectangle, int> LoadMap(string filePath)
        {
            Dictionary<Rectangle, int> result = new();

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
                            result[new Rectangle(x * 64, y * 64, 64, 64)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
        }

        public override void LoadContent()
        {
            _Sounds = new List<SoundEffect>();
            _winSound = _content.Load<SoundEffect>("Sounds/Win");
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

            healthButton = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth - 200, 100),
                Text = hero.Health + " HP",
            };
            _levelButtons = new List<Button>()
            {
                healthButton
            };
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_window, _game, _graphicsDevice, _content));

            bulletManager.Update(gameTime, sprites);
            // Remove inactive bullets
            sprites.RemoveAll(sprite => !sprite.IsActive);

            foreach (Button button in _levelButtons)
            {
                if (button == healthButton)
                {
                    button.SetText(hero.Health + " HP");
                }
            }
            if (hero.IsActive == false)
            {
                // lost state
                _game.ChangeState(new GameOverState(_window, _game, _graphicsDevice, _content));
            }
            bool enemyFound = false;
            foreach (var sprite in sprites)
            {
                if (sprite is Enemy)
                {
                    enemyFound = true;
                }
            }
            if (!enemyFound)
            {
                // won state
                _winSound.Play();
                _game.ChangeState(new GameWonState(_window, _game, _graphicsDevice, _content));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
