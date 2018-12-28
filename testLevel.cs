using Microsoft.Xna.Framework;

namespace Asteroids {
    public class TestLevel : Level {

        public const double LAUNCH_COOLDOWN = 3;

        public double cooldown = LAUNCH_COOLDOWN;

        public override void Update(Game1 game, GameTime elapsedTime) {
            cooldown -= elapsedTime.ElapsedGameTime.TotalSeconds;
            if (cooldown < 0) {

                //game.SS
                // TODO: Do the thing.
                cooldown += LAUNCH_COOLDOWN;
            }
        }
    }
}