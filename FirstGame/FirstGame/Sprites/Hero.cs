using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FirstGame.Input;
using FirstGame.Interfaces;
using FirstGame.Managers;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame.Sprites
{
    public class Hero : Sprite
    {
        private MovementManager movementManager = new MovementManager();
        private Animation heroForwards;
        private Animation heroBackwards;
        private Animation heroLeft;
        private Animation heroRight;

        public KeyboardReader keyboardReader;
        public MouseReader mouseReader;

        public Vector2 SpeedUp { get; set; } // Specific to Hero

        public Hero(Texture2D texture, GameWindow window, Bullet bulletTemplate) : base(texture)
        {
            this.TextureName = texture.Name;
            this.keyboardReader = new KeyboardReader();
            this.mouseReader = new MouseReader(window);
            this.BulletTemplate = bulletTemplate;

            // Initialize animations, positions, etc.
            InitializeAnimations();

            Position = new Vector2(100, 100);
            Speed = new Vector2(1, 1);
            SpeedUp = new Vector2(0.1f, 0.1f);
            Health = 5;
        }

        private void InitializeAnimations()
        {
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
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (isInvulnerable)
            {
                invulnerabilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                flickerTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (flickerTimer >= flickerRate)
                {
                    isFlickering = !isFlickering; // Toggle flickering state
                    flickerTimer = 0f;
                }

                if (invulnerabilityTimer >= invulnerabilityTime)
                {
                    isInvulnerable = false;
                    invulnerabilityTimer = 0f;
                    isFlickering = false; // Reset flickering
                }
            }

            Vector2 movementDirection = keyboardReader.ReadInput();

            mouseReader.UpdateMouseState();

            Move();

            UpdateAnimation(gameTime, movementDirection);

            if (mouseReader.WasLeftButtonReleased())
            {
                Vector2 targetPosition = mouseReader.GetMouseCursor().Location.ToVector2();
                AddBullet(sprites, targetPosition);
            }
        }

        private void UpdateAnimation(GameTime gameTime, Vector2 direction)
        {
            if (direction.X == -1)
                heroLeft.Update(gameTime);
            else if (direction.X == 1)
                heroRight.Update(gameTime);
            else if (direction.Y == -1)
                heroBackwards.Update(gameTime);
            else if (direction.Y == 1)
                heroForwards.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0)
        {
            Texture2D texture = textures[0];
            foreach (Texture2D item in textures)
            {
                if (item.Name == this.TextureName)
                {
                    texture = item;
                    break;
                }
            }

            // Apply a greyscale color if the hero is flickering
            Color color = isFlickering ? Color.Gray : Color.White;

            var input = keyboardReader.ReadInput();
            if (input.X == -1)
                spriteBatch.Draw(texture, Position, heroLeft.CurrentFrame.SourceRectangle, color, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
            else if (input.X == 1)
                spriteBatch.Draw(texture, Position, heroRight.CurrentFrame.SourceRectangle, color, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
            else if (input.Y == -1)
                spriteBatch.Draw(texture, Position, heroBackwards.CurrentFrame.SourceRectangle, color, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
            else
                spriteBatch.Draw(texture, Position, heroForwards.CurrentFrame.SourceRectangle, color, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
        }

        public void Move()
        {
            movementManager.MoveHero(this);
        }
    }
}
