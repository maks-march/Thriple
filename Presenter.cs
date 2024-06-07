using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Thriple.Controls;
using Thriple.Objects;
using Thriple.Windows;

namespace Thriple
{
    public class Presenter : Game
    {
        private const string ASSET_NAME_SPRITESHEET = "sprites";
        private const string ASSET_NAME_SHADOWSHEET = "shadow";
        private const string ASSET_NAME_SFX_CLICKSOUND = "click";
        private const string ASSET_NAME_SFX_GAMEOVER = "gameover";
        private const string ASSET_NAME_SFX_THEME = "theme";

        private const int WINDOW_WIDTH = 1920;
        private const int WINDOW_HEIGHT = 1280;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SoundEffect _sfxClick;
        private SoundEffectInstance _sfxGameover;
        private SoundEffectInstance _sfxTheme;

        private Texture2D _spriteSheet;
        private Texture2D _shadowSheet;

        private KeyboardState _previousKeyboardState;

        private ObjectsModel _model;
        private Controller _controller;
        private IWindow _startWindow;
        private IWindow _endWindow;
        private IWindow _shadowWindow;

        private bool IsGameoverPlays = false;

        public Presenter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _sfxClick = Content.Load<SoundEffect>(ASSET_NAME_SFX_CLICKSOUND);

            var gameoverSFX = Content.Load<SoundEffect>(ASSET_NAME_SFX_GAMEOVER);
            _sfxGameover = gameoverSFX.CreateInstance();

            var theme = Content.Load<SoundEffect>(ASSET_NAME_SFX_THEME);
            _sfxTheme = theme.CreateInstance();
            _sfxTheme.IsLooped = true;

            _shadowSheet = Content.Load<Texture2D>(ASSET_NAME_SHADOWSHEET);
            _spriteSheet = Content.Load<Texture2D>(ASSET_NAME_SPRITESHEET);

            _model = new ObjectsModel(_spriteSheet, _sfxClick);
            _model.DrawOrder = 10;

            _controller = new Controller(_model.Figure);

            _startWindow = new StartGame(_spriteSheet);
            _endWindow = new EndGame(_spriteSheet);
            _shadowWindow = new ShadowWindow(_shadowSheet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
            KeyboardState keyboardState = Keyboard.GetState();

            if (_previousKeyboardState != keyboardState)
            {
                if (_model.GameState == GameState.Off)
                {
                     _sfxTheme.Play();
                    _model.Clear();
                    _model.Initialize();
                    IsGameoverPlays = false;
                    _sfxGameover.Stop();
                }
                if (_model.GameState == GameState.Start)
                {
                    _model.Initialize();
                }
                if (_model.GameState == GameState.On && !_model.Figure.IsClear)
                {

                    _controller.ProcessControlls(keyboardState);
                    //_sfxClick.Play();
                }
            }
            _previousKeyboardState = keyboardState;
            _model.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            _model.Draw(_spriteBatch, gameTime);

            if (_model.GameState == GameState.Start)
            {
                _shadowWindow.Draw(_spriteBatch, gameTime);
                _startWindow.Draw(_spriteBatch, gameTime);
                _sfxTheme.Play();
            }
            if (_model.GameState == GameState.Off)
            {
                _shadowWindow.Draw(_spriteBatch, gameTime);
                _endWindow.Draw(_spriteBatch, gameTime);
                _sfxTheme.Stop();
                if (!IsGameoverPlays)
                {
                    _sfxGameover.Play();
                    IsGameoverPlays = true;
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}