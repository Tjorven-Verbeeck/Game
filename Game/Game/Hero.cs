using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Hero : IGameObject
    {
        private Texture2D texture;
        Animation animation;


        public Hero(Texture2D texture)
        {
            this.texture = texture;

            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(108, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(216, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(324, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(432, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(540, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(648, 0, 108, 140)));
            animation.AddFrame(new AnimationFrame(new Rectangle(756, 0, 108, 140)));
        }

        private Vector2 position = new Vector2(0, 0);
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, animation.CurrentFrame.SourceRectangle, Color.White);

        }

        public void update(GameTime gameTime)
        {
            animation.Update(gameTime);

            Move();
        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }


        private Vector2 speed = new Vector2(1, 1);
        private Vector2 speedUp = new Vector2(0.1f, 0.1f);
        private void Move()
        {
            position += speed;
            speed += speedUp;
            float maxSpeed = 10;
            speed = Limit(speed, maxSpeed);
            if (position.X + 140 > 800 || position.X < 0)
            {
                speed.X *= -1;
                speedUp.X *= -1;
            }
            if (position.Y + 108 > 480 || position.Y < 0)
            {
                speed.Y *= -1;
                speedUp.Y *= -1;
            }
        }

        








    }
}
