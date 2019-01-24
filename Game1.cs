using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Asteroids.Common;
using paujo.Debug;

namespace Asteroids {
    public partial class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        public Logger Log { get; private set; }
        public ContentManager ContentManager { get; private set; }

        SpriteFont debugFont;

        public ControlManager controlManager;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            Log = new Logger();
            Log.AddConsoleTarget();

            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            
            ContentManager = new ContentManager(this);
            ContentManager.AddContentDirectory(Directory.GetCurrentDirectory() + "/assets/");
            ContentManager.LoadContent();

            EntitiesPreInit();
            WavesPreInit();
            CollisionPreInit();
            
            player = new Player();
            entities.Add(player);

            base.Initialize();
            controlManager = new ControlManager();

            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            EntitiesPostInit();
            WavesPostInit();
            CollisionPostInit();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Rectangle safeArea = GraphicsDevice.Viewport.TitleSafeArea;
            Vector2 playerPosition = new Vector2(0f,  0.5f);
            Animation playerAnimation = new Animation();
            //var playerSpriteSheet = Content.Load<Texture2D>("assets\\playerAnimated");

            var playerSpriteSheet = Content.Load<Texture2D>("assets\\testOrient");
            //playerAnimation.Initialize(playerSpriteSheet, playerPosition, 100, 200,
            //        64, 100, Color.White, 1f, MathHelper.PiOver2, true);
            playerAnimation.Initialize(playerSpriteSheet, playerPosition, 100, 100,
                    1, 100, Color.White, 1f, MathHelper.PiOver2, true);
            float imageRatio = (float)playerAnimation.frameWidth / (float)GraphicsDevice.Viewport.Width;
            playerAnimation.scale = Player.SIZE / imageRatio;
            player.Initialize(ContentManager.GetAnimationByKey("playerAnimated"), playerPosition);

            debugFont = Content.Load<SpriteFont>("assets\\DebugFont");


            //var spriteSheet = Content.Load<SpriteSheet>("assets\\spritesheets\\player.json");
        }


        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            controlManager.Update(Keyboard.GetState(), GamePad.GetState(PlayerIndex.One), Mouse.GetState());

            UpdateEntities(gameTime);
            UpdateWaves(gameTime);
            UpdateCollision();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (var entity in entities)
                entity.Draw(this, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        #region Helper Methods

        /// <summary>
        /// Convert a screen-relative location to screen-exact location (in pixels)
        /// </summary>
        /// <param name="position">The location, in screen-relative coordinates</param>
        /// <returns>The location in screen-exact coordinates</returns>
        public Vector2 RelativeToReal(Vector2 position) {
            var screenW = GraphicsDevice.Viewport.Width;
            var screenH = GraphicsDevice.Viewport.Height;
            return new Vector2(position.X * screenW, position.Y * screenH);
        }

        #endregion
    }
}
