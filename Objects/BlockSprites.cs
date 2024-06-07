using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thriple.Graphics;

namespace Thriple.Objects
{
    public class BlockSprites
    {
        private const int YELLOW_BLOCKSPRITE_X = 0;
        private const int GREEN_BLOCKSPRITE_X = 80;
        private const int BLUE_BLOCKSPRITE_X = 160;
        private const int RED_BLOCKSPRITE_X = 240;
        private const int IDLE_BLOCKSPRITE_X = 320;
        private const int WHITE_BLOCKSPRITE_X = 400;

        private const int BLOCK_WIDTH_HEIGHT = 80;
        private const int BLOCKSPRITE_Y = 0;

        public Sprite YellowSprite { get; }
        public Sprite GreenSprite { get; }
        public Sprite BlueSprite { get; }
        public Sprite RedSprite { get; }
        public Sprite WhiteSprite { get; }
        public Sprite IdleSprite { get; }

        public BlockSprites(Texture2D spriteSheet)
        {
            IdleSprite = new Sprite(spriteSheet, IDLE_BLOCKSPRITE_X, BLOCKSPRITE_Y, BLOCK_WIDTH_HEIGHT, BLOCK_WIDTH_HEIGHT);
            GreenSprite = new Sprite(spriteSheet, GREEN_BLOCKSPRITE_X, BLOCKSPRITE_Y, BLOCK_WIDTH_HEIGHT, BLOCK_WIDTH_HEIGHT);
            RedSprite = new Sprite(spriteSheet, RED_BLOCKSPRITE_X, BLOCKSPRITE_Y, BLOCK_WIDTH_HEIGHT, BLOCK_WIDTH_HEIGHT);
            BlueSprite = new Sprite(spriteSheet, BLUE_BLOCKSPRITE_X, BLOCKSPRITE_Y, BLOCK_WIDTH_HEIGHT, BLOCK_WIDTH_HEIGHT);
            YellowSprite = new Sprite(spriteSheet, YELLOW_BLOCKSPRITE_X, BLOCKSPRITE_Y, BLOCK_WIDTH_HEIGHT, BLOCK_WIDTH_HEIGHT);
            WhiteSprite = new Sprite(spriteSheet, WHITE_BLOCKSPRITE_X, BLOCKSPRITE_Y, BLOCK_WIDTH_HEIGHT, BLOCK_WIDTH_HEIGHT);
        }

        public Sprite GetByType(BlockType type)
        {
            switch (type)
            {
                case BlockType.Red:
                    return RedSprite;
                case BlockType.Green:
                    return GreenSprite;
                case BlockType.Blue:
                    return BlueSprite;
                case BlockType.Yellow:
                    return YellowSprite;
                case BlockType.White:
                    return WhiteSprite;
                case BlockType.Idle:
                    return IdleSprite;
                default:
                    return null;
            }
        }
    }
}