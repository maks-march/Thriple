using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using Thriple.Graphics;

namespace Thriple.Objects
{
    public class Block : IGameObject
    {
        private const float TIME_STEP_DEL = 0.15f;

        private const int BLOCK_WIDTH_HEIGHT = 80;
        private const int LEFT_MARGIN = 400;

        private Vector2 _position;

        private BlockSprites _blockSprites;

        private SpriteAnimation _deleteAnimation;

        private bool _isDelPlay = false;

        public Sprite GetIdleSprite { get { return _blockSprites.IdleSprite; } }
        public bool IsPlaying { get { return _isDelPlay; } }
        public BlockType Type { get; private set; }
        public int DrawOrder { get; private set; } = 5;
        public Sprite Sprite { get; private set; }
        public Vector2 Position 
        { 
            get 
            { 
                return new Vector2(_position.X, _position.Y); 
            }

            set
            {
                _position = value;
            }
        }



        public (int x,int y) Coordinates
        {
            get
            {
                var x = (int)(_position.X - LEFT_MARGIN) / BLOCK_WIDTH_HEIGHT;
                var y = ((int)_position.Y / BLOCK_WIDTH_HEIGHT);
                return (x, y);
            }
        }
        public Block(Texture2D spriteSheet, BlockType type, Vector2 position)
        {
            _blockSprites = new BlockSprites(spriteSheet);
            Type = type;
            this.ChangeBlock(Type);
            _position = position;

            _deleteAnimation = new SpriteAnimation(TIME_STEP_DEL, Sprite, _blockSprites.IdleSprite);
            _deleteAnimation.ShouldLoop = false;
            _deleteAnimation.AddFrame(_blockSprites.IdleSprite, 0);
            _deleteAnimation.AddFrame(_blockSprites.WhiteSprite, TIME_STEP_DEL);
            _deleteAnimation.AddFrame(Sprite, TIME_STEP_DEL * 2);
            _deleteAnimation.AddFrame(_blockSprites.WhiteSprite, TIME_STEP_DEL * 3);
            _deleteAnimation.AddFrame(Sprite, TIME_STEP_DEL * 4);
            _deleteAnimation.AddFrame(_blockSprites.WhiteSprite, TIME_STEP_DEL * 5);

        }

        public void Update(GameTime gameTime)
        {
            if (_deleteAnimation.IsPlaying)
                _deleteAnimation.Update(gameTime);
            else
                _isDelPlay = _deleteAnimation.IsPlaying;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_isDelPlay)
                _deleteAnimation.Draw(spriteBatch, Position);
            else
                Sprite.Draw(spriteBatch, _position);
        }

        public void ChangeBlock(BlockType type)
        {
            Type = type;
            Sprite = _blockSprites.GetByType(Type);
            _deleteAnimation = new SpriteAnimation(TIME_STEP_DEL, Sprite, _blockSprites.IdleSprite);
        }

        public void Play()
        {
            _isDelPlay = true;
            _deleteAnimation.Play();
        }

        public void MoveDown()
        {
            _position.Y += BLOCK_WIDTH_HEIGHT;
        }

        public void MoveLeft()
        {
            _position.X -= BLOCK_WIDTH_HEIGHT;
        }

        public void MoveRight()
        {
            _position.X += BLOCK_WIDTH_HEIGHT;
        }
    }
}