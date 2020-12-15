using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class SwordLayer : Layer
    {
        public bool AttachedToPlayer => true;

        public bool CollidesWith(ICollider col)
        {
            return col.layer is EnemyLayer;
        }
    }
}
