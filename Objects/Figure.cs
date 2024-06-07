using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Thriple.Objects
{
    public class Figure : IGameObject
    {
        private const int START_POS_X = 6;
        private const int START_POS_Y = 0;
        private const int POS_X_FOR_NEXT = 2;
        private const int POS_Y_FOR_NEXT = 2;
        private const int LEFT_MARGIN = 400;
        private const int BLOCK_WIDTH_HEIGHT = 80;
        private int BLOCK_TYPE_COUNT = EnumExpression.GetEnumMax(BlockType.Blue)-2;
        private int FIELD_TYPE_COUNT = EnumExpression.GetEnumMax(FigureType.Cube);

        private List<Block> _blocks;
        private Random _random;
        private Texture2D _spriteSheet;
        private List<int> _typesList;
        private bool _isCurrent;

        public int _startPosX;
        public int _startPosY;
        public int _leftMargin;
        public bool IsMovableRight { get; set; }
        public bool IsMovableLeft { get; set; }
        public bool IsMovableDown { get; set; }
        public bool IsCurrent 
        { 
            get { return _isCurrent; }
            set 
            {
                if (value)
                {
                    _startPosY = START_POS_Y; 
                    _startPosX = START_POS_X;
                    _leftMargin = LEFT_MARGIN;
                }
                else
                {
                    _startPosY = POS_Y_FOR_NEXT;
                    _startPosX = POS_X_FOR_NEXT;
                    _leftMargin = 0;
                }
                _isCurrent = value;
            } 
        }

        public IEnumerable<(int x, int y)> Positions 
        {
            get 
            {
                foreach (var block in _blocks)
                {
                    yield return block.Coordinates;
                }
            }
        }

        public bool IsClear { get { return _blocks.Count == 0; } }

        public IEnumerable<Block> Blocks
        {
            get
            {
                foreach (var block in _blocks)
                {
                    yield return block;
                }
            }
        }
        public int DrawOrder => 2;
        public FigureType Type { get; set; }

        public Figure(Texture2D spriteSheet, bool isCurrent)
        {
            _spriteSheet = spriteSheet;
            _random = new Random();
            _typesList = new List<int>();
            IsCurrent = isCurrent;
            BuildFigure();
        }

        public void BuildFigure()
        {
            IsMovableLeft = true;
            IsMovableRight = true;
            IsMovableDown = true;
            _blocks = new List<Block>(4);
            Type = (FigureType)_random.Next(0, FIELD_TYPE_COUNT);
            switch (Type)
            {
                case FigureType.Column:
                    BuildColumn();
                    break;
                case FigureType.Pin:
                    BuildPin();
                    break;
                case FigureType.Cube:
                    BuildCube();
                    break;
                case FigureType.LeftNail:
                    BuildRightNail();
                    break;
                case FigureType.RightNail:
                    BuildLeftNail();
                    break;
            }
        }

        private void BuildRightNail()
        {
            var position = new Vector2((_startPosX + 1) * BLOCK_WIDTH_HEIGHT + _leftMargin, _startPosY * BLOCK_WIDTH_HEIGHT);
            _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            for (int count = 0; count < 3; count++)
            {
                position = new Vector2(_startPosX * BLOCK_WIDTH_HEIGHT + _leftMargin, (_startPosY + count) * BLOCK_WIDTH_HEIGHT);
                _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            }
        }
        
        private void BuildLeftNail()
        {
            var position = new Vector2((_startPosX - 1) * BLOCK_WIDTH_HEIGHT + _leftMargin, _startPosY * BLOCK_WIDTH_HEIGHT);
            _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            for (int count = 0; count < 3; count++)
            {
                position = new Vector2(_startPosX * BLOCK_WIDTH_HEIGHT + _leftMargin, (_startPosY + count) * BLOCK_WIDTH_HEIGHT);
                _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            }
        }

        private void BuildCube()
        {
            for (int count = 0; count < 2; count++)
            {
                var position = new Vector2(_startPosX * BLOCK_WIDTH_HEIGHT + _leftMargin, (_startPosY + count) * BLOCK_WIDTH_HEIGHT);
                _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            }
            for (int count = 1; count >= 0; count--)
            {
                var position = new Vector2((_startPosX + 1) * BLOCK_WIDTH_HEIGHT + _leftMargin, (_startPosY + count) * BLOCK_WIDTH_HEIGHT);
                _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            }
        }

        private void BuildPin()
        {
            var position = new Vector2(_startPosX * BLOCK_WIDTH_HEIGHT + _leftMargin, _startPosY * BLOCK_WIDTH_HEIGHT);
            _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            for (int count = -1; count < 2; count++)
            {
                position = new Vector2((_startPosX + count) * BLOCK_WIDTH_HEIGHT + _leftMargin, (_startPosY + 1) * BLOCK_WIDTH_HEIGHT);
                _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            }
        }

        private void BuildColumn()
        {
            for (int count = 0; count < 4; count++)
            {
                var position = new Vector2(_startPosX * BLOCK_WIDTH_HEIGHT + _leftMargin, (_startPosY + count) * BLOCK_WIDTH_HEIGHT);
                _blocks.Add(new Block(_spriteSheet, GetNextType(), position));
            }
        }

        private BlockType GetNextType()
        {
            if (_typesList.Count == 0)
            {
                _typesList = new List<int>();
                for (int i = 1; i <= BLOCK_TYPE_COUNT; i++)
                {
                    _typesList.Add(i);
                }
            }
            var next = _random.Next(0, _typesList.Count);
            var result = (BlockType)_typesList[next];
            _typesList.RemoveAt(next);
            return result;
        }

        public void ChangeToCurrent(IEnumerable<Block> blocks)
        {
            Clear();
            IsMovableLeft = true;
            IsMovableRight = true;
            IsMovableDown = true;
            foreach (var block in blocks) 
            {
                var position = block.Position;
                position.X = ((int)(position.X / BLOCK_WIDTH_HEIGHT) - POS_X_FOR_NEXT + START_POS_X) * BLOCK_WIDTH_HEIGHT + LEFT_MARGIN;
                position.Y = ((int)(position.Y / BLOCK_WIDTH_HEIGHT) - POS_Y_FOR_NEXT + START_POS_Y) * BLOCK_WIDTH_HEIGHT;
                block.Position = position;
                _blocks.Add(block);
            }
        }

        public void ChangeColors()
        {
            var firstType = _blocks[0].Type;
            for (int count = 0; count < _blocks.Count-1; count++)
            {
                _blocks[count].ChangeBlock(_blocks[count+1].Type);
            }
            _blocks[^1].ChangeBlock(firstType);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var block in _blocks)
            {
                block.Draw(spriteBatch, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsCurrent)
                foreach (var block in _blocks)
                {
                    block.MoveDown();
                    block.Update(gameTime);
                }
        }

        public void Clear()
        {
            _blocks.Clear();
        }

        public bool MoveRight()
        {
            if (IsMovableRight)
            {
                foreach (var block in _blocks)
                {
                    block.MoveRight();
                }
                return true;
            }
            return false;
        }
        
        public bool MoveDown()
        {
            if (IsMovableDown)
            {
                foreach (var block in _blocks)
                {
                    block.MoveDown();
                }
                return true;
            }
            return false;
        }

        public bool MoveLeft()
        {
            if (IsMovableLeft)
            {
                foreach (var block in _blocks)
                {
                    block.MoveLeft();
                }
                return true;
            }
            return false;
        }
    }
}
