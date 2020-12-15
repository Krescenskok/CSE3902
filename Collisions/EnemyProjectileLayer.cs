using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class EnemyProjectileLayer : Layer
    {
        public bool AttachedToPlayer => false;

        public bool CollidesWith(ICollider col)
        {
            return col.layer is PlayerLayer
                || col.layer is ObstacleLayer
                || col.layer is ShieldLayer;
        }
    }
}
