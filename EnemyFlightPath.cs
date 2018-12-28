using Microsoft.Xna.Framework;

namespace Asteroids {

    public abstract class EnemyFlightPath {
        public virtual void InitEnemy(Enemy enemy) {
            enemy.flightPath = this;
        }
        abstract public void Update(Enemy enemy, GameTime gameTime);
        public abstract string Key { get; }
    }
}