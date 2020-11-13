using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class TriggerLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {
            return other is PlayerLayer
                || other is DefaultLayer;

        }
    }
}
