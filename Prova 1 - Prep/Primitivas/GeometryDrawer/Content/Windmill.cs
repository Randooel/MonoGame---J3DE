using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GeometryDrawer
{
    public class Windmill
    {
        Game game;
        Matrix world;
        Vector3 position, scale, rotation;
        WindmillDrawer wind;
        List<BladesDrawer> blades = new List<BladesDrawer>();

        // OBS: Se eu só precisasse de 1 eixo de cada tranformação, poderia fazer um único Vector3 pra armazenar esses valores.
        //EX: public Windmiil(Vector3 transforms)  Inserir os valores nesse formato: new Vector3(pos, rot, sca) e, nas funções de tranformação mutiplicar os inúteis por 0 e/ou tranformar
        // no mesmo valor definido para aquele eixo  
        public Windmill(Game game, Vector3 position, Vector3 rotation, Vector3 scale, int bladesNumber, Vector3 bladeScale)
        {
            this.game = game;
            this.rotation = rotation;
            this.position = position;
            this.scale = scale;
            

            this.world = Matrix.Identity;
            // IPC: APLICAR ROTAÇÃO ANTES DE TRANSLADAR. SE NÃO FIZER ISSO, O  OBJETO IRÁ SE MOVR FORA DO PRÓPRIO EIXO
            this.world *= Matrix.CreateRotationY(this.rotation.Y);
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateTranslation(this.position);
            
            

            this.wind = new WindmillDrawer(this.game, this.world);

            for(int i = 0; i < bladesNumber; i++)
            {
                Vector3 initialPos = new Vector3(0f, 1.5f, 0.05f);
                Vector3 initialRot = new Vector3(0f, 0f, MathHelper.ToRadians(90f * i));
                Vector3 initialSca = bladeScale;
                if(i < 4)
                {
                    initialRot += new Vector3(0f, 0f, 10f);
                }
                BladesDrawer blade = new BladesDrawer(this.game, initialPos, initialRot, initialSca);

                this.blades.Add(blade);
            }
        }

        public void Update(GameTime gameTime)
        {
            this.wind.Update(gameTime);
            foreach(var blade in blades)
            {
                blade.Update(gameTime, this.world);
            }
        }

        public void Draw(Camera camera)
        {
            this.wind.Draw(camera);
            
            foreach(var blades in this.blades)
            {
                blades.Draw(camera);
            }
        }
    }
}
