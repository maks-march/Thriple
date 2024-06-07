using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Thriple.Graphics;

namespace Thriple.Windows
{
    public class EndGame : IWindow
    {
        private const int SPRITE_POS_X = 0;
        private const int SPRITE_POS_Y = 862;
        private const int SPRITE_WIDTH = 1280;
        private const int SPRITE_HEIGHT = 1345 - SPRITE_POS_Y;
        private const int POSITION_X = 300;
        private const int POSITION_Y = 400;


        private Texture2D _spriteSheet;
        private Vector2 Position { get; }
        private Sprite Sprite { get; }


        public EndGame(Texture2D spriteSheet)
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
