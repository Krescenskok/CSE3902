using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class ShieldLayer : Layer
    {
        public bool AttachedToPlayer => true;
        public bool CollidesWith(ICollider other)
        {
            return other.layer is EnemyProjectileLayer;
        }
    }
}
