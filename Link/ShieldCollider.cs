﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class ShieldCollider : ICollider
    {

        private int strength;
        private Rectangle bounds;
        private Vector2 localPosition;
        private int linkSize;
        private int shieldWidth;

        private int ZERO = 0;
        private int NEG_ONE = -1;
        private int ONE = 1;
        private int TWO = 2;


        private LinkPlayer link;
        private Direction currentDirection;

        private Dictionary<Direction, Vector2> directionOffset;
        public string Name { get => "Shield"; }

        public Layer layer { get; set; }

        private TestCollider testCol;
        public ShieldCollider(LinkPlayer link, int strength)
        {
            this.link = link;
            this.strength = strength;
            linkSize = link.hitbox.Width;
            shieldWidth = linkSize / 4;

            bounds = new Rectangle(link.currentLocation.ToPoint(), new Point(linkSize, shieldWidth));

            localPosition = Vector2.Zero;

            float posOffset = shieldWidth / linkSize;
            directionOffset = new Dictionary<Direction, Vector2>()
            {
                {Direction.left, new Vector2(NEG_ONE * posOffset,ZERO) },
                {Direction.right, new Vector2(ONE,ZERO) },
                {Direction.up, new Vector2(ZERO,NEG_ONE * posOffset) },
                {Direction.down, new Vector2(ZERO,ONE) }
            };

            CollisionHandler.Instance.AddCollider(this, Layers.Shield);
        }

        public void ChangeDirection(Direction dir)
        {

            if (!dir.Equals(currentDirection))
            {
                localPosition = directionOffset[dir];
                localPosition.X *= linkSize;
                localPosition.Y *= linkSize;

                if (dir.Equals(Direction.left) || dir.Equals(Direction.right)) bounds.Size = new Point(shieldWidth, linkSize);
                else bounds.Size = new Point(linkSize, shieldWidth);

                currentDirection = dir;
            }

        }

        public void TurnOn(Direction dir)
        {
            ChangeDirection(dir);

            CollisionHandler.Instance.AddCollider(this, Layers.PlayerWeapon);
            testCol = RoomEnemies.Instance.AddTestCollider(bounds,this);
        }

        public void TurnOff()
        {

            CollisionHandler.Instance.RemoveCollider(this);
            RoomEnemies.Instance.RemoveTest(testCol);
        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Shield";
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

        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {

        }

        public void SendMessage(string msg, object value)
        {
            Direction from = Directions.Parse(msg.Substring(10));
            from = Directions.Opposite(from);
            bool hit = from.Equals(link.CurrentDirection);

            if (hit && msg.Contains("TakeDamage"))
            {
                int damage = link.UseRing ? (int)value / TWO : (int)value;
                damage = Math.Max(damage - strength, ZERO);
                if(damage > ZERO)
                {
                    link.IsDamaged = true;
                    link.Health -= damage;
                    //link.knockback(from);
                }
                else
                {
                    Sounds.Instance.Play("Shield");
                }
            }
        }

        public void Update()
        {
            bounds.Location = link.currentLocation.ToPoint() + localPosition.ToPoint();

        }
    }
}
