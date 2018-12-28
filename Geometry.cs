using Microsoft.Xna.Framework;

namespace Asteroids {
    public class Geometry {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle Bound { get; set; }

        public void Update(GameTime gameTime) {
            var oldPos = Position;
            float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            oldPos.X += Velocity.X * timePassed;
            oldPos.Y += Velocity.Y * timePassed;
            Position = oldPos;
        }
    }
}