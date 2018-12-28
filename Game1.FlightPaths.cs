using System.Collections.Generic;

namespace Asteroids {
    public partial class Game1 {
        Dictionary<string, EnemyFlightPath> flightPaths;

        public void FlightPathsPreInit() {
            flightPaths = new Dictionary<string, EnemyFlightPath>();

        }


        public void FlightPathsPostInit() {
        }


        public EnemyFlightPath GetFlightPath(string flightPathKey) {
            EnemyFlightPath res = flightPaths[flightPathKey];
            return res;
        }


        public void RegisterFlightPath(EnemyFlightPath newPath) {
            flightPaths[newPath.Key] = newPath;
        }
    }
}