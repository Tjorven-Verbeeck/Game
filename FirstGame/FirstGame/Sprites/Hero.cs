using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FirstGame.Input;
using FirstGame.Interfaces;
using FirstGame.Managers;
using SharpDX.Direct3D9;
using SharpDX.Direct2D1.Effects;

namespace FirstGame.Sprites
{
    public class Hero : Sprite
    {
        private Game1 game1Instance = new Game1();
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
            heroForwards.AddFrame(new AnimationFrame(new Rectangle(1, 1, 32, 32)));
            heroForwards.AddFrame(new AnimationFrame(new Rectangle(37, 1, 32, 32)));
            heroForwards.AddFrame(new AnimationFrame(new Rectangle(73, 1, 32, 32)));

            heroBackwards = new Animation();
            heroBackwards.AddFrame(new AnimationFrame(new Rectangle(2, 39, 32, 32)));
            heroBackwards.AddFrame(new AnimationFrame(new Rectangle(38, 39, 32, 32)));
            heroBackwards.AddFrame(new AnimationFrame(new Rectangle(74, 39, 32, 32)));

            heroRight = new Animation();
            heroRight.AddFrame(new AnimationFrame(new Rectangle(3, 75, 32, 32)));
            heroRight.AddFrame(new AnimationFrame(new Rectangle(38, 75, 32, 32)));
            heroRight.AddFrame(new AnimationFrame(new Rectangle(73, 75, 32, 32)));

            heroLeft = new Animation();
            heroLeft.AddFrame(new AnimationFrame(new Rectangle(3, 112, 32, 32)));
            heroLeft.AddFrame(new AnimationFrame(new Rectangle(38, 112, 32, 32)));
            heroLeft.AddFrame(new AnimationFrame(new Rectangle(73, 112, 32, 32)));

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
                spriteBatch.Draw(Texture, Position, heroLeft.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, game1Instance.scale, SpriteEffects.None, 0f);
            }
            else if (InputReader.ReadInput().X == 1)
            {
                spriteBatch.Draw(Texture, Position, heroRight.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, game1Instance.scale, SpriteEffects.None, 0f);
            }
            else if (InputReader.ReadInput().Y == -1)
            {
                spriteBatch.Draw(Texture, Position, heroBackwards.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, game1Instance.scale, SpriteEffects.None, 0f);
            }
            else if (InputReader.ReadInput().Y == 1 || InputReader.ReadInput().X == 0 && InputReader.ReadInput().Y == 0)
            {
                spriteBatch.Draw(Texture, Position, heroForwards.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, game1Instance.scale, SpriteEffects.None, 0f);
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
