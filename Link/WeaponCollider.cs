using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class WeaponCollider : ICollider
    {
        private string name;
        private int damageAmount;
        private Rectangle bounds;

        private Point localPosition;

        

        private int size;
       

        private LinkPlayer link;
        private Direction currentDirection;

        private Dictionary<Direction, Point> directionOffset;
        public string Name { get => "Sword"; }

        public Layer layer { get; set; }

        private TestCollider testCol;
        public WeaponCollider(int attack, LinkPlayer link)
        {
            this.link = link;
            damageAmount = attack;
            CollisionHandler.Instance.AddCollider(this, Layers.PlayerWeapon);

            
            size = link.hitbox.Width;
           

            bounds = new Rectangle(link.currentLocation.ToPoint(), new Point(size, size));
            

            localPosition = Vector2.Zero.ToPoint();

            directionOffset = new Dictionary<Direction, Point>()
            {
                {Direction.left, new Point(-1,0) },
                {Direction.right, new Point(1,0) },
                {Direction.up, new Point(0,-1) },
                {Direction.down, new Point(0,1) }
            };
        }

        private void CalculateBounds(Direction dir)
        {

            if (!dir.Equals(currentDirection))
            {
                localPosition = directionOffset[dir];
                localPosition.X *= size;
                localPosition.Y *= size;
                

                currentDirection = dir;
            }
          
        }

        public void TurnOn(Direction dir)
        {
            CalculateBounds(dir);
            
            CollisionHandler.Instance.AddCollider(this, Layers.PlayerWeapon);
            
        }

        public void TurnOff()
        {
            
            CollisionHandler.Instance.RemoveCollider(this);

        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Sword";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            string dir = collision.From.ToString();

            if (col.CompareTag("Enemy")) col.SendMessage("EnemyTakeDamage" + dir, damageAmount);
        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
            
        }

        public void SendMessage(string msg, object value)
        {
           
        }

        public void Update()
        {
            bounds.Location = link.currentLocation.ToPoint() + localPosition;
            
        }
    }
}
