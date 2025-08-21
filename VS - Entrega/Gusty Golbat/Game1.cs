using Gusty_Golbat.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Gusty_Golbat
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private BasicEffect _effect;

        private Collider[] _collider;
        private Golbat[] _golbats;

        private PlaneDrawer _plane;
        private Texture2D _backgroundTexture;
        private Texture2D _golbatTexture;

        private List<Coracao> _coracoes;
        private int _coracaoCount = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Screen.GetInstance().SetWidth(_graphics.PreferredBackBufferWidth = 1280);
            Screen.GetInstance().SetHeight(_graphics.PreferredBackBufferHeight = 720);
        }

        protected override void Initialize()
        {
            _camera = new Camera();
            _camera.SetupView(new Vector3(-5.5f, 0f, 10f), Vector3.Zero, Vector3.Up);

            _golbats = new Golbat[]
            {
                new Golbat(this, new Vector3(-15f,0f,-8f), Vector3.Zero, new Vector3(1f,1.5f,0.5f), 5, _golbatTexture, Vector3.One, Color.Green),
                //new Golbat(this, new Vector3(8f,0f,-8f), Vector3.Zero, new Vector3(1f,1.5f,0.5f), 0, _golbatTexture, Vector3.One, Color.Green)
            };

            _collider = new Collider[]
            {
                new Collider(this, new Vector3(0,2,-6), new Vector3(6,4,0.5f), Color.Green),
                new Collider(this, new Vector3(0,2,6), new Vector3(6,4,0.5f), Color.Green)
            };

            _coracoes = new List<Coracao>
            {
                new Coracao(this, new Vector3(30f,0f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,2f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,3f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,-3f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,-2f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,-1f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,1f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,0f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,2f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,3f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,-3f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,-2f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,-1f,-8f), Vector3.One, Color.Red),
                new Coracao(this, new Vector3(30f,1f,-8f), Vector3.One, Color.Red),
            };

            _plane = new PlaneDrawer(GraphicsDevice);
            _plane.SetPlaneInitialTransform(new Vector3(0f, 0f, -10f), new Vector3(90f, 0f, 0f), new Vector3(2f, 0f, 0.7f));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _effect = new BasicEffect(GraphicsDevice) { TextureEnabled = true };

            _backgroundTexture = Content.Load<Texture2D>("Background");
            _golbatTexture = Content.Load<Texture2D>("Golbat");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _camera.Update(gameTime);

            foreach (var golbat in _golbats)
                golbat.Update(gameTime);

            for (int i = _coracoes.Count - 1; i >= 0; i--)
            {
                var coracao = _coracoes[i];
                coracao.Update(gameTime);

                if (coracao.IsColliding(_golbats[0].GetBoundingBox()))
                {
                    _coracaoCount++;
                    Window.Title = _coracaoCount.ToString();
                    _coracoes.RemoveAt(i);
                }
            }

            foreach (var c in _collider)
            {
                if (c.IsColliding(_golbats[0].GetBoundingBox()))
                {
                    Window.Title = "Colidiu";
                    c.GetLineBox().SetColor(Microsoft.Xna.Framework.Color.Red);
                    _golbats[0].RestorePosition();
                }
                else
                {
                    c.GetLineBox().SetColor(Microsoft.Xna.Framework.Color.Green);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            _effect.View = _camera.GetView();
            _effect.Projection = _camera.GetProjection();

            foreach (var golbat in _golbats)
                golbat.Draw(_camera);

            foreach (var coracao in _coracoes)
                coracao.Draw(_camera);

            _plane.Draw(_effect, _backgroundTexture);

            base.Draw(gameTime);
        }
    }
}
