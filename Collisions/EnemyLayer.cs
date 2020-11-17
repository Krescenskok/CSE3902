using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class EnemyLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {
            return 
                other.layer is ObstacleLayer
                || other.layer is DefaultLayer;
        }
    }
}
