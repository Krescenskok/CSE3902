using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class NPC_Layer : Layer
    {
        public bool AttachedToPlayer => false;
        public bool CollidesWith(ICollider col)
        {
            return col.layer is DefaultLayer;
        }
    }
}
