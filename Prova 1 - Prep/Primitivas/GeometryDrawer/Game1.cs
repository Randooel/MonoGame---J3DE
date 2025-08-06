using GeometryDrawer.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;


namespace GeometryDrawer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // INSTÂNCIAS DE CLASSES AQUI:
        private List<CubeDrawer> _cubes = new List<CubeDrawer>();
        private List<PlaneDrawer> _planes = new List<PlaneDrawer>();

        Windmill[] windmill;

        // CÂMERA
        Camera camera;

        // EFFECT
        private BasicEffect effect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Definindo a resolução da tela
            Screen.GetInstance().SetWidth(_graphics.PreferredBackBufferWidth = 800);
            Screen.GetInstance().SetHeight(_graphics.PreferredBackBufferHeight = 600);
        }

        protected override void Initialize()
        {
            this.camera = new Camera();
            this.camera.SetupView(new Vector3(0f,2f,10f), new Vector3(0f,0f,0f), Vector3.Up);

            windmill = new Windmill[]
            {
                //                  position                        rotation Y            scale       
                new Windmill(this, new Vector3(-4f,0f,-3f), new Vector3(0f,1f,0f) ,new Vector3(1f,1f,1f), 
                // blade number,    blade scale
                    4, new Vector3(1f,1f,1f)),

                new Windmill(this, new Vector3(4f,0f,-3f), new Vector3(0f,-1f,0f), new Vector3(1f,1f,1f),
                    4, new Vector3(2f,2f,2f)),

                /*
                new Windmill(this, new Vector3(0f,0f,0f), new Vector3(0f,0f,0f), new Vector3(2f,1f,1f), 
                    4, new Vector3(1f,1f,1f)),

                new Windmill(this, new Vector3(4f,0f,0f), new Vector3(0f,0f,0f), new Vector3(1f,2f,1f), 
                    7, new Vector3(1f,1f,1f)),

                new Windmill(this, new Vector3(8f,0f,0f), new Vector3(0f,0f,0f), new Vector3(1f, 1f,2f), 
                    7, new Vector3(1f,1f,1f)),

                new Windmill(this, new Vector3(12f,0f,0f), new Vector3(0f,0f,0f), new Vector3(2f,2f,2f), 
                    10, new Vector3(1f,1f,1f)),
                */
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            effect = new BasicEffect(GraphicsDevice);

            _cubes.Add(new CubeDrawer(GraphicsDevice));

            _planes.Add(new PlaneDrawer(GraphicsDevice));


            // TRANSFORMAÇÕES
            // Definindo as posições iniciais dos objetos
            // (posiçãoXYZ, rotaçãoY, escala)
            _cubes[0].SetCubeInitialPos(new Vector3(0, 0, 0), 0f, 1f);

            _planes[0].SetPlaneInitialPos(new Vector3(0, 0, 0), 0f, 1f);

        }

        protected override void Update(GameTime gameTime)
        {
            // COLA: Time.deltaTime = gameTime.ElapsedGameTime.TotalSeconds

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // Update da câmera
            camera.Update(gameTime);

            // Update do Moinho
            foreach(var wind in windmill)
            {
                wind.Update(gameTime);
            }

            // ROTAÇÃO MUNDO
            // Girar mundo no eixo Y
            //world *= Matrix.CreateRotationY(0.01f);

            // Para voltar ao passo zero
            //world = Matrix.Identity;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();

            // MATRIZES
            effect.VertexColorEnabled = true;

            // DESENHO DAS CLASSES DE OBJETOS
            foreach(var wind in windmill)
            {
                wind.Draw(this.camera);
            }

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
