using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    //layer for blocks, walls, nonmoving objects
    public class ObstacleLayer : Layer
    {
        public bool AttachedToPlayer => false;
        public bool CollidesWith(ICollider other)
        {
            return other.layer is PlayerLayer
                || other.layer is PlayerWeaponLayer
                || other.layer is DefaultLayer;
        }
    }
}
