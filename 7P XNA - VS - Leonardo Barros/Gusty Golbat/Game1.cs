using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gusty_Golbat
{
    public class Game1 : Game
    {
        // SETUP
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private BasicEffect _effect;


        // PERSONAGEM


        // CENÁRIO
        private PlaneDrawer _plane;
        private Texture2D _backgroundTexture;

        // ENTIDADES
        



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // SETUP
            Screen.GetInstance().SetWidth(_graphics.PreferredBackBufferWidth = 1280);
            Screen.GetInstance().SetHeight(_graphics.PreferredBackBufferHeight = 720);
        }

        protected override void Initialize()
        {
            // SETUP
            this._camera = new Camera();
            this._camera.SetupView(new Vector3(-5.5f, 0f, 10f), new Vector3(0f, 0f, 0f), Vector3.Up);

            // CENÁRIO
            this._plane = new PlaneDrawer(GraphicsDevice);
            this._plane.SetPlaneInitialTransform(new Vector3(0f, 0f, -10f), new Vector3(90f, 0f, 0f), new Vector3(2f, 0f, 0.7f));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // SETUP
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _effect = new BasicEffect(GraphicsDevice);
            _effect.TextureEnabled = true;

            // CENÁRIO
            _backgroundTexture = Content.Load<Texture2D>("Background");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // CAMERA
            _camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // CAMERA
            _effect.View = _camera.GetView();
            _effect.Projection = _camera.GetProjection();

            // PERSONAGENS


            // CENÁRIO
            _plane.Draw(this._effect, this._backgroundTexture);

            base.Draw(gameTime);
        }
    }
}
