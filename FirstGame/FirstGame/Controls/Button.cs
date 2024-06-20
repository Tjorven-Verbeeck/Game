using FirstGame.Input;
using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Controls
{
    internal class Button : Component
    {
        private SpriteFont _font;
        private Texture2D _texture;

        public event EventHandler Click;

        public IInputReader InputReader { get; set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width * 2, _texture.Height * 2); }
        }
        public String Text { get; set; }

        public Button(Texture2D texture, SpriteFont font, IInputReader inputReader)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.White;
            this.InputReader = inputReader;
        }

        public void Update(GameTime gameTime)
        { 
            if (InputReader is MouseReader mouseReader)
            {
                mouseReader.UpdateMouseState();
                Rectangle mouseRect = mouseReader.GetMouseCursor();
                Debug.WriteLine(Rectangle);
                Debug.WriteLine(mouseRect);
                if (mouseRect.Intersects(Rectangle))
                {
                    PenColor = Color.DarkGray;
                        if (mouseReader.WasLeftButtonReleased())
                        {
                            Click?.Invoke(this, new EventArgs());
                        }
                }
                else
                {
                    PenColor = Color.White;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);

            if (!string.IsNullOrEmpty(Text))
            {
                float x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                float y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }
    }
}
