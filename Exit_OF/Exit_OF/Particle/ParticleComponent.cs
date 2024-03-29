﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exit_OF
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ParticleComponent : IGameComponent
    {
        public List<Emitter> particleEmitterList = new List<Emitter>();

        Matrix invertY = Matrix.CreateScale(1, -1, 1);

        Vector3 particlePosition = new Vector3(-80, 40, 0);
        Vector3 viewSpacePosition;

        public ParticleComponent(Exit_OF game)
        {
            // TODO: Construct any child components here
            game.Components.Add(this);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize() { }

        public void LoadContent(ContentManager content)
        {
            Emitter smokeEmitter = new Emitter();
            smokeEmitter.Active = true;
            smokeEmitter.TextureList.Add(content.Load<Texture2D>(@"HomeEScreen\smoke"));
            smokeEmitter.RandomEmissionInterval = new RandomMinMax(45);
            smokeEmitter.ParticleLifeTime = 1350;
            smokeEmitter.ParticleDirection = new RandomMinMax(-15, 15);
            smokeEmitter.ParticleSpeed = new RandomMinMax(4.5f);
            smokeEmitter.ParticleRotation = new RandomMinMax(0);
            smokeEmitter.RotationSpeed = new RandomMinMax(-0.017f, 0.017f);
            smokeEmitter.ParticleFader = new ParticleFader(true, true);
            smokeEmitter.ParticleScaler = new ParticleScaler(0.1f, 0.2f, 50, smokeEmitter.ParticleLifeTime);
            smokeEmitter.Position = new Vector2(0, 0);

            particleEmitterList.Add(smokeEmitter);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            foreach (Emitter emitter in particleEmitterList)
            {
                emitter.UpdateParticles(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, BasicEffect basicEffect, Matrix projection, Matrix view, int fireStr)
        {
            basicEffect.World = invertY;
            basicEffect.View = Matrix.Identity;
            basicEffect.Projection = projection;

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);

            foreach (Emitter emitter in particleEmitterList)
            {
                viewSpacePosition = Vector3.Transform(particlePosition, view * invertY);

                foreach (Particle particle in emitter.ParticleList)
                {
                    spriteBatch.Draw(particle.Texture, (particle.Position * 0.05f) + new Vector2(viewSpacePosition.X, viewSpacePosition.Y), null, particle.Color, particle.Rotation, particle.Center, particle.Scale * fireStr/2000, 0, viewSpacePosition.Z);
                }
            }

            spriteBatch.End();
        }
    }
}
