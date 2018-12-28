using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids {
    public class Bullet : GameEntity {

        public Texture2D texture;

        public Geometry geometry;

        public float scale;

        public const float BULLET_SPEED = 1f;

        public const float SIZE = .03f;

        CollisionCircle collisionCircle;

        public override ICollisionShape CollisionShape {
            get => collisionCircle;
        }

        public void Initialize(Game1 game, Vector2 pos) {
            texture = game.Content.Load<Texture2D>("assets\\bullet");
            geometry = new Geometry();
            geometry.Position = pos;
            geometry.Bound = texture.Bounds;
            geometry.Velocity = new Vector2(BULLET_SPEED, 0f);
            float imgRatio = (float)texture.Width / (float)game.GraphicsDevice.Viewport.Width;
            scale = SIZE / imgRatio;
            collisionCircle = new CollisionCircle(pos, SIZE);
        }

        public override void Update(Game1 game, GameTime delta) {
            geometry.Update(delta);
            collisionCircle.circle.Center = geometry.Position;
            if (geometry.Position.X > game.GraphicsDevice.Viewport.Width) game.KillEntity(this);
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch) {
            var realPosition = new Vector2(game.GraphicsDevice.Viewport.Width * geometry.Position.X,
                    game.GraphicsDevice.Viewport.Height * geometry.Position.Y);
            spriteBatch.Draw(texture, realPosition, null, null, null, 0, new Vector2(scale, scale));
        }
    }
}