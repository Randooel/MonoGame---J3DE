using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceshipGame.Content;
using System;

namespace SpaceshipGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #region SPACE STUFF
        Texture2D space;
        #endregion

        Ship ship = new Ship();
        Timer timer = new Timer();
        Asteroid asteroid = new Asteroid(250);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            ship.position.X = _graphics.PreferredBackBufferWidth / 2;
            ship. position.Y = _graphics.PreferredBackBufferHeight / 2;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ship.sprite = Content.Load<Texture2D>("ship");
            asteroid.sprite = Content.Load<Texture2D>("asteroid");
            space = Content.Load<Texture2D>("space");

            ship.spaceFont = Content.Load<SpriteFont>("spaceFont");
            timer.timerFont = Content.Load<SpriteFont>("timerFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // SHIP LOGIC
            ship.ShipUdpate(gameTime);

            // TIMER LOGIC
            timer.TimerUpdate(gameTime);

            // ASTEROID LOGIC
            asteroid.AsteroidUpdate(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(space, new Vector2(0, 0), Color.White);
            // Centers the sprite into the intended point. It does so by subtracting half of the sprite's height and width
            _spriteBatch.Draw(ship.sprite, new Vector2(ship.position.X -34, ship.position.Y -50), Color.White);
            _spriteBatch.Draw(asteroid.sprite, new Vector2(asteroid.position.X - asteroid.radius, asteroid.position.Y - asteroid.radius), Color.White);

            _spriteBatch.DrawString(ship.spaceFont, "SPACE", new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(timer.timerFont, timer.ToString(), new Vector2(0, 50), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
