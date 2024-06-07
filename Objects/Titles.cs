using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thriple.Graphics;

namespace Thriple.Objects
{
    public class TitleNext : IGameObject
    {
        private const int TITLE_SPRITE_POS_X = 0;
        private const int TITLE_SPRITE_POS_Y = 1515;
        private const int TITLE_SPRITE_HEIGHT = 90;
        private const int TITLE_SPRITE_WIDTH = 150;
        private const int TITLE_POS_X = 40;
        private const int TITLE_POS_Y = 40;

        private Sprite _sprite;
        private Vector2 _position;
        public int DrawOrder => 10;

        public TitleNext(Texture2D spriteSheet)
        {
            _position = new Vector2(TITLE_POS_X, TITLE_POS_Y);
            _sprite = new Sprite(spriteSheet, TITLE_SPRITE_POS_X, TITLE_SPRITE_POS_Y, TITLE_SPRITE_WIDTH, TITLE_SPRITE_HEIGHT);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, _position);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
    
    public class TitleScore : IGameObject
    {
        private const int TITLE_SPRITE_POS_X = 150;
        private const int TITLE_SPRITE_POS_Y = 1515;
        private const int TITLE_SPRITE_HEIGHT = 90;
        private const int TITLE_SPRITE_WIDTH = 200;
        private const int TITLE_POS_X = 1450;
        private const int TITLE_POS_Y = 40;

        private Sprite _sprite;
        private Vector2 _position;
        public int DrawOrder => 10;

        public TitleScore(Texture2D spriteSheet)
        {
            _position = new Vector2(TITLE_POS_X, TITLE_POS_Y);
            _sprite = new Sprite(spriteSheet, TITLE_SPRITE_POS_X, TITLE_SPRITE_POS_Y, TITLE_SPRITE_WIDTH, TITLE_SPRITE_HEIGHT);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, _position);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
    public class TitleHiScore : IGameObject
    {
        private const int TITLE_SPRITE_POS_X = 352;
        private const int TITLE_SPRITE_POS_Y = 1515;
        private const int TITLE_SPRITE_HEIGHT = 90;
        private const int TITLE_SPRITE_WIDTH = 400;
        private const int TITLE_POS_X = 1450;
        private const int TITLE_POS_Y = 280;

        private Sprite _sprite;
        private Vector2 _position;
        public int DrawOrder => 10;

        public TitleHiScore(Texture2D spriteSheet)
        {
            _position = new Vector2(TITLE_POS_X, TITLE_POS_Y);
            _sprite = new Sprite(spriteSheet, TITLE_SPRITE_POS_X, TITLE_SPRITE_POS_Y, TITLE_SPRITE_WIDTH, TITLE_SPRITE_HEIGHT);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, _position);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
