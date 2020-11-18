using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class DefaultLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {
            return true;
        }
    }
}
