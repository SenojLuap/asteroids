using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Asteroids {
    public class Animation : GameEntity {

        public Texture2D spriteStrip;

        public float scale;

        public double elapsedTime;

        public double frameTime;

        public int frameCount;

        public int currentFrame;

        public Color color;

        public Rectangle sourceRect = new Rectangle();

        public Rectangle destinationRect = new Rectangle();

        public int frameWidth;

        public int frameHeight;

        public bool active;

        public bool looping;

        public Vector2 position;

        public float rotation;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth,
                int frameHeight, int frameCount, int frameTime, Color color, float scale,
                float rotation, bool looping) {
            this.color = color;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;
            this.looping = looping;
            this.position = position;
            this.spriteStrip = texture;
            this.rotation = rotation;
            elapsedTime = 0;
            currentFrame = 0;

            active = true;
        }


        public void Initialize(Texture2D texture, int frameWidth, int frameHeight,
                int frameCount, int frameTime, float scale=1.0f, float rotation=0.0f,
                bool looping=true) {
            this.color = Color.White;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameTime = frameTime;
            this.scale = scale;
            this.looping = looping;
            this.position = new Vector2(0f, 0f);
            this.spriteStrip = texture;
            this.rotation = rotation;
            elapsedTime = 0;
            currentFrame = 0;

            active = true;
        }

        public override void Update(Game1 game, GameTime ellapsed) {
            if (active == false) return;
            var realPosition = new Vector2(game.GraphicsDevice.Viewport.Width * position.X,
                    game.GraphicsDevice.Viewport.Height * position.Y);

            elapsedTime += ellapsed.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > frameTime) {
                currentFrame++;
                if (currentFrame == frameCount) {
                    currentFrame = 0;
                    if (!looping) active = false;
                }
                elapsedTime -= frameTime;
            }
            int framesPerRow = spriteStrip.Width / frameWidth;
            sourceRect = new Rectangle((currentFrame % framesPerRow)*frameWidth,
                    (currentFrame / framesPerRow) * frameHeight, frameWidth, frameHeight);
            destinationRect = new Rectangle((int)realPosition.X - (int)(frameWidth * scale) / 2,
                    (int)realPosition.Y - (int)(frameHeight*scale) / 2,
                    (int)(frameWidth*scale), (int)(frameHeight*scale));
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch) {
            var realPosition = new Vector2(game.GraphicsDevice.Viewport.Width * position.X,
                    game.GraphicsDevice.Viewport.Height * position.Y);
            if (active) spriteBatch.Draw(spriteStrip, realPosition, sourceRect, color,
                    rotation, new Vector2(frameWidth/2, frameHeight/2), scale, SpriteEffects.None, 0);
//                    rotation, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}