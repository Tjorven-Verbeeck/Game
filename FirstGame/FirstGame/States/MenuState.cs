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
        private Button level1Button;
        private Button level2Button;

        private List<Button> _menuButtons;

        public MenuState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content): base(window, game, graphicsDevice, content)
        {
            // Buttons
            _buttonTexture = _content.Load<Texture2D>("Controls/GUI_Button");
            _buttonfont = _content.Load<SpriteFont>("Fonts/File");
            level1Button = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth / 2 - 50, Game1._screenHeight / 2 - 175),
                Text = "Level1",
            };
            level2Button = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth / 2 - 50, Game1._screenHeight / 2 - 100),
                Text = "Level2",
            };
            exitButton = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth / 2 - 50, Game1._screenHeight / 2 - 25),
                Text = "Exit",
            };
            level1Button.Click += Level1Button_Click;
            level2Button.Click += Level2Button_Click;
            exitButton.Click += ExitButton_Click;
            _menuButtons = new List<Button>()
            {
                level1Button,
                level2Button,
                exitButton
            };
        }
        
        public override void LoadContent()
        {
            // menu

            // background texture uploaden
            // menuBackGroundTexture = _content.Load<Texture2D>("Backgrounds/Menu");

            
        }

        private void Level1Button_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new Level1State(_window, _game, _graphicsDevice, _content));
        }
        private void Level2Button_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new Level2State(_window, _game, _graphicsDevice, _content));
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Button button in _menuButtons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
