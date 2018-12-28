using Microsoft.Xna.Framework;

namespace Asteroids {
    public class Enemy : GameEntity {

        public EnemyFlightPath flightPath;

        public object flightState;

        public Geometry geometry;

        public override void Init(Game1 game) {
            base.Init(game);
            geometry = new Geometry();
        }

        public override void Update(Game1 game, GameTime delta) {
            if (flightPath != null) flightPath.Update(this, delta);
        }

    }
}