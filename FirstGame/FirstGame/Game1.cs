using FirstGame.Controls;
using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _screenWidth = 800;
        private int _screenHeight = 400;

        private Texture2D _heroTexture;

        private List<Button> _menuButtons;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private Hero hero;
        private MouseReader mouseReader;
        public float scale = 3;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            

            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _heroTexture = Content.Load<Texture2D>("Sprites/Hero");
            hero = new Hero(_heroTexture, new Input.KeyboardReader());

            Button exitButton = new Button(Content.Load<Texture2D>("Controls/GUI_Button"), Content.Load<SpriteFont>("Fonts/File"), new Input.MouseReader())
            {
                Position = new Vector2(_screenWidth / 2 - 50, _screenHeight / 2 - 25),
                Text = "Exit",
            };

            exitButton.Click += ExitButton_Click;
            _menuButtons = new List<Button>()
            {
                exitButton
            };
        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            foreach (Button button in _menuButtons)
                button.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            foreach (Button button in _menuButtons)
                button.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}