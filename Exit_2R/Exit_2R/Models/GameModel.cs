using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Exit_2R
{
    class GameModel
    {
        public Model Model;
        public Vector3 Position = new Vector3(0, 0, 0);

        public void ContentLoad(ContentManager Content, string FileLoaction)
        {
            Model = Content.Load<Model>(FileLoaction);
        }

        public void DrawMeshes(ChaseCamera Camera)
        {
            Matrix[] boneTransforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = boneTransforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(Position);
                    effect.Projection = Camera.Projection;
                    effect.View = Camera.View;
                }
                mesh.Draw();
            }
        }
    }
}
