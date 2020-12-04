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
        private Vector2 localPosition;

        private double SIZE = 1.5;
        private int ZERO = 0;
        private int ONE = 1;
        private int NEG_ONE = -1;
        private int linkSize;
        private int weaponLength;

        private LinkPlayer link;
        private Direction currentDirection;

        private Dictionary<Direction, Vector2> directionOffset;
        public string Name { get => "Sword"; }

        public Layer layer { get; set; }

        private int turnOffClock = 0;
        private const int TIME = 10; // by 1/60 of second

        private ItemForLink current { get => link.CurrentWeapon; }
        private const ItemForLink wood = ItemForLink.WoodenSword;
        private const ItemForLink metal = ItemForLink.Sword;
        private const ItemForLink magic = ItemForLink.MagicalRod;

        private Dictionary<ItemForLink, int> weaponPower;

        public WeaponCollider(int attack, LinkPlayer link)
        {
            this.link = link;
            damageAmount = attack;
            linkSize = link.hitbox.Width;
            weaponLength = (int)(linkSize * SIZE);

            bounds = new Rectangle(link.currentLocation.ToPoint(), new Point(linkSize, linkSize));

            localPosition = Vector2.Zero;

            float posOffset = weaponLength / linkSize;
            directionOffset = new Dictionary<Direction, Vector2>()
            {
                {Direction.left, new Vector2(NEG_ONE * posOffset,ZERO) },
                {Direction.right, new Vector2(ONE,ZERO) },
                {Direction.up, new Vector2(ZERO,NEG_ONE * posOffset) },
                {Direction.down, new Vector2(ZERO,ONE) }
            };

            weaponPower = new Dictionary<ItemForLink, int>()
            {
                {wood, HPAmount.QuarterHeart },
                {metal, HPAmount.HalfHeart },
                {magic, HPAmount.ThreeQuarterHeart }
            };

            CollisionHandler.Instance.AddCollider(this, Layers.PlayerWeapon);
        }

        private void CalculateBounds(Direction dir)
        {
            if (!dir.Equals(currentDirection))
            {
                localPosition = directionOffset[dir];
                localPosition.X *= linkSize;
                localPosition.Y *= linkSize;

                if (dir.Equals(Direction.left) || dir.Equals(Direction.right)) bounds.Size = new Point(weaponLength, linkSize);
                else bounds.Size = new Point(linkSize, weaponLength);
                currentDirection = dir;
            }

        }

        public void TurnOn(Direction dir)
        {
            bool primary = (current is wood || current is metal || current is magic) && !link.IsSecondAttack;
            
            if(turnOffClock == 0 && primary)
            {
                CalculateBounds(dir);
                turnOffClock = TIME;               
                damageAmount = weaponPower[current];

                CollisionHandler.Instance.AddCollider(this, Layers.PlayerWeapon);
                Sounds.Instance.Play("SwordSlash");
            }
            
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
            bounds.Location = link.currentLocation.ToPoint() + localPosition.ToPoint(); ;
            if (turnOffClock > 1) turnOffClock--;
            else if (turnOffClock == 1) { turnOffClock--; TurnOff(); }
        }
    }
}
