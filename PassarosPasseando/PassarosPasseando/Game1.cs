using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PassarosPasseando
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /*
        Texture2D sheerHeartAttackSprite;
        Texture2D target;
        */

        SpriteFont gameFont;

        Vector2 x, y;

        // Constructor: Handle basic settings for the game
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // Basicaly void Start
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        // Include art assets to the code
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            /*
            sheerHeartAttackSprite = Content.Load<Texture2D>("Sheer_Heart_Attack_Manga");

            target = Content.Load<Texture2D>("target");
            */

            gameFont = Content.Load<SpriteFont>("galleryFont");
            // TODO: use this.Content to load your game content here
        }

        // Game loop logic
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        // Display graphics and fonts to the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            /*
            _spriteBatch.Draw(target, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(sheerHeartAttackSprite, new Vector2(0, 0), Color.White);
            */

            _spriteBatch.DrawString(gameFont, "Sheer Heart Attack!", new Vector2(450, 10), Color.Black);
            
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
