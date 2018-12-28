using System.Collections.Generic;

namespace Asteroids {

    public partial class Game1 {

        public const float COLLISION_MAP_RESOLUTION = .1f;

        public Dictionary<int, IList<GameEntity>> CollisionMap;

        public static int CollisionMapCoordHash(int x, int y) {
            return (x << 16) | y;
        }


        public void CollisionPreInit() {
            CollisionMap = new Dictionary<int, IList<GameEntity>>();
        }


        public void CollisionPostInit() {
        }


        public void UpdateCollision() {
            CollisionMap.Clear();
            var ignore = new HashSet<GameEntity>();
            foreach (var entity in entities) {
                ignore.Clear();
                if (IsEntityAlive(entity) && entity.Collidable) {
                    foreach (var cell in entity.CollisionShape.CollisionMapCells(COLLISION_MAP_RESOLUTION)) {
                        IList<GameEntity> entList;
                        if (!CollisionMap.ContainsKey(cell)) {
                            entList = new List<GameEntity>();
                            CollisionMap[cell] = entList;
                        } else {
                            entList = CollisionMap[cell];
                            foreach (var other in entList) {
                                // TODO: Add safety here:
                                // - null-check collision shape
                                // - make sure both entities are alive.
                                if (!ignore.Contains(other) && entity.CollisionShape.CollidesWith(other.CollisionShape)) {
                                    entity.Collision(other, this);
                                    other.Collision(entity, this);
                                    ignore.Add(other);
                                }
                            }
                        }
                        entList.Add(entity);
                    }
                }
            }
        }

    }
}