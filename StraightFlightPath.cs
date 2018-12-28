

using Microsoft.Xna.Framework;

namespace Asteroids {

    public class StraightFlightPath : EnemyFlightPath {

        public Vector2 velocity;

        public StraightFlightPath(Vector2 velocity) : base() {
            this.velocity = velocity;
        }

        public override string Key { get => "straight"; }

        public override void InitEnemy(Enemy enemy) {
            base.InitEnemy(enemy);
            enemy.geometry.Velocity = velocity;
        }

        public override void Update(Enemy enemy, GameTime gameTime) {
            // Nothing to do. 
        }
    }
}