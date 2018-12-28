using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Asteroids {

    public class CollisionRect : ICollisionShape {

        public RectangleF rectangle;

        public CollisionRect(RectangleF rectangle) {
            this.rectangle = rectangle;
        }

        public bool CollidesWith(ICollisionShape otherShape) {
            if (otherShape is CollisionRect) {
                var otherRect = otherShape as CollisionRect;
                return CollidesWith(((CollisionRect)otherShape).rectangle);
            }
            if (otherShape is CollisionPoint) {
                return rectangle.Contains(((CollisionPoint)otherShape).Position);
            }
            return otherShape.CollidesWith(this);
        }


        public bool CollidesWith(RectangleF otherRect) {
            // TODO: Rect/Rect collision
            return false;
        }

        public IEnumerable<int> CollisionMapCells(float resolution) {
            var res = new List<int>();

            int minX = (int)Math.Floor(rectangle.X / resolution);
            int maxX = (int)Math.Floor((rectangle.X + rectangle.Width) / resolution);
            int minY = (int)Math.Floor(rectangle.Y / resolution);
            int maxY = (int)Math.Floor((rectangle.Y + rectangle.Height) / resolution);
            for (int x = minX; x <= maxX; x++) {
                for (int y = minY; y <= maxY; y++) {
                    res.Add(Game1.CollisionMapCoordHash(x, y));
                }
            }

            return res;
        }
    }
}