using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class PlayerWeaponLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {
            return other.layer is EnemyLayer
                || other.layer is ObstacleLayer
                || other.layer is PlayerLayer
                || other.layer is DefaultLayer;
        }
    }
}
