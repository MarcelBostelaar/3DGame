using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test3D
{

    public class Test3DDemo : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Camera
        Vector3 camTarget;
        Vector3 camPosition;
        Matrix projectionMatrix;
        Matrix viewMatrix;
        Matrix worldMatrix;

        //BasicEffect for rendering
        BasicEffect basicEffect;
        BasicEffect basicEffectTextured;

        //Geometric info
        VertexPositionColor[] triangleVertices;
        VertexPositionNormalTexture[] TexturedTriangleVertices;
        VertexBuffer vertexBuffer;
        VertexBuffer TexturedvertexBuffer;

        //texture
        Texture2D PlaceHolderTexture;

        //Orbit
        bool orbit = false;

        public Test3DDemo()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            //Setup Camera
            camTarget = new Vector3(0f, 0f, 0f); //the position of the camera target
            camPosition = new Vector3(0f, 0f, -100f); //the position of your camera
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                               MathHelper.ToRadians(45f),
                               GraphicsDevice.DisplayMode.AspectRatio,
                1f, 1000f);
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,
                         new Vector3(0f, 1f, 0f));// Y up
            worldMatrix = Matrix.CreateWorld(camTarget, Vector3.
                          Forward, Vector3.Up);

            //Texture test
            TexturedTriangleVertices = new VertexPositionNormalTexture[3];
            TexturedTriangleVertices[0] = new VertexPositionNormalTexture(new Vector3(0, 20, 0),
                new Vector3(0, 0, -1),
                new Vector2(0, 0));
            TexturedTriangleVertices[1] = new VertexPositionNormalTexture(new Vector3(-20, -20, 0),
                new Vector3(0, 0, -1),
                new Vector2(0, 1));
            TexturedTriangleVertices[2] = new VertexPositionNormalTexture(new Vector3(20, -20, 0),
                new Vector3(0, 0, -1),
                new Vector2(1, 0));

            ///Triangle


            //BasicEffect
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;

            // Want to see the colors of the vertices, this needs to 
            //be on
            basicEffect.VertexColorEnabled = true;

            //Lighting requires normal information which 
            //VertexPositionColor does not have
            //If you want to use lighting and VPC you need to create a 
            //custom def
            basicEffect.LightingEnabled = false;

            
            //Geometry  - a simple triangle about the origin
            triangleVertices = new VertexPositionColor[3];
            triangleVertices[0] = new VertexPositionColor(new Vector3(
                                  0, 20, 0), Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(
                                  -20, -20, 0), Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(
                                  20, -20, 0), Color.Blue);

            //Vert buffer
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(
                           VertexPositionColor), 3, BufferUsage.
                           WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices)
                                                      ;
            //vertexBuffer.

            TexturedvertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), 3, BufferUsage.WriteOnly);
            TexturedvertexBuffer.SetData(TexturedTriangleVertices);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PlaceHolderTexture = Content.Load<Texture2D>("Placeholder");
            basicEffectTextured = new BasicEffect(GraphicsDevice);
            basicEffectTextured.TextureEnabled = true;
            basicEffectTextured.Texture = PlaceHolderTexture;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camPosition.X -= 1f;
                camTarget.X -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camPosition.X += 1f;
                camTarget.X += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camPosition.Y -= 1f;
                camTarget.Y -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camPosition.Y += 1f;
                camTarget.Y += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                camPosition.Z += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                camPosition.Z -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                orbit = !orbit;
            }

            if (orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(
                                        MathHelper.ToRadians(1f));
                camPosition = Vector3.Transform(camPosition,
                              rotationMatrix);
            }
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,
                         Vector3.Up);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            basicEffect.Projection = projectionMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.World = worldMatrix;

            basicEffectTextured.World = worldMatrix;
            basicEffectTextured.View = viewMatrix;
            basicEffectTextured.Projection = projectionMatrix;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            //GraphicsDevice.SetVertexBuffer(TexturedvertexBuffer);

            //Turn off culling so we see both sides of our rendered 
            //triangle
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.
                    Passes)
            {
                pass.Apply();
                //GraphicsDevice.dr
                //GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, TexturedTriangleVertices, 0, 3);
                GraphicsDevice.DrawPrimitives(PrimitiveType.
                                              TriangleList, 0, 3);
            }

            base.Draw(gameTime);
        }
    }
}