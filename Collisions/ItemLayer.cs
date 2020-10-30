using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class ItemLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {
            return other.layer is PlayerLayer
                || other.layer is DefaultLayer;
        }
    }
}
