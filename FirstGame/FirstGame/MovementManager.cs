using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    public class MovementManager
    {
        public void Move(IMovable movable)
        {
            var direction = movable.InputReader.ReadInput();

            var distance = direction * movable.Speed;
            var futurePosition = movable.Position + distance;
            if (futurePosition.X + 140 > 800 || futurePosition.X < 0 || futurePosition.Y + 108 > 480 || futurePosition.Y < 0)
            {
                movable.Position = futurePosition;
            }
            movable.Position += distance;
        }
    }
}
