using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thriple.Graphics;

namespace Thriple.Objects
{

    public class ObjectsModel : IGameObject
    {
        private Blocks _blocks;

        private Figure _figure;
        private Figure _nextFigure;

        private TitleNext _nextTitle;
        private TitleScore _scoreTitle;
        private TitleHiScore _hiScoreTitle;

        private ScoreBoard _scoreBoard;

        private ObjectsManager _objectsManager;

        private Texture2D _spriteSheet;

        public GameState GameState { get; set; }

        public Figure Figure { get { return _figure; } }
        public ObjectsModel(Texture2D spriteSheet, SoundEffect sfxMove) 
        {
            _spriteSheet = spriteSheet;
            _blocks = new Blocks(_spriteSheet);
            _figure = new Figure(_spriteSheet, true);
            _nextFigure = new Figure(_spriteSheet, false);

            _nextTitle = new TitleNext(_spriteSheet);
            _scoreTitle = new TitleScore(_spriteSheet);
            _hiScoreTitle = new TitleHiScore(_spriteSheet);

            _scoreBoard = new ScoreBoard(_spriteSheet);

            GameState = GameState.Start;
            _objectsManager = new ObjectsManager();

            _objectsManager.AddObject(_blocks);
            _objectsManager.AddObject(_nextTitle);
            _objectsManager.AddObject(_scoreTitle);
            _objectsManager.AddObject(_hiScoreTitle);

            _objectsManager.AddObject(_scoreBoard);
        }

        public void Initialize()
        {
            GameState = GameState.On;
            _figure.BuildFigure();
            _nextFigure.BuildFigure();
            _objectsManager.AddObject(_figure);
            _objectsManager.AddObject(_nextFigure);
        }

        public int DrawOrder { get; set; }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _objectsManager.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            if (GameState == GameState.On || GameState == GameState.Start)
            {
                CheckFigure();

                _scoreBoard.Score = _blocks.Score;

                _objectsManager.Update(gameTime);
            }
        }

        private void CheckFigure()
        {
            foreach (var coords in _figure.Positions)
            {
                if (coords.x <= 0 || _blocks[coords.x - 1, coords.y] != BlockType.Idle)
                    _figure.IsMovableLeft = false;
                if (coords.x >= _blocks.Size.width-1 || _blocks[coords.x + 1, coords.y] != BlockType.Idle)
                    _figure.IsMovableRight = false;


                if (coords.y >= _blocks.Size.height - 1 || _blocks[coords.x, coords.y+1] != BlockType.Idle)
                {
                    _figure.IsMovableDown = false;
                    if (!_blocks.PrintFigure(_figure.Blocks))
                    {
                        _figure.Clear();
                        _nextFigure.Clear();
                        GameState = GameState.Off;
                        return;
                    }
                    _figure.ChangeToCurrent(_nextFigure.Blocks);
                    _nextFigure.BuildFigure();
                    break;
                }
            }
        }

        public void Clear()
        {
            _blocks.Clear();
            _figure.Clear();
            _nextFigure.Clear();
            _scoreBoard.Clear();
            _objectsManager.DeleteObject(_figure);
            _objectsManager.DeleteObject(_nextFigure);
        }
    }
}
