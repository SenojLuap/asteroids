using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Asteroids {

    public class CollisionCircle : ICollisionShape {

        public Circle circle;

        public CollisionCircle(Vector2 pos, float radius) {
            circle = new Circle {
                Center = pos,
                Radius = radius
            };
        }


        public CollisionCircle(Circle circle) {
            this.circle = circle;
        }


        public bool CollidesWith(ICollisionShape otherShape) {
            if (otherShape is CollisionSet)
                return otherShape.CollidesWith(this);
            var circleShape = otherShape as CollisionCircle;
            if (circleShape != null)
                return CollidesWith(circleShape.circle);
            var rectShape = otherShape as CollisionRect;
            if (rectShape != null)
                return CollidesWith(rectShape.rectangle);
            var pointShape = otherShape as CollisionPoint;
            if (pointShape != null)
                return circle.Contains(pointShape.Position);
            return false;
        }


        public bool CollidesWith(Circle otherCircle) {
            double minDistSquared = Math.Pow(circle.Radius + otherCircle.Radius, 2);
            
            double distSquared = Math.Pow(otherCircle.Center.Y - circle.Center.Y, 2) +
                    Math.Pow(otherCircle.Center.X - circle.Center.X, 2);
            return distSquared < minDistSquared;
        }


        public bool CollidesWith(RectangleF otherRect) {
            foreach (var corner in otherRect.Corners())
                if (circle.Contains(corner))
                    return true;
            var tallRect = new RectangleF() {
                X = otherRect.X,
                Width = otherRect.Width,
                Y = otherRect.Y - circle.Radius,
                Height = otherRect.Height + circle.Radius + circle.Radius
            };
            if (tallRect.Contains(circle.Center))
                return true;
            var wideRect = new RectangleF() {
                X = otherRect.X - circle.Radius,
                Width = otherRect.Width + circle.Radius + circle.Radius,
                Y = otherRect.Y,
                Height = otherRect.Height
            };
            return wideRect.Contains(circle.Center);
        }


        private const bool FAST_COLLISION_MAP_ALGORITHM = true;

        public IEnumerable<int> CollisionMapCells(float resolution) {
            var res = new List<int>();
            if (FAST_COLLISION_MAP_ALGORITHM) {
                int minX = (int)Math.Floor((circle.Center.X - circle.Radius ) / resolution);
                int maxX = (int)Math.Floor((circle.Center.X + circle.Radius ) / resolution);
                int minY = (int)Math.Floor((circle.Center.Y - circle.Radius ) / resolution);
                int maxY = (int)Math.Floor((circle.Center.Y + circle.Radius ) / resolution);
                for (int x = minX; x <= maxX; x++)
                    for (int y = minY; y <= maxY; y++)
                        res.Add(Game1.CollisionMapCoordHash(x, y));
            } else {

            }
            return res;
        }
    }
}

