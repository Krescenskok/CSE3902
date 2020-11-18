using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class NPC_Layer : Layer
    {
        public bool CollidesWith(ICollider col)
        {
            return col.layer is DefaultLayer;
        }
    }
}
