using System.Collections.Generic;

namespace Asteroids {

    public interface ICollisionShape {

        bool CollidesWith(ICollisionShape otherShape);

        IEnumerable<int> CollisionMapCells(float resolution);
    }
}