using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MundoUm.Content;

namespace MundoUm
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Triangle triangle;

        VertexBuffer vertexBuffer;
        Matrix world;
        Matrix view;
        Matrix projection;

        BasicEffect effect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //triangle = new Triangle(0,1,0);

            world = Matrix.Identity;
            view = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Window.ClientBounds.Width / (float)Window.ClientBounds.Height, 1, 100);

            
            verts = new VertexPositionColor[3];
            verts[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);
            verts[1] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Blue);
            verts[2] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Yellow);
            

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), verts.Length, BufferUsage.None);

            vertexBuffer.SetData<VertexPositionColor>(verts);

            effect = new BasicEffect(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            effect.World = this.world;
            effect.View = this.view;
            effect.Projection = this.projection;
            effect.VertexColorEnabled = true;

            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, verts, 0, 1);
            }

            base.Draw(gameTime);
        }
    }
}
