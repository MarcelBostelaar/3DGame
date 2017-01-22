using Game1.CameraSystems;
using Game1.ModelsAndAnimation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CustomContentManager contentManager;

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
        SimpleBox Testbox;
        BetterQuad Checkerboard;
        VertexDeclaration vertexDeclaration;
        
        Orthographic3rdPerson Camera;
        
        Vector3 Target = new Vector3(0, 0, 0);

        bool ResizeIsBeingHandled = false;
        void WindowClientChanged(object sender, EventArgs e)
        {
            ResizeIsBeingHandled = !ResizeIsBeingHandled;
            if (ResizeIsBeingHandled)
            {
                this.graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                this.graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                graphics.ApplyChanges();
                Camera.aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            }
        }

        ModelClass Cube;

        protected override void Initialize()
        {
            contentManager = new CustomContentManager(Content, GraphicsDevice);
            //var temp = new StaticWorldObject("Fuck", "Fuck", Manager);
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(WindowClientChanged);

            Testbox = new SimpleBox(Vector3.Zero, new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
            base.Initialize();


            //this.GraphicsDevice.re

            ModelClass newone = new ModelClass();
            newone.SaveJson("Content/Models/");
            CakeLayerModel NewOne = new CakeLayerModel();
            NewOne.SaveJson("Content/Models/", "CakeLayerDummyData");

            Cube = contentManager.Load<ModelClass>("fuck");
            Camera = new Orthographic3rdPerson(50, 0, (float)(1.75f * Math.PI), (float)(1.75f * Math.PI), (float)(1.5f * Math.PI), GraphicsDevice.Viewport.AspectRatio, 1, 100, 10, 1);
            this.graphics.PreferredBackBufferWidth = 500;
            this.graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        Texture2D PlaceHoldertexture;
        Texture2D CheckerBoardTexture;
        BasicEffect quadEffect;

        StaticWorldObject testobject;

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlaceHoldertexture = contentManager.Load<Texture2D>("placeholder lol");
            CheckerBoardTexture = contentManager.Load<Texture2D>("Checkerboard");
            quadEffect = new BasicEffect(graphics.GraphicsDevice);
            //quadEffect.EnableDefaultLighting();

            Checkerboard = new BetterQuad(new Vector3(-5, 0, -5), new Vector3(10, 0, 0), new Vector3(0, 0, 10));

            quadEffect.World = Matrix.Identity;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = PlaceHoldertexture;

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                });

            testobject = new StaticWorldObject("TestWorldObject", "DummyItemTexture", new Vector3(5, 1, 5), contentManager, GraphicsDevice);
            testobject = new StaticWorldObject("TestCakeLayer", "DummyItemTexture", new Vector3(4, 1, 4), contentManager, GraphicsDevice);
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
                Camera.zoom -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                Camera.zoom += 0.1f;
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
            //foreach

            //View = Camera.GetCamera(Target);
            GraphicsDevice.Clear(Color.CornflowerBlue);





            testobject.Draw(GraphicsDevice, gameTime, Camera.GetCamera(Target), Camera.projection);




            quadEffect.Texture = PlaceHoldertexture;
            quadEffect.View = Camera.GetCamera(Target);
            quadEffect.Projection = Camera.projection;
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();


                //GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                //    Testbox.Vertices, 0, 8,
                //    Testbox.Indexes, 0, Testbox.Indexes.Length / 3
                //    );


                //GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                //    Cube.Vertices, 0, Cube.Vertices.Length,
                //    Cube.Indices, 0, Cube.PrimitiveCount
                //    );

                //var indixes = new short[6];
                //var vertexess = new VertexPositionTexture[4];
                //Cube.Index_Buffer.GetData(indixes);
                //Cube.Vertex_Buffer.GetData(vertexess);
                //GraphicsDevice.SetVertexBuffer(Cube.Vertex_Buffer);
                //GraphicsDevice.Indices = Cube.Index_Buffer;
                //GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6);
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