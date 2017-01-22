using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int DiameterWiel = 800;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        public static Texture2D SingleWhitePixel;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("TextFont");
            Logo = Content.Load<Texture2D>("Scouting Logo");
            Wijzer = Content.Load<Texture2D>("Wijzer");

            Waardes = new List<string>();
            System.IO.TextReader lezer = File.OpenText("Content/Waardes.txt");
            string temp = lezer.ReadLine();
            while(temp != null)
            {
                Waardes.Add(temp);
                temp = lezer.ReadLine();
            }
            if(Waardes.Count < 2)
            {
                throw new Exception("Rad moet minimaal 2 waardes hebben!");
            }

            // TODO: use this.Content to load your game content here
            Wiel = new RenderTarget2D(GraphicsDevice, DiameterWiel, DiameterWiel);
            TekenWiel();

            SingleWhitePixel = new Texture2D(GraphicsDevice, 1, 1);
            SingleWhitePixel.SetData<Color>(new Color[] { Color.White });

            DraaiSnel = new Button(10, 10, 200, 50, "Draai Hard", Color.Red, Color.White);
            DraaiSnel.OnClick += DraaiSnell;

            DraaiLangzaam = new Button(10, 70, 200, 50, "Draai Langzaam", Color.LightGreen, Color.White);
            DraaiLangzaam.OnClick += DraaiLangzaamm;
        }
        Texture2D Logo, Wijzer;

        SpriteFont font;
        List<string> Waardes;

        Button DraaiSnel, DraaiLangzaam;
        Random random = new Random();

        private void DraaiSnell(object sender, EventArgs e)
        {
            rotationspeed = (float)random.Next(10000, 20000) / 100000;
        }

        private void DraaiLangzaamm(object sender, EventArgs e)
        {
            rotationspeed = (float)random.Next(4000, 8000) / 100000;
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

            // TODO: Add your update logic here

            rotation += rotationspeed;
            rotationspeed -= 0.0001f;
            if (rotationspeed < 0)
            {
                rotationspeed = 0;
            }
            DraaiSnel.CheckClick();
            DraaiLangzaam.CheckClick();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        float rotation = 0;
        float rotationspeed = 0;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            DraaiSnel.draw(spriteBatch, font);
            DraaiLangzaam.draw(spriteBatch, font);

            //spriteBatch.DrawString(font, "Test", Vector2.Zero, Color.White);
            spriteBatch.Draw(Wiel, position: new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2), color: Color.White, rotation: rotation, origin: new Vector2(DiameterWiel/2, DiameterWiel / 2));

            spriteBatch.Draw(Wijzer, position: new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2 - DiameterWiel/2), color: Color.White, origin: new Vector2(9, 13));

            //spriteBatch.DrawString()
            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }










        Color TextKleur = Color.Gray;

        public void TekenWiel()
        {
            Slice = new RenderTarget2D(GraphicsDevice, 1, SliceWidth);
            GraphicsDevice.SetRenderTarget(Slice);
            GraphicsDevice.Clear(Color.White);
            GraphicsDevice.SetRenderTarget(null);



            GraphicsDevice.SetRenderTarget(Wiel);
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.TransparentBlack);
            float Rotatie = 2 * (float)Math.PI / Waardes.Count;

            //spriteBatch.Draw(Logo, new Vector2(DiameterWiel / 2 - Logo.Width / 2, DiameterWiel / 2 - Logo.Height / 2), Color.White);
            spriteBatch.Draw(Logo, position: new Vector2(DiameterWiel/2), color: Color.White, origin: new Vector2(Logo.Width / 2, Logo.Height / 2), scale: new Vector2(0.6f));


            float TussenRotatie = 0;
            Color[] Kleuren = { Color.White, Color.Blue, Color.Red };

            for (int i = 0; i < Waardes.Count; i++)
            {
                while(TussenRotatie < Rotatie * (i+1))
                {
                    spriteBatch.Draw(Slice, position: new Vector2(DiameterWiel / 2), color: Kleuren[i % Kleuren.Length], rotation: TussenRotatie - Rotatie / 2, origin: new Vector2(0, DiameterWiel / 2));
                    TussenRotatie += Rotatie / 10000;
                }
                Vector2 dimenties = font.MeasureString(Waardes[i]);
                spriteBatch.DrawString(font, Waardes[i], new Vector2(DiameterWiel/2, DiameterWiel/2), TextKleur, Rotatie*i, new Vector2(dimenties.X/2, DiameterWiel/2 - DistanceTextFromTop), 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }

        RenderTarget2D Wiel;
        RenderTarget2D Slice;

        int SliceWidth = 60;
        int DistanceTextFromTop = 20;
    }
}
