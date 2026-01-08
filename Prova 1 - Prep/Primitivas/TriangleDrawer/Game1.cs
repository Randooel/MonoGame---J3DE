using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TriangleDrawer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Triangle Stuff
        VertexPositionColor[] verts;
        VertexBuffer vertexBuffer;

        // WVP
        Matrix world;
        Matrix view;
        Matrix projection;

        BasicEffect effect;

        // CÂMERA
        Vector3 cameraPosition = new Vector3(0, 0, 5);
        Vector3 cameraTarget = Vector3.Zero;
        Vector3 cameraUp = Vector3.Up;
        float cameraSpeed = 10f;


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

            // INSTANCIAÇÃO DA GEOMETRIA

            InstantiateGeometry();

            // INSTANCIAÇÃO DO BASIC EFFECT
            effect = new BasicEffect(GraphicsDevice);

            base.Initialize();
        }

        void InstantiateGeometry()
        {
            // Caso a geometria não apareça, pode ser pela ordem de desenho dos vértices estar no sentido anti - horário,
            // fazendo este ser desenhado de costar e ficar invisível por conta do Culling
            // LOGO, DESCOMENTAR CASO ISSO ACONTEÇA:
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Define a quantidade de vértices que serão desenhados
            verts = new VertexPositionColor[36 + 6];

            
            CubeVerts();
            PlaneVerts();

            //TriangleVerts();

            // INSTANCIAÇÃO DO BUFFER E PASSAR A GEOMETRIA PARA ELE
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(
                VertexPositionColor), verts.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(verts);
        }

        void TriangleVerts()
        {
            verts = new VertexPositionColor[3];

            verts[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red); // Define posição e cor do vértice
            verts[1] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Green); // " "
            verts[2] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Purple);
        }

        void CubeVerts()
        {
            // IPC: Caso algum triângulo esteja com a normal errada pra consertar é muito
            // simples - basta trocar a posição e cor do primeiro e último vértice e...... (<- qtd de pontos pra indicar o tamanho da tela divida)
            // VOILÀ

            //FRENTE DO CUBO
            
            verts[0] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red); // Define posição e cor do vértice
            verts[1] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Green); // " "
            verts[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Blue); // " "

            // Segundo triângulo para criação de um plano
            verts[3] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Blue);
            verts[4] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Purple);
            verts[5] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            // TRASEIRA (Igual a frente do cubo, mas todo Z é -2)
            // Precisa ser desenhado em direção anti-horária para a normal ficar para trás já que é a parte traseira do cubo
            verts[6] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[7] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Green);
            verts[8] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Red);

            verts[9] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Purple);
            verts[10] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[11] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Red);

            // TOPO (Mesma coisa que a frente só que todo Y é 1 e alguns Zs são -2)
            verts[12] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Blue);
            verts[13] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Green);
            verts[14] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            verts[15] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Purple);
            verts[16] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Blue);
            verts[17] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            // ESQUERDA
            verts[18] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);
            verts[19] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Purple);
            verts[20] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Blue);

            verts[21] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Blue);
            verts[22] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Purple);
            verts[23] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            // DIREITA (mesma coisa que esquerda, mas todo X é +1
            verts[24] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[25] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Purple);
            verts[26] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Red);

            verts[27] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Red);
            verts[28] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Purple);
            verts[29] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);

            // BAIXO (mesma coisa que o topo, mas todo Y é -1)
            verts[30] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red);
            verts[31] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Green);
            verts[32] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);

            verts[33] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red);
            verts[34] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[35] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Purple);

            
        }

        void PlaneVerts()
        {
            // Mesma coisa que o topo do cubo, só que X e Z bem maiores é todo Y = -1
            verts[36] = new VertexPositionColor(new Vector3(10, -1, -10), Color.Pink);
            verts[37] = new VertexPositionColor(new Vector3(10, -1, 10), Color.Pink);
            verts[38] = new VertexPositionColor(new Vector3(-10, -1, 10), Color.Pink);

            verts[39] = new VertexPositionColor(new Vector3(-10, -1, -10), Color.Pink);
            verts[40] = new VertexPositionColor(new Vector3(10, -1, -10), Color.Pink);
            verts[41] = new VertexPositionColor(new Vector3(-10, -1, 10), Color.Pink);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


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



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // DESENHO DOS VÉRTICES ARMAZENADOS NO BUFFER USANDO EFEITOS

            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            effect.World = this.world;
            effect.View = this.view;
            effect.Projection = this.projection;
            effect.VertexColorEnabled = true;

            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>
                    (PrimitiveType.TriangleList, verts, 0, 14);
            }


            base.Draw(gameTime);
        }
    }
}
