using FirstGame.Enemies;
using FirstGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Sprites.Enemies
{
    internal class Kamikaze : Enemy
    {
        private MovementManager movementManager = new MovementManager();

        private Animation kamikazeForwards;
        private Animation kamikazeBackwards;
        private Animation kamikazeLeft;
        private Animation kamikazeRight;
        private Animation currentAnimation;

        public Kamikaze(Texture2D texture, GameWindow window) : base(texture)
        {
            this.TextureName = texture.Name;
            InitializeAnimations();

            do
            {
                Position = new Vector2(spawn.Next(200, 1720), spawn.Next(200, 880));
            }
            while (Position.X > 1650 && Position.Y < 370);

            Speed = new Vector2(1, 1);
            Health = 1;
            Damage = 3;
        }

        private void InitializeAnimations()
        {
            kamikazeForwards = new Animation();
            kamikazeForwards.AddFrame(new AnimationFrame(new Rectangle(1, 1, 32, 32)));
            kamikazeForwards.AddFrame(new AnimationFrame(new Rectangle(36, 1, 32, 32)));
            kamikazeForwards.AddFrame(new AnimationFrame(new Rectangle(71, 1, 32, 32)));

            kamikazeBackwards = new Animation();
            kamikazeBackwards.AddFrame(new AnimationFrame(new Rectangle(1, 36, 32, 32)));
            kamikazeBackwards.AddFrame(new AnimationFrame(new Rectangle(36, 36, 32, 32)));
            kamikazeBackwards.AddFrame(new AnimationFrame(new Rectangle(71, 36, 32, 32)));

            kamikazeRight = new Animation();
            kamikazeRight.AddFrame(new AnimationFrame(new Rectangle(1, 71, 32, 32)));
            kamikazeRight.AddFrame(new AnimationFrame(new Rectangle(36, 71, 32, 32)));
            kamikazeRight.AddFrame(new AnimationFrame(new Rectangle(71, 71, 32, 32)));

            kamikazeLeft = new Animation();
            kamikazeLeft.AddFrame(new AnimationFrame(new Rectangle(1, 106, 32, 32)));
            kamikazeLeft.AddFrame(new AnimationFrame(new Rectangle(36, 106, 32, 32)));
            kamikazeLeft.AddFrame(new AnimationFrame(new Rectangle(71, 106, 32, 32)));

            currentAnimation = kamikazeForwards; // Default animation
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
                string directionString = RotateTowards(new Vector2(player.Position.X + 32, player.Position.Y + 32), new Vector2(Position.X + 32, Position.Y + 32));
                switch (directionString)
                {
                    case "Right":
                        currentAnimation = kamikazeRight;
                        break;
                    case "Left":
                        currentAnimation = kamikazeLeft;
                        break;
                    case "Forwards":
                        currentAnimation = kamikazeForwards;
                        break;
                    case "Backwards":
                        currentAnimation = kamikazeBackwards;
                        break;
                }
            }

            Move(sprites);

            currentAnimation.Update(gameTime);

            // Check for collisions with other sprites
            foreach (var sprite in sprites)
            {
                if (sprite is Hero)
                {
                    if (new Rectangle(this.TextureRectangle.X, this.TextureRectangle.Y, 64, 64).Intersects(new Rectangle(sprite.TextureRectangle.X, sprite.TextureRectangle.Y, 64, 64)) && IsActive)
                    {
                        (sprite as Hero).TakeDamage(Damage);
                        IsActive = false; // Deactivate trap after collision
                        Debug.WriteLine(sprite.Health);
                    }
                }
            }
        }

        

        public void Move(List<Sprite> sprites)
        {
            if (sprites.FirstOrDefault() is Hero hero)
            {
                movementManager.MoveEnemy(this, hero);
            }            
        }
    }
}
