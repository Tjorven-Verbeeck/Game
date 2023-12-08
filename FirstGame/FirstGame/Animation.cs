using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    public class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        private double secondcounter = 0;
        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];
            secondcounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 6;

            if (secondcounter > 1d / fps)
            {
                counter++;
                secondcounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }
    }
}
