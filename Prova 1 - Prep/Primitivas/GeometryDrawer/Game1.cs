using GeometryDrawer.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace GeometryDrawer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // INSTÂNCIAS DE CLASSES AQUI:
        private List<CubeDrawer> _cubes = new List<CubeDrawer>();
        private List<PlaneDrawer> _planes = new List<PlaneDrawer>();
        private List<WindmillDrawer> _windmills = new List<WindmillDrawer>();

        // MATRIZES
        private Matrix world;
        private Matrix view;
        private Matrix projection;

        // EFFECT
        private BasicEffect effect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // INSTANCIAÇÃO DAS MATRIZES
            //world = Matrix.Identity;
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

            _cubes.Add(new CubeDrawer(GraphicsDevice));

            _planes.Add(new PlaneDrawer(GraphicsDevice));

            _windmills.Add(new WindmillDrawer(GraphicsDevice));
            _windmills.Add(new WindmillDrawer(GraphicsDevice));


            // TRANSFORMAÇÕES
            // Definindo as posições iniciais dos objetos
            // (posiçãoXYZ, rotaçãoY, escala)
            _cubes[0].SetCubeInitialPos(new Vector3(1, -1, 1), 0f, 1f);

            _planes[0].SetPlaneInitialPos(new Vector3(1, -1, 1), 0f, 1f);

            // MOINHO 1
            _windmills[0].SetWindmillInitialPos(new Vector3(-3, -1, -2), 45f, 1f);
            // HÉLICES
            float helixDistance = 0.1f;
            float helixHeight = 1f;
            _windmills[0].AddHelixInitialPos(new Vector3(helixDistance, helixHeight, 0.5f), 0f, 0f, 1f);
            _windmills[0].AddHelixInitialPos(new Vector3(0, helixDistance + helixHeight, 0.5f), 0f, 90f, 1f);
            _windmills[0].AddHelixInitialPos(new Vector3(-helixDistance, helixHeight, 0.5f), 0f, 180f, 1f);
            _windmills[0].AddHelixInitialPos(new Vector3(0, -helixDistance + helixHeight, 0.5f), 0f, 270f, 1f);

            // MOINHO2
            _windmills[1].SetWindmillInitialPos(new Vector3(6, -1, -2), -45f, 1f);
            // HÉLICES
            float helixDistance2 = 0.1f;
            float helixHeight2 = 1f;
            _windmills[1].AddHelixInitialPos(new Vector3(helixDistance2, helixHeight2, 0.5f), 0f, 0f, 1f);
            _windmills[1].AddHelixInitialPos(new Vector3(0, helixDistance2 + helixHeight2, 0.5f), 0f, 90f, 1f);
            _windmills[1].AddHelixInitialPos(new Vector3(-helixDistance2, helixHeight2, 0.5f), 0f, 180f, 1f);
            _windmills[1].AddHelixInitialPos(new Vector3(0, -helixDistance2 + helixHeight2, 0.5f), 0f, 270f, 1f);
        }

        protected override void Update(GameTime gameTime)
        {
            // COLA: Time.deltaTime = gameTime.ElapsedGameTime.TotalSeconds

            foreach(var windmill in _windmills)
            {
                windmill.Update(gameTime);
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

            foreach (var windmill in _windmills)
            {
                windmill.Draw(effect);
            }

                base.Draw(gameTime);
        }
    }
}
