using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    //layer for blocks, walls, nonmoving objects
    public class ObstacleLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {
            return other.layer is PlayerLayer
                || other.layer is DefaultLayer;
        }
    }
}
