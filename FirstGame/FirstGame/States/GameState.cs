using FirstGame.Input;
using FirstGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.States
{
    internal class GameState : State
    {
        private List<Sprite> sprites;

        private Hero hero;

        private Texture2D _heroTexture;

        public GameState(GameWindow window, Game1 game, GraphicsDevice graphicsDevice , ContentManager content) : base(window, game, graphicsDevice, content)
        {

        }

        public override void LoadContent()
        {
            // Hero
            _heroTexture = _content.Load<Texture2D>("Sprites/Hero");
            hero = new Hero(_heroTexture, new KeyboardReader());
        }
        
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_window, _game, _graphicsDevice, _content));
            hero.Update(gameTime);
        }

        public override void PostUpdate()
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            hero.Draw(spriteBatch);
            spriteBatch.End();
        }

        

        

        
    }
}
