using FirstGame.Controls;
using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using FirstGame.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        public static int _screenWidth = 1920;
        public static int _screenHeight = 1080;

        private State _currentState;
        private State _nextState;

        public static Random random;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.HardwareModeSwitch = false;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // States
            _currentState = new MenuState(Window, this, GraphicsDevice, Content);
            _currentState.LoadContent();
            _nextState = null;
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }
            _currentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }
    }
}