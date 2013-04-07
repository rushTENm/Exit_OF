using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Exit_3R
{
    class GameModel
    {
        public Model Model;
        public Vector3 Position = Vector3.Zero;
        float Scale;

        public BoundingSphere boundingS;

        public void Init(ContentManager content, float scale, Vector3 position, string FileLoaction)
        {
            Model = content.Load<Model>(FileLoaction);

            Scale = scale;
            Position = position;

            boundingS = new BoundingSphere(Model.Meshes[0].BoundingSphere.Center, Model.Meshes[0].BoundingSphere.Radius);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                boundingS = BoundingSphere.CreateMerged(boundingS, mesh.BoundingSphere);
            }
            boundingS.Radius *= scale;
            boundingS.Center = position;
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

                    effect.World = boneTransforms[mesh.ParentBone.Index] * Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position);
                    effect.Projection = Camera.Projection;
                    effect.View = Camera.View;
                }
                mesh.Draw();
            }
        }
    }
}