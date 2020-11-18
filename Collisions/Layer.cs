using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public interface Layer
    {
        bool CollidesWith(ICollider col);
       
    }
}
