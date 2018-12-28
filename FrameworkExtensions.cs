using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework {

    public struct Circle {
        public Vector2 Center;
        public float Radius;

        public Circle(Vector2 center, float radius) {
            this.Center = center;
            this.Radius = radius;
        }

        public bool Contains(Vector2 pos) {
            double distSquared = Math.Pow(pos.X - Center.X, 2) +
                    Math.Pow(pos.Y - Center.Y, 2);
            return distSquared < Math.Pow(Radius, 2);
        }
    }


    public struct RectangleF {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public RectangleF(float x, float y, float width, float height) {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public RectangleF(Vector2 pos, Vector2 size) {
            this.X = pos.X;
            this.Y = pos.Y;
            this.Width = size.X;
            this.Height = size.Y;
        }

        public bool Contains(Vector2 pos) {
            if (pos.X >= X && pos.X <= (X + Width)) {
                if (pos.Y >= Y && pos.Y <= (Y + Width))
                    return true;
            }
            return false;
        }

        public IList<Vector2> Corners() {
            var res = new List<Vector2>();
            res.Add(new Vector2(X, Y));
            res.Add(new Vector2(X, Y + Height));
            res.Add(new Vector2(X + Width, Y));
            res.Add(new Vector2(X + Width, Y + Height));
            return res;
        }
    }


    public static class FrameworkExtensions {

        public static IList<Vector2> Corners(this Rectangle rectangle) {
            var res = new List<Vector2>();
            res.Add(new Vector2(rectangle.X, rectangle.Y));
            res.Add(new Vector2(rectangle.X, rectangle.Y + rectangle.Height));
            res.Add(new Vector2(rectangle.X + rectangle.Width, rectangle.Y));
            res.Add(new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));
            return res;
        }
    }
}