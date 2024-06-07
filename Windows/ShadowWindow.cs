using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Thriple.Graphics;
using Thriple.Windows;

namespace Thriple.Objects
{
    public class ShadowWindow : IWindow
    {
        private const int SPRITE_POS_X = 0;
        private const int SPRITE_POS_Y = 0;
        private const int SPRITE_WIDTH = 1920;
        private const int SPRITE_HEIGHT = 1280;
        private const int POSITION_X = 0;
        private const int POSITION_Y = 0;


        private Texture2D _spriteSheet;
        private Vector2 Position { get; }
        private Sprite Sprite { get; }

        public ShadowWindow(Texture2D spriteSheet) 
        {
            _spriteSheet = spriteSheet;
            Position = new Vector2(POSITION_X, POSITION_Y);
            Sprite = new Sprite(spriteSheet, new Vector2(SPRITE_POS_X, SPRITE_POS_Y), SPRITE_WIDTH, SPRITE_HEIGHT);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position);
        }
    }
}
