using System;
using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace Asteroids {

    public class EnemyWave {

        public Type enemyType;

        public EnemyFormation formation;

        public string flightPathKey;

        public double lastMillitime;

        public EnemyWave(Type enemyType, EnemyFormation formation, string flightPathKey) {
            if (!typeof(Enemy).IsAssignableFrom(enemyType))
                throw new ArgumentException("Enemy type must be of type '" + typeof(Enemy).Name + "'");
            
            this.formation = formation;
            this.flightPathKey = flightPathKey;
        }


        public void Update(Game1 game, GameTime gameTime) {
            double newMillitime = lastMillitime + gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (var toSpawn in formation.GetSpawnList(lastMillitime, newMillitime))
                Spawn(game, toSpawn);
            lastMillitime = newMillitime;
            if (lastMillitime > formation.TotalTime) game.KillWave(this);
        }


        public void Spawn(Game1 game, Vector2 spawnPos) {
            Enemy enemy = (Enemy)Activator.CreateInstance(enemyType);
            if (enemy == null) {
                game.Log.Error(String.Format("Could not create instance: {0}", enemyType.Name));
                return;
            }
            enemy.Init(game);
            enemy.geometry.Position = spawnPos;
            EnemyFlightPath flightPath = game.GetFlightPath(flightPathKey);
            if (flightPath == null) {
                game.Log.Error(String.Format("No flightpath registered to key: {0}", flightPathKey));
                return;
            }
            flightPath.InitEnemy(enemy);
            game.AddEntity(enemy);
        }
    }
}