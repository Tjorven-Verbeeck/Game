using FirstGame.Input;
using FirstGame.Interfaces;
using FirstGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Characters
{
    public class Hero : IGameObject, IMovable
    {
        private Managers.MovementManager movementManager = new MovementManager();
        private Texture2D texture;
        private Animation heroForwards;
        private Animation heroBackwards;
        private Animation heroLeft;
        private Animation heroRight;
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
            inputReader = inputreader;

            heroForwards = new Animation();
            heroForwards.AddFrame(new AnimationFrame(new Rectangle(6, 0, 98, 106)));
            heroForwards.AddFrame(new AnimationFrame(new Rectangle(133, 0, 98, 106)));
            heroForwards.AddFrame(new AnimationFrame(new Rectangle(260, 0, 98, 106)));

            heroBackwards = new Animation();
            heroBackwards.AddFrame(new AnimationFrame(new Rectangle(0, 133, 98, 106)));
            heroBackwards.AddFrame(new AnimationFrame(new Rectangle(125, 133, 98, 106)));
            heroBackwards.AddFrame(new AnimationFrame(new Rectangle(256, 133, 98, 106)));

            heroRight = new Animation();
            heroRight.AddFrame(new AnimationFrame(new Rectangle(6, 266, 98, 100)));
            heroRight.AddFrame(new AnimationFrame(new Rectangle(133, 266, 98, 100)));
            heroRight.AddFrame(new AnimationFrame(new Rectangle(250, 266, 98, 100)));

            heroLeft = new Animation();
            heroLeft.AddFrame(new AnimationFrame(new Rectangle(6, 399, 98, 109)));
            heroLeft.AddFrame(new AnimationFrame(new Rectangle(133, 399, 98, 109)));
            heroLeft.AddFrame(new AnimationFrame(new Rectangle(250, 399, 98, 109)));

            position = new Vector2(10, 10);
            speed = new Vector2(1, 1);
            speedUp = new Vector2(0.1f, 0.1f);
        }


        public void draw(SpriteBatch spriteBatch)
        {
            if (inputReader.ReadInput().X == -1)
            {
                spriteBatch.Draw(texture, position, heroLeft.CurrentFrame.SourceRectangle, Color.White);
            }
            else if (inputReader.ReadInput().X == 1)
            {
                spriteBatch.Draw(texture, position, heroRight.CurrentFrame.SourceRectangle, Color.White);
            }
            else if (inputReader.ReadInput().Y == -1)
            {
                spriteBatch.Draw(texture, position, heroBackwards.CurrentFrame.SourceRectangle, Color.White);
            }
            else if (inputReader.ReadInput().Y == 1 || inputReader.ReadInput().X == 0 && inputReader.ReadInput().Y == 0)
            {
                spriteBatch.Draw(texture, position, heroForwards.CurrentFrame.SourceRectangle, Color.White);
            }
        }

        public void update(GameTime gameTime)
        {
            Move();

            if (inputReader.ReadInput().X == -1)
            {
                heroLeft.Update(gameTime);
            }
            else if (inputReader.ReadInput().X == 1)
            {
                heroRight.Update(gameTime);
            }
            else if (inputReader.ReadInput().Y == -1)
            {
                heroBackwards.Update(gameTime);
            }
            else if (inputReader.ReadInput().Y == 1)
            {
                heroForwards.Update(gameTime);
            }
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
