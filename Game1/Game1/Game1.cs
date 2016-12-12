using Game1.CameraSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        Quad quad;
        Quad quad2;
        SimpleBox Testbox;
        BetterQuad Checkerboard;
        VertexDeclaration vertexDeclaration;
        Matrix View, Projection;

        //Vector3 CamPos = new Vector3(0, 4, 4);
        ThirdPersonCamera Camera = new ThirdPersonCamera(5, 0, -3.14f/4, (float)(2*Math.PI), (float)(1.5f * Math.PI), 10, 3);
        Vector3 Target = Vector3.Zero;

        protected override void Initialize()
        {
            Testbox = new SimpleBox(Vector3.Zero, new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
            this.graphics.PreferredBackBufferWidth = 500;
            this.graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();
            quad = new Quad(Vector3.Zero, Vector3.Backward, Vector3.Up, 1, 1);
            quad2 = new Quad(new Vector3(0.5f, 0, 1), Vector3.Backward, Vector3.Up, 1, 1);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 500);
            //Projection = Matrix.CreateOrthographic(5, 5, 1, 500);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        Texture2D PlaceHoldertexture;
        Texture2D CheckerBoardTexture;
        BasicEffect quadEffect;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlaceHoldertexture = Content.Load<Texture2D>("Placeholder");
            CheckerBoardTexture = Content.Load<Texture2D>("Checkerboard");
            quadEffect = new BasicEffect(graphics.GraphicsDevice);
            //quadEffect.EnableDefaultLighting();

            Checkerboard = new BetterQuad(new Vector3(-5, 0, -5), new Vector3(10, 0, 0), new Vector3(0, 0, 10));

            quadEffect.World = Matrix.Identity;
            quadEffect.View = View;
            quadEffect.Projection = Projection;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = PlaceHoldertexture;

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                });

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Camera.pitch += MathHelper.ToRadians(1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Camera.pitch += MathHelper.ToRadians(-1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Camera.yaw += MathHelper.ToRadians(-1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Camera.yaw += MathHelper.ToRadians(1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                Camera.distanceFromTarget += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                Camera.distanceFromTarget -= 0.1f;
            }
            //Movement controls.
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                var vector = Camera.RotateFlatVector(new Vector2(0, 0.1f));
                Target += new Vector3(vector.X, 0, -vector.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                var vector = Camera.RotateFlatVector(new Vector2(-0.1f, 0));
                Target += new Vector3(vector.X, 0, -vector.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                var vector = Camera.RotateFlatVector(new Vector2(0, -0.1f));
                Target += new Vector3(vector.X, 0, -vector.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                var vector = Camera.RotateFlatVector(new Vector2(0.1f, 0));
                Target += new Vector3(vector.X, 0, -vector.Y);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            View = Camera.GetCamera(Target);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            quadEffect.Texture = PlaceHoldertexture;
            quadEffect.View = View;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                /*
                GraphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    quad2.Vertices, 0, 4,
                    quad2.Indexes, 0, 2);*/

                //GraphicsDevice.DrawUserIndexedPrimitives
                //    <VertexPositionNormalTexture>(
                //    PrimitiveType.TriangleList,
                //    quad.Vertices, 0, 4,
                //    quad.Indexes, 0, 2);

                GraphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionTexture>(PrimitiveType.TriangleList,
                    Testbox.Vertices, 0, 8,
                    Testbox.Indexes, 0, Testbox.Indexes.Length / 3
                    );
            }

            quadEffect.Texture = CheckerBoardTexture;
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    Checkerboard.Vertices, 0, 4,
                    Checkerboard.Indexes, 0, 2
                    );
            }

            base.Draw(gameTime);
        }
    }
}