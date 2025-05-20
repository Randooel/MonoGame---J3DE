using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Threading;

namespace TargetGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D _targetSprite;
        Texture2D _skySprite;
        SpriteFont _gameFont;

        MouseState _mState;
        bool _mReleased;
        int _score = 0;

        double _timer = 10;
        double _timerAbs;

        #region TARGET VARIABLES

        Vector2 _targetPosition = new Vector2(300, 300);
        const int _targetRadius = 45;

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loading sprites's respective textures
            _targetSprite = Content.Load<Texture2D>("target");
            _skySprite = Content.Load<Texture2D>("sky");

            // Lodiang the font archive into the SpriteFont variable
            _gameFont = Content.Load<SpriteFont>("galleryFont");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_timer > 0)
            {
                _timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if(_timer < 0)
            {
                _timer = 0;
                Exit();
            }
            
            _timerAbs = Math.Abs(_timer);

            _mState = Mouse.GetState();

            if(_mState.LeftButton == ButtonState.Pressed && _mReleased == true)
            {
                float mouseTargetDist = Vector2.Distance(_targetPosition, _mState.Position.ToVector2());

                if(mouseTargetDist < _targetRadius)
                {
                    _score++;

                    Random rand = new Random();

                    _targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth);
                    _targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight);
                }
                
                _mReleased = false;
            }
            if(_mState.LeftButton == ButtonState.Released)
            {
                _mReleased = true;
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_skySprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_targetSprite, new Vector2(_targetPosition.X - _targetRadius, _targetPosition.Y - _targetRadius), Color.White);
            _spriteBatch.DrawString(_gameFont, _score.ToString(), new Vector2(10, 10), Color.Black);
            _spriteBatch.DrawString(_gameFont, Math.Ceiling(_timer/*Abs*/).ToString(), new Vector2(10, 50), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}