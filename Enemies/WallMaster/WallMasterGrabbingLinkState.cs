﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class WallMasterGrabbingLinkState : IEnemyState
    {

        private Vector2 location;
        private Rectangle currentSpace;

        private Rectangle targetSpace;

        private WallMaster master;


        private const int normalMovingSpeed = 1;
        private int moveSpeed = normalMovingSpeed;

        public Vector2 Location { get => location; }

        private Game game;

        public WallMasterGrabbingLinkState(Vector2 location,WallMaster master, Rectangle targetSpace, string dir, Game game)
        {
            this.location = location;
            this.master = master;
            this.game = game;

            EnemySprite sprite = EnemySpriteFactory.Instance.CreateWallMasterGrabSprite(dir);
            master.SetSprite(sprite);

            this.targetSpace = targetSpace;
        }

    

        public void Update()
        {
            if (Arrived(currentSpace, targetSpace)) Camera.Instance.BackToSquareOne();
            else MoveOneUnit();
        }


        public void MoveOneUnit()
        {
            location = MoveToward(location, targetSpace.Location.ToVector2());
            currentSpace.Location = location.ToPoint();
            master.Location = location;

        }


        public Vector2 MoveToward(Vector2 position, Vector2 target)
        {
            Vector2 direction = target - position;
            direction.Normalize();
            position = Vector2.Add(position, direction * moveSpeed);

            return position;
        }

        private bool Arrived(Rectangle position, Rectangle target)
        {
            float dist = Vector2.Distance(position.Location.ToVector2(), target.Location.ToVector2());
            return dist <= 1;
        }

   

        public void TakeDamage(int amount)
        {
           //do nothing
        }

        public void Stun(bool b)
        {
            //do nothing

        }

        public void ChangeDirection()
        {
            //do nothing
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            //do nothing
        }

        public void Die()
        {
            //does not die
        }

        public void Attack()
        {
            //do nothing
        }
    }
}
