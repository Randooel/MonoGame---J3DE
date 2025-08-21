using Gusty_Golbat.Content;
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
        Collider[] _collider;
        private Golbat[] _golbats;

        // CENÁRIO
        private PlaneDrawer _plane;
        private Texture2D _backgroundTexture;
        private Texture2D _golbatTexture;

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
            // -5.5f
            this._camera.SetupView(new Vector3(0f, 0f, 10f), new Vector3(0f, 0f, 0f), Vector3.Up);

            // PERSONAGENS
            _golbats = new Golbat[]
            {
                // Jogador
                new Golbat(this, new Vector3(0f,0f,-8f), new Vector3(0f,0f,0f), new Vector3(0.5f ,0.5f, 0.2f), 5,
                Vector3.One, Color.Green),
                new Golbat(this, new Vector3(8f,0f,-8f), new Vector3(0f,0f,0f), new Vector3(1f ,1f, 1f), 0,
                Vector3.One, Color.Green),
            };

            this._collider = new Collider[]
            {
                new Collider(this, new Vector3(0,2,-6), new Vector3(6,4,0.5f), Color.Green),
                new Collider(this, new Vector3(0,2, 6), new Vector3(6, 4, 0.5f), Color.Green),
            };

            // CENÁRIO
            _plane = new PlaneDrawer(GraphicsDevice);
            _plane.SetPlaneInitialTransform(new Vector3(0f, 0f, -10f), new Vector3(90f, 0f, 0f), new Vector3(2f, 0f, 0.7f));

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

            // PERSONAGENS
            foreach(var golbat in _golbats)
            {
                golbat.Update(gameTime);
            }

            foreach(Collider c in this._collider)
            {
                if (c.IsColliding(_golbats[0].GetBoundingBox()))
                {
                    Window.Title = "Colidiu";
                    c.GetLineBox().SetColor(Color.Red);

                    _golbats[0].RestorePosition();
                }
                else
                {
                    Window.Title = "Gutsy Golbat";
                    c.GetLineBox().SetColor(Color.Green);
                }

                for (int i = 0; i < _golbats.Length; i++)
                {
                    for (int j = i + 1; j < _golbats.Length; j++)
                    {
                        if (_golbats[i].IsColliding(_golbats[j].GetBoundingBox()))
                        {
                            Window.Title = $"Golbat {i} colidiu com Golbat {j}";

                            _golbats[i].RestorePosition();
                            _golbats[j].RestorePosition();
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // CAMERA
            _effect.View = _camera.GetView();
            _effect.Projection = _camera.GetProjection();

            // PERSONAGENS
            foreach(var golbat in _golbats)
            {
                golbat.Draw(this._camera);
            }

            // CENÁRIO
            _plane.Draw(this._effect, this._backgroundTexture);

            base.Draw(gameTime);
        }
    }
}