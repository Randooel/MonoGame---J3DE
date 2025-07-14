using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;

        //DECLARAÇÃO DOS OBJETOS
        private List<CubeDrawer> _cubes = new List<CubeDrawer>();
        private List<PlaneDrawer> _planes = new List<PlaneDrawer>();

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

        // COPO
        public double fillCupTime1 = 200;
        public double fillCupTime2 = 400;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
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

            // Adicionando cubos no array
            _cubes.Add(new CubeDrawer(GraphicsDevice));
            _cubes.Add(new CubeDrawer(GraphicsDevice));
            _cubes.Add(new CubeDrawer(GraphicsDevice));
            _cubes.Add(new CubeDrawer(GraphicsDevice));
            _cubes.Add(new CubeDrawer(GraphicsDevice));
            _cubes.Add(new CubeDrawer(GraphicsDevice));
            _cubes.Add(new CubeDrawer(GraphicsDevice));

            // Adicionando planos no array
            _planes.Add(new PlaneDrawer(GraphicsDevice));


            // TRANSFORMAÇÕES
            // BAR
            _cubes[0].SetCubeInitialPos(new Vector3(1, 0, -3), 0f, new Vector3(5,2,3));

            // MESA
            // Topo da mesa
            _cubes[1].SetCubeInitialPos(new Vector3(1, 0f, 5), 0f, new Vector3(1, 0.05f, 1));
            // pé
            _cubes[2].SetCubeInitialPos(new Vector3(1, -1f, 4.25f), 0f, new Vector3(0.25f, 1f, 0.25f));

            //PERSONAGEM
            //Cabeça
            _cubes[3].SetCubeInitialPos(new Vector3(-5f, 0.5f, 7f), 0f, new Vector3(1f, 1f, 1f));
            // Corpo
            _cubes[4].SetCubeInitialPos(new Vector3(-5f, -1f, 6.5f), 0f, new Vector3(0.5f, 1.75f, 0.5f));

            // Copos
            _cubes[5].SetCubeInitialPos(new Vector3(0.5f, 0.35f, 4.25f), 0f, new Vector3(0.25f, 0.25f, 0.25f));
            _cubes[6].SetCubeInitialPos(new Vector3(1.5f, 0.35f, 4.25f), 0f, new Vector3(0.25f, 0.25f, 0.25f));


            _planes[0].SetPlaneInitialPos(new Vector3(1, -1, 1), 0f, 1f);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MoveCamera(gameTime);

            fillCupTime1 -= gameTime.ElapsedGameTime.TotalSeconds;

            if (fillCupTime1 > 0)
            {
                fillCupTime1 --;
            }
            if (fillCupTime1 < 0)
            {
                fillCupTime1 = 0;

                _cubes[5].SetCubeInitialPos(new Vector3(0.5f, 0.35f, 4.25f), 0f, new Vector3(0.5f, 0.5f, 0.5f));
            }
         
            fillCupTime2 -= gameTime.ElapsedGameTime.TotalSeconds;

            if (fillCupTime2 > 0)
            {
                fillCupTime2--;
            }
            if (fillCupTime2 < 0)
            {
                fillCupTime2 = 0;
                _cubes[6].SetCubeInitialPos(new Vector3(1.5f, 0.35f, 4.25f), 0f, new Vector3(0.5f, 0.5f, 0.5f));
            }


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

                //GraphicsDevice.RasterizerState = RasterizerState.CullNone;

                // MATRIZES
                effect.World = world;
                effect.View = this.view;
                effect.Projection = projection;
                effect.VertexColorEnabled = true;

                // DESENHO DAS CLASSES DE OBJETOS
                foreach(var cube in _cubes)
                {
                    cube.Draw(effect);
                }

                foreach (var plane in _planes)
                {
                    plane.Draw(effect);
                }

                base.Draw(gameTime);          
        }
    }
}
