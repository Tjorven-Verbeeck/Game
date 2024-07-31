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
    internal class GameWonState : State
    {
        private Texture2D _buttonTexture;
        private SpriteFont _buttonfont;

        private Button gameOverButton;
        private Button menuButton;

        private List<Button> _menuButtons;

        public GameWonState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(window, game, graphicsDevice, content)
        {
            // I did not put this code and the GameLostState code in a sepperate file because i maay want to change the screens at a later point in time sto defferentiate more than what it is now.
            _buttonTexture = _content.Load<Texture2D>("Controls/GUI_Button");
            _buttonfont = _content.Load<SpriteFont>("Fonts/File");
            gameOverButton = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth / 2 - 50, Game1._screenHeight / 2 - 175),
                Text = "Game won",
            };
            menuButton = new Button(_buttonTexture, _buttonfont, new MouseReader(window))
            {
                Position = new Vector2(Game1._screenWidth / 2 - 50, Game1._screenHeight / 2 - 100),
                Text = "Menu",
            };
            menuButton.Click += MenuButton_Click;
            _menuButtons = new List<Button>()
            {
                gameOverButton,
                menuButton,
            };
        }

        public override void LoadContent()
        {
            // menu

            // background texture uploaden
            // menuBackGroundTexture = _content.Load<Texture2D>("Backgrounds/Menu");


        }

        private void MenuButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_window, _game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in _menuButtons)
                if (button == menuButton)
                {
                    button.Update(gameTime);
                }
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

