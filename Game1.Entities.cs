using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Asteroids {

    public partial class Game1 {
        List<GameEntity> entities;
        List<GameEntity> entitiesBirthpool;
        List<GameEntity> entitiesDeadpool;
        bool inEntitiesUpdate;

        public void EntitiesPreInit() {
            entities = new List<GameEntity>();
            entitiesBirthpool = new List<GameEntity>();
            entitiesDeadpool = new List<GameEntity>();
            inEntitiesUpdate = false;
        }

        public void EntitiesPostInit() { }

        public void AddEntity(GameEntity entity) {
            if (inEntitiesUpdate)
                entitiesBirthpool.Add(entity);
            else
                entities.Add(entity);
        }


        public void KillEntity(GameEntity entity) {
            if (inEntitiesUpdate)
                entitiesDeadpool.Add(entity);
            else
                entities.Remove(entity);
        }


        public void UpdateEntities(GameTime delta) {
            inEntitiesUpdate = true;
            foreach (var entity in entities) {
                if (entitiesDeadpool.Contains(entity)) continue;
                entity.Update(this, delta);
            }
            inEntitiesUpdate = false;
            ResolveEntities();
        }


        public void ResolveEntities() {
            foreach (var entity in entitiesBirthpool)
                entities.Add(entity);
            entitiesBirthpool.Clear();
            foreach (var entity in entitiesDeadpool)
                entities.Remove(entity);
            entitiesDeadpool.Clear();
        }


        public bool IsEntityAlive(GameEntity entity) {
            if (entitiesDeadpool.Contains(entity)) return false;
            if (entitiesBirthpool.Contains(entity)) return true;
            return entities.Contains(entity);
        }

    }

}