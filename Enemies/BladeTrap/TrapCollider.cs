using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Sprint4
{
    public class TrapCollider : ICollider
    {

        private Vector2 attackDirection;
        private int attackLength;
        private BladeTrap trap;
        private IEnemyState trapState;
        private Rectangle bounds;

        public string Name { get => "trap"; }
        public Layer layer { get; set; }

        public TrapCollider(Rectangle trapRect, BladeTrap trap, string attackDirection, int attackRange)
        {
            bounds = new Rectangle();

            
            trapState = trap.state;
            this.trap = trap;

            attackLength = attackRange / 2;

            if (attackDirection.Equals("right"))
            {
                this.attackDirection = new Vector2(1, 0);

                

                bounds.Location = trapRect.Location;
                bounds.Width = attackRange;
                bounds.Height = trapRect.Height;

                
            }
            else if (attackDirection.Equals("left"))
            {
                this.attackDirection = new Vector2(-1,0);
              

                bounds.X = trapRect.X - attackRange;
                bounds.Y = trapRect.Y;
                bounds.Width = attackRange;
                bounds.Height = trapRect.Height;
            }
            else if (attackDirection.Equals("up"))
            {
                this.attackDirection = new Vector2(0,-1);

                bounds.X = trapRect.X;
                bounds.Y = trapRect.Y - attackRange;
                bounds.Width = trapRect.Width;
                bounds.Height = attackRange;
            }
            else //down by default
            {
                this.attackDirection = new Vector2(0, 1);

                bounds.Location = trapRect.Location;
                bounds.Width = trapRect.Width;
                bounds.Height = attackRange;
            }

            CollisionHandler.Instance.AddCollider(this,Layers.Enemy);
        }

        public TrapCollider()
        {

        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "TrapCollider";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player"))
            {
                BladeTrapRestState restingState = trapState as BladeTrapRestState;
                if (trapState is BladeTrapRestState)
                {
                    restingState.SetDirection(attackDirection, attackLength);
                    trapState.Attack();


                }



            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player"))
            {
                BladeTrapRestState restingState = trapState as BladeTrapRestState;
                if (trapState is BladeTrapRestState)
                {
                    restingState.SetDirection(attackDirection, attackLength);
                    trapState.Attack();

                    
                }
               

             
            }
            
        }

        public void SendMessage(string msg, object value)
        {
           //does not recieve messages
        }

        public void Update()
        {
            trapState = trap.state;
        }

    }
}
