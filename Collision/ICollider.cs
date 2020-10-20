using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public interface ICollider
    {

        bool CompareTag(string tag);
        bool Equals(ICollider col);

        void SendMessage(string msg, object value);

        Rectangle Bounds();

        
        void HandleCollision(ICollider col, Collision collision);

        void HandleCollisionEnter(ICollider col, Collision collision);


    }
}
