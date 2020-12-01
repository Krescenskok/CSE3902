using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class PlayerWeaponLayer : Layer
    {
        public bool AttachedToPlayer => false;
        public bool CollidesWith(ICollider other)
        {
            return other.layer is EnemyLayer
                || other.layer is ObstacleLayer
                || other.layer is PlayerLayer
                || other.layer is DefaultLayer;
        }
    }
}
