using FirstGame.Controls;
using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using FirstGame.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace FirstGame.States
{
    class MenuState : State
    {
        private Texture2D menuBackGroundTexture;

        private Texture2D _buttonTexture;
        private SpriteFont _buttonfont;

        private Button exitButton;
        private Button startButton;

        private List<Button> _menuButtons;

        public MenuState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content): base(window, game, graphicsDevice, content)
        {
            // Buttons
            _buttonTexture = _content.Load<Texture2D>("Controls/GUI_Button");
            _buttonfont = _content.Load<SpriteFont>("Fonts/File");
            startButton = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth / 2 - 50, Game1._screenHeight / 2 - 100),
                Text = "Start",
            };
            exitButton = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth / 2 - 50, Game1._screenHeight / 2 - 25),
                Text = "Exit",
            };
            startButton.Click += StartButton_Click;
            exitButton.Click += ExitButton_Click;
            _menuButtons = new List<Button>()
            {
                startButton,
                exitButton
            };
        }
        
        public override void LoadContent()
        {
            // menu

            // background texture uploaden
            // menuBackGroundTexture = _content.Load<Texture2D>("Backgrounds/Menu");

            
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new GameState(_window, _game, _graphicsDevice, _content));
        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in _menuButtons)
                button.Update(gameTime);
        }

        public override void PostUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Button button in _menuButtons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
