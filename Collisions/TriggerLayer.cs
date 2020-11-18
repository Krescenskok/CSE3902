using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class TriggerLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {

            
            return other.layer is PlayerLayer
                || other.layer is DefaultLayer;

        }
    }
}
