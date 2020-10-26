﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class HandPlayerFinderCollider : ICollider
    {

        public string Name { get => "WallMasterEyes"; }

        private Rectangle bounds;
        private WallMaster master;
        private WallMasterMoveState state;
      
  

        public HandPlayerFinderCollider(Rectangle masterRect, WallMaster wallMaster, Game game)
        {

            master = wallMaster;
            
            bounds = masterRect;

            

            state = (WallMasterMoveState)wallMaster.State;

            CollisionHandler.Instance.AddCollider(this);
        }


        public Rectangle Bounds()
        {
            return bounds;
        }


      
        public bool CompareTag(string tag)
        {
            return tag == Name;
        }

        public bool Equals(ICollider col)
        {
            return col == this;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {

            if (col.CompareTag("Player") || col.Name.Equals("gel")) state.LockOnToPlayerPosition(collision);

        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") || col.Name.Equals("gel")) state.LockOnToPlayerPosition(collision);

        }

        public void SendMessage(string msg, object value)
        {
            //does not handle messages
        }
    }
}
