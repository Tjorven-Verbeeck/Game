using FirstGame.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    public class Hero : IGameObject, IMovable
    {
        private MovementManager movementManager = new MovementManager();
        private Texture2D texture;
        private Animation animation;
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private Vector2 speedUp;
        public Vector2 SpeedUp
        {
            get { return speedUp; }
            set { speedUp = value; }
        }
        private IInputReader inputReader;
        public IInputReader InputReader
        {
            get { return inputReader; }
            set { inputReader = value; }
        }

        public Hero(Texture2D texture, IInputReader inputreader)
        {
            this.texture = texture;
            this.inputReader = inputreader;

            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(108, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(216, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(324, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(432, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(540, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(648, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(756, 0, 108, 140)));

            position = new Vector2(10, 10);
            speed = new Vector2(1, 1);
            speedUp = new Vector2(0.1f, 0.1f);
        }

        
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, animation.CurrentFrame.SourceRectangle, Color.White);

        }

        public void update(GameTime gameTime)
        {
            Move();

            animation.Update(gameTime);
        }

        public void Move()
        {
            movementManager.Move(this);
        }

        public void ChangeInputReader(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }
    }
}
