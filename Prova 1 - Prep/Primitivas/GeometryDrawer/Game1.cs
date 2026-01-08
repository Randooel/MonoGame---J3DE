using GeometryDrawer.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GeometryDrawer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // INSTÂNCIAS DE CLASSES AQUI:
        private CubeDrawer _cubeDrawer;
        private PlaneDrawer _planeDrawer;

        // MATRIZES
        private Matrix world;
        private Matrix view;
        private Matrix projection;

        // EFFECT
        private BasicEffect effect;

        // CÂMERA
        private Vector3 cameraPosition = new Vector3(0, 0, 5);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUp = Vector3.Up;
        private float cameraSpeed = 10f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // INSTANCIAÇÃO DAS MATRIZES
            world = Matrix.Identity;
            view = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero,
                Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, Window.ClientBounds.Width /
                (float)Window.ClientBounds.Height, 1, 100);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            effect = new BasicEffect(GraphicsDevice);

            _cubeDrawer = new CubeDrawer(GraphicsDevice);

            _planeDrawer = new PlaneDrawer(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MoveCamera(gameTime);

            base.Update(gameTime);
        }

        void MoveCamera(GameTime gameTime)
        {
            // CÂMERA: MOVIMENTO
            KeyboardState keyboard = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Vetor de direção da câmera (em relação ao target)
            Vector3 forward = Vector3.Normalize(cameraTarget - cameraPosition);
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, cameraUp));

            // Movimentos
            if (keyboard.IsKeyDown(Keys.W))
                cameraPosition += forward * cameraSpeed * deltaTime;

            if (keyboard.IsKeyDown(Keys.S))
                cameraPosition -= forward * cameraSpeed * deltaTime;

            if (keyboard.IsKeyDown(Keys.A))
                cameraPosition -= right * cameraSpeed * deltaTime;

            if (keyboard.IsKeyDown(Keys.D))
                cameraPosition += right * cameraSpeed * deltaTime;

            if (keyboard.IsKeyDown(Keys.E))
                cameraPosition += cameraUp * cameraSpeed * deltaTime;

            if (keyboard.IsKeyDown(Keys.Q))
                cameraPosition -= cameraUp * cameraSpeed * deltaTime;

            // Atualiza a View Matrix com nova posição
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUp);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // MATRIZES
            effect.World = world;
            effect.View = this.view;
            effect.Projection = projection;
            effect.VertexColorEnabled = true;

            _cubeDrawer.Draw(effect);
            _planeDrawer.Draw(effect);

                base.Draw(gameTime);
        }
    }
}
