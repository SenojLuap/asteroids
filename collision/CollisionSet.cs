using System.Collections.Generic;

namespace Asteroids {

    public class CollisionSet : ICollisionShape {

        public List<ICollisionShape> subShapes;

        public CollisionSet() {
            subShapes = new List<ICollisionShape>();
        }


        public CollisionSet(IList<ICollisionShape> shapes) {
            foreach (var shape in shapes)
                subShapes.Add(shape);
        }

        public bool CollidesWith(ICollisionShape otherShape) {
            foreach (var subShape in subShapes) {
                if (subShape.CollidesWith(otherShape))
                    return true;
            }
            return false;
        }

        public IEnumerable<int> CollisionMapCells(float resolution) {
            var res = new HashSet<int>();

            foreach (var subShape in subShapes) {
                foreach (var subCell in subShape.CollisionMapCells(resolution)) {
                    res.Add(subCell);
                }
            }

            return res;
        }
    }
}