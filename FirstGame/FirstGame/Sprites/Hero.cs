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

        public bool HasDied = false;

        public KeyboardReader keyboardReader;
        public MouseReader mouseReader;

        public Vector2 SpeedUp { get; set; } // Specific to Hero
        public Bullet BulletTemplate { get; set; }

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
            Texture2D tex = textures[0];
            foreach (Texture2D item in textures)
            {
                if (item.Name == this.TextureName)
                {
                    tex = item;
                    break;
                }
            }

            var input = keyboardReader.ReadInput();
            if (input.X == -1)
                spriteBatch.Draw(tex, Position, heroLeft.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
            else if (input.X == 1)
                spriteBatch.Draw(tex, Position, heroRight.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
            else if (input.Y == -1)
                spriteBatch.Draw(tex, Position, heroBackwards.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
            else
                spriteBatch.Draw(tex, Position, heroForwards.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
        }

        private void AddBullet(List<Sprite> sprites, Vector2 targetPosition)
        {
            Bullet bullet = BulletTemplate.Clone() as Bullet;
            bullet.Position = this.Position;

            Vector2 direction = targetPosition - bullet.Position;
            if (direction != Vector2.Zero)
                direction.Normalize();

            bullet.Direction = direction;
            bullet.Speed = new Vector2(500, 500);
            bullet.parent = this; // Set the parent to this hero

            sprites.Add(bullet);
        }

        public void Move()
        {
            movementManager.Move(this);
        }

        public void takeDamage(int damage)
        {
            HP -= damage;
            Debug.WriteLine(HP);
        }

    }
}
