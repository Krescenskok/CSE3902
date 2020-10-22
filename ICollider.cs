using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public interface ICollider
    {
        /// <summary>
        /// Used to check type of collider
        /// <para>Ex. For enemies, return true if tag == "enemy"</para>
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>true if tag equals specified string</returns>
        bool CompareTag(string tag);

        string Tag { get; }
        bool Equals(ICollider col);

        /// <summary>
        /// Method used to Send a Message to something you're colliding with
        /// </summary>
        /// <param name="msg">Name of action to be taken</param>
        /// <param name="value">Value needed to take action</param>
        void SendMessage(string msg, object value);

        /// <summary>
        /// Rectangle representing physical size and location of the collider
        /// </summary>
        /// <returns></returns>
        Rectangle Bounds();

        /// <summary>
        /// Called every frame while colliding with the other object
        /// </summary>
        /// <param name="col">The other collider</param>
        /// <param name="collision"></param>
        void HandleCollision(ICollider col, Collision collision);

        /// <summary>
        /// Called once at the start of the collision
        /// </summary>
        /// <param name="col">The other collider</param>
        /// <param name="collision"></param>
        void HandleCollisionEnter(ICollider col, Collision collision);


    }
}
