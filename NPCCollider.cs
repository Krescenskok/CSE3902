using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class NPCCollider : ICollider
    {
        public NPCCollider(Rectangle rectangle)
        {

        }
        public Rectangle Bounds()
        {
            throw new NotImplementedException();
        }

        public bool CompareTag(string tag)
        {
            throw new NotImplementedException();
        }

        public bool Equals(ICollider col)
        {
            throw new NotImplementedException();
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            throw new NotImplementedException();
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string msg, object value)
        {
            throw new NotImplementedException();
        }
    }
}
