using FirstGame.Controls;
using FirstGame.Input;
using FirstGame.Managers;
using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace FirstGame.States
{
    public abstract class State
    {
        protected GameWindow _window;
        protected Game1 _game;
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _content;

        public State(GameWindow window, Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _window = window;
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void PostUpdate();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
