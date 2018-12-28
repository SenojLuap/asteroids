using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids {

    public class GameEntity {

        public virtual ICollisionShape CollisionShape { get; set; }

        public virtual void Init(Game1 game) { }
        public virtual void Update(Game1 game, GameTime delta) { }
        public virtual void Draw(Game1 game, SpriteBatch spriteBatch) { }

        public virtual bool Collidable {
            get => CollisionShape != null;
        }

        public virtual void Collision(GameEntity other, Game1 game) {
        }
    }

}