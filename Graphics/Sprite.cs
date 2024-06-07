using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thriple.Graphics
{
    public class Sprite
    {
        public Sprite(Texture2D spriteSheet, int x, int y, int width, int height) 
        {
            SpriteSheet = spriteSheet;
            X = x;
            Y = y; 
            Width = width;   
            Height = height;
        }

        public Sprite(Texture2D spriteSheet, Vector2 position, int width, int height)
        {
            SpriteSheet = spriteSheet;
            X = (int)position.X;
            Y = (int)position.Y;
            Width = width;
            Height = height;
        }

        public Texture2D SpriteSheet { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Color TintColor { get; set; } = Color.White;

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(SpriteSheet, position, new Rectangle(X, Y, Width, Height), TintColor);
        }
    }
}
