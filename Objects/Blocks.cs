using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using Thriple.Graphics;

namespace Thriple.Objects
{
    public class Blocks : IGameObject, IInstantObject
    {
        private int FIELD_WIDTH = 13;
        private int FIELD_HEIGHT = 15;

        private int LEFT_MARGIN = 400;
        private const int BLOCK_WIDTH_HEIGHT = 80;

        private BlockSprites _blockSprites;

        private BlockType[,] _blocksTable;

        private List<Block> _toUpdate = new List<Block>();
        private List<Block> _toRemoveUpdate = new List<Block>();
        private Texture2D _spriteSheet;

        public int Score { get; private set; }
        public (int width, int height) Size 
        {
            get 
            {
                return (FIELD_WIDTH, FIELD_HEIGHT);
            } 
        }
        

        public Blocks(Texture2D spriteSheet) 
        {
            _spriteSheet = spriteSheet;
            _blocksTable = new BlockType[FIELD_WIDTH, FIELD_HEIGHT]; 
            _spriteSheet = spriteSheet;
            _blockSprites = new BlockSprites(spriteSheet);

        }

        public BlockType this[int x, int y]
        {
            get
            {
                return _blocksTable[x,y];
            }
            set
            {
                _blocksTable[x, y] = value;
            }
        }

        public Block GetBlock(int x, int y)
        {
            return new Block(_spriteSheet, _blocksTable[x, y], new Vector2(x * BLOCK_WIDTH_HEIGHT + LEFT_MARGIN, y * BLOCK_WIDTH_HEIGHT));
        }

        public int DrawOrder => 1;

        public IEnumerable<Block> GetBlocks
        {
            get
            {
                for (int x = FIELD_WIDTH - 1; x >= 0; x--)
                    for (int y = FIELD_HEIGHT - 1; y >= 0; y--)
                        if (_blocksTable[x,y] != BlockType.Idle)
                            yield return GetBlock(x, y);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int x = FIELD_WIDTH - 1; x >= 0; x--)
                for (int y = FIELD_HEIGHT - 1; y >= 0; y--)
                    _blockSprites.GetByType(_blocksTable[x,y]).Draw(spriteBatch, new Vector2(x * BLOCK_WIDTH_HEIGHT + LEFT_MARGIN, y * BLOCK_WIDTH_HEIGHT));

            foreach (var block in _toUpdate)
            {
                block.Draw(spriteBatch, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            InstantUpdate(gameTime);
            var blocks = GetThriples();
            Score += blocks.Length;
            foreach (var block in blocks)
            {
                _blocksTable[block.Coordinates.x, block.Coordinates.y] = BlockType.Idle;
                block.Play();
                _toUpdate.Add(block);
            }
            foreach (var block in GetBlocks)
            {
                var coords = block.Coordinates;

                if (coords.y + 1 < FIELD_HEIGHT && _blocksTable[coords.x, coords.y + 1] == BlockType.Idle)
                {
                    _blocksTable[coords.x, coords.y + 1] = block.Type;
                    _blocksTable[coords.x, coords.y] = BlockType.Idle;
                }
            }
        }

        public void InstantUpdate(GameTime gameTime)
        {
            foreach (var item in _toUpdate)
            {
                item.Update(gameTime);
                if (!item.IsPlaying)
                    _toRemoveUpdate.Add(item);
            }
            foreach (var item in _toRemoveUpdate)
            {
                _toUpdate.Remove(item);
            }
            _toRemoveUpdate.Clear();
        }

        private Block[] GetThriples()
        {
            var blocks = new List<Block>();

            foreach (var block in GetBlocks)
            {
                blocks.AddRange(GetAroundBlocks(block));
            }

            return blocks.ToArray();
        }

        private Block[] GetAroundBlocks(Block block)
        {
            var blocks = new List<Block>();
            var x = block.Coordinates.x;
            var y = block.Coordinates.y - 1;
            var opposite = 2 * x;
            for (int i = x-1; i <= x+1; i++)
            {
                if (opposite - i < FIELD_WIDTH && 
                        i < FIELD_WIDTH && 
                        i >= 0 && 
                        opposite - i >= 0 && 
                        y + 2 < FIELD_HEIGHT && 
                        y >= 0)
                    if (_blocksTable[i, y] == block.Type && _blocksTable[opposite-i, y + 2] == block.Type)
                    {
                        blocks.Add(GetBlock(i, y));
                        blocks.Add(GetBlock(opposite - i, y + 2));
                    }
            }
            if (x + 1 < FIELD_WIDTH && x-1 >= 0 && y + 1 < FIELD_HEIGHT && y + 1 >= 0)
                if (_blocksTable[x - 1, y + 1] == block.Type && _blocksTable[x + 1, y + 1] == block.Type)
                {
                    blocks.Add(GetBlock(x - 1, y + 1));
                    blocks.Add(GetBlock(x + 1, y + 1));
                }
            if (blocks.Count > 0)
            {
                blocks.Add(block);
            }
            return blocks.ToArray();
        }

        public bool PrintFigure(IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
            {
                var coords = block.Coordinates;
                 if (_blocksTable[coords.x, coords.y] != BlockType.Idle)
                    return false;
                _blocksTable[coords.x, coords.y] = block.Type;
            }
            return true;
        }

        public void Clear()
        {
            Score = 0;
            for (int x = FIELD_WIDTH - 1; x >= 0; x--)
                for (int y = FIELD_HEIGHT - 1; y >= 0; y--)
                    _blocksTable[x, y] = BlockType.Idle;
        }
    }
}