using FirstGame.Enemies;
using FirstGame.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FirstGame.Sprites.Enemies
{
    internal class Turret : Enemy
    {
        private Animation turretForwards;
        private Animation turretBackwards;
        private Animation turretLeft;
        private Animation turretRight;
        private Animation currentAnimation;

        private double timer; // Timer for controlling firing rate
        private double fireRate = 0.75; // Fire rate in seconds (0.5 seconds)

        public Bullet BulletTemplate { get; set; }

        public Turret(Texture2D texture, GameWindow window, Bullet bulletTemplate) : base(texture)
        {
            this.TextureName = texture.Name;
            this.BulletTemplate = bulletTemplate;
            InitializeAnimations();

            Position = new Vector2(spawn.Next(200, 1720), spawn.Next(200, 880));
            //Rectangle trapRectangle = new Rectangle((int)Position.X, (int)Position.Y, this.textureSize.Width, this.textureSize.Height);
            Health = 1;
            timer = 0; // Initialize the timer
        }

        private void InitializeAnimations()
        {
            turretForwards = new Animation();
            turretForwards.AddFrame(new AnimationFrame(new Rectangle(1, 2, 32, 32)));
            turretForwards.AddFrame(new AnimationFrame(new Rectangle(36, 2, 32, 32)));
            turretForwards.AddFrame(new AnimationFrame(new Rectangle(71, 2, 32, 32)));

            turretBackwards = new Animation();
            turretBackwards.AddFrame(new AnimationFrame(new Rectangle(1, 39, 32, 32)));
            turretBackwards.AddFrame(new AnimationFrame(new Rectangle(36, 39, 32, 32)));
            turretBackwards.AddFrame(new AnimationFrame(new Rectangle(71, 39, 32, 32)));

            turretRight = new Animation();
            turretRight.AddFrame(new AnimationFrame(new Rectangle(1, 76, 32, 32)));
            turretRight.AddFrame(new AnimationFrame(new Rectangle(36, 76, 32, 32)));
            turretRight.AddFrame(new AnimationFrame(new Rectangle(71, 76, 32, 32)));

            turretLeft = new Animation();
            turretLeft.AddFrame(new AnimationFrame(new Rectangle(1, 113, 32, 32)));
            turretLeft.AddFrame(new AnimationFrame(new Rectangle(36, 113, 32, 32)));
            turretLeft.AddFrame(new AnimationFrame(new Rectangle(71, 113, 32, 32)));

            currentAnimation = turretForwards; // Default animation
        }

        public override void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0)
        {
            foreach (Texture2D texture in textures)
            {
                if (texture.Name == this.TextureName)
                {
                    if (IsActive)
                    {
                        spriteBatch.Draw(texture, Position, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // Assuming the player is the first sprite in the list
            var player = sprites.FirstOrDefault();
            if (player != null)
            {
                RotateTowards(new Vector2(player.Position.X + 32, player.Position.Y + 32));
            }

            currentAnimation.Update(gameTime);

            timer += gameTime.ElapsedGameTime.TotalSeconds;

            // Check if the turret is alive
            if (IsActive)
            {
                if (timer >= fireRate)
                {

                    Vector2 targetPosition = new Vector2(player.Position.X + 32, player.Position.Y + 32);
                    AddBullet(sprites, targetPosition);

                    // Reset the timer after firing
                    timer = 0;
                }
            }
        }

        private void RotateTowards(Vector2 targetPosition)
        {
            Vector2 direction = targetPosition - Position;
            direction.Normalize();

            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                if (direction.X > 0)
                {
                    currentAnimation = turretRight;
                }
                else
                {
                    currentAnimation = turretLeft;
                }
            }
            else
            {
                if (direction.Y > 0)
                {
                    currentAnimation = turretForwards;
                }
                else
                {
                    currentAnimation = turretBackwards;
                }
            }
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
            bullet.parent = this; // Set the parent to this enemy

            sprites.Add(bullet);
        }
    }
}
