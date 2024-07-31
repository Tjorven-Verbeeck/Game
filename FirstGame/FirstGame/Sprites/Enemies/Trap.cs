using FirstGame.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Sprites.Enemies
{
    internal class Trap : Enemy
    {
        private Animation trapAnimation;

        public Trap(Texture2D texture, GameWindow window) : base(texture)
        {
            this.TextureName = texture.Name;

            InitializeAnimations();

            Position = new Vector2(spawn.Next(200,1720), spawn.Next(200, 880));
            Health = 1;
            Damage = 3;
        }

        private void InitializeAnimations()
        {
            trapAnimation = new Animation();
            trapAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 1, 16, 16)));
            trapAnimation.AddFrame(new AnimationFrame(new Rectangle(22, 1, 16, 16)));
            trapAnimation.AddFrame(new AnimationFrame(new Rectangle(42, 1, 16, 16)));
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            trapAnimation.Update(gameTime);

            CheckSpriteCollision(sprites);
        }

        public override void Draw(SpriteBatch spriteBatch, List<Texture2D> textures, float rotation = 0)
        {
            foreach (Texture2D texture in textures)
            {
                if (texture.Name == this.TextureName)
                {
                    if (IsActive)
                    {
                        spriteBatch.Draw(texture, Position, trapAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 4, SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}
