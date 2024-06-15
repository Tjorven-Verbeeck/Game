using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FirstGame.Input;
using FirstGame.Interfaces;
using FirstGame.Managers;
using SharpDX.Direct3D9;

namespace FirstGame.Sprites
{
    public class Hero : Sprite
    {
        private MovementManager movementManager = new MovementManager();
        private Animation heroForwards;
        private Animation heroBackwards;
        private Animation heroLeft;
        private Animation heroRight;

        public IInputReader InputReader { get; set; } // Specific to Hero

        public Vector2 SpeedUp { get; set; } // Specific to Hero

        public Hero(Texture2D texture, IInputReader inputReader) : base(texture)
        {
            this.Texture = texture;
            this.InputReader = inputReader;

            // Initialize animations, positions, etc.
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

            Position = new Vector2(10, 10);
            Speed = new Vector2(1, 1);
            SpeedUp = new Vector2(0.1f, 0.1f);

        }

        public override void Update(GameTime gameTime)
        {
            // Implement hero-specific update logic
            Move();

            if (InputReader.ReadInput().X == -1)
            {
                heroLeft.Update(gameTime);
            }
            else if (InputReader.ReadInput().X == 1)
            {
                heroRight.Update(gameTime);
            }
            else if (InputReader.ReadInput().Y == -1)
            {
                heroBackwards.Update(gameTime);
            }
            else if (InputReader.ReadInput().Y == 1)
            {
                heroForwards.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Implement hero-specific draw logic
            if (InputReader.ReadInput().X == -1)
            {
                spriteBatch.Draw(Texture, Position, heroLeft.CurrentFrame.SourceRectangle, Color.White);
            }
            else if (InputReader.ReadInput().X == 1)
            {
                spriteBatch.Draw(Texture, Position, heroRight.CurrentFrame.SourceRectangle, Color.White);
            }
            else if (InputReader.ReadInput().Y == -1)
            {
                spriteBatch.Draw(Texture, Position, heroBackwards.CurrentFrame.SourceRectangle, Color.White);
            }
            else if (InputReader.ReadInput().Y == 1 || InputReader.ReadInput().X == 0 && InputReader.ReadInput().Y == 0)
            {
                spriteBatch.Draw(Texture, Position, heroForwards.CurrentFrame.SourceRectangle, Color.White);
            }
        }

        public void Move()
        {
            movementManager.Move(this);
        }

        public void ChangeInputReader(IInputReader inputReader)
        {
            this.InputReader = inputReader;
        }

        // Additional hero-specific methods and properties
    }
}
