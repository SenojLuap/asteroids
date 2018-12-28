using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Asteroids {

    public class CollisionPoint : ICollisionShape {

        public const float MIN_COLLISION_DIST = .00001f;

        public Vector2 Position;

        public bool CollidesWith(ICollisionShape otherShape) {
            if (otherShape is CollisionPoint) {
                var otherPoint = otherShape as CollisionPoint;
                return Vector2.Distance(Position, otherPoint.Position) < MIN_COLLISION_DIST;
            }
            return otherShape.CollidesWith(this);
        }

        public IEnumerable<int> CollisionMapCells(float resolution) {
            var res = new List<int>();

            int x = (int)Math.Floor(Position.X / resolution);
            int y = (int)Math.Floor(Position.Y / resolution);
            res.Add(Game1.CollisionMapCoordHash(x, y));

            return res;
        }
    }
}