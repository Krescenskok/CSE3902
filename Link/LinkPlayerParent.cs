using System;
using Microsoft.Xna.Framework;
using Sprint5.Link;

namespace Sprint5
{

    public enum ItemForLink
    {
        Shield,
        WoodenSword,
        Sword,
        MagicalRod,
        ArrowBow,
        BlueRing,
        Boomerang,
        BlueCandle,
        Bomb,
        Clock
    }


    public class LinkPlayerParent
    {
        public ILinkState state;
        const float HEALTH = 60;
        const int NUM_OF_RUPEE = 0;
        bool loc = false;
        public Vector2 currentLocation;
        private bool isAttacking = false;
        private bool isDamaged = false;
        private double damageStartTime;
        private float health = HEALTH;
        public bool isWalkingInPlace;
        private bool isPickingUpItem = false;
        private bool useRing = false;
        private float fullHealth = HEALTH;
        private int delay;
        private bool clock = false;
        private bool largeShield = false;
        private bool drawShield = true;
        private bool isDead = false;
        private bool isShooting = false;

        public LinkSprite sprite;
        public Rectangle hitbox;

        public float Health
        {
            get { return health; }
            set { health = value; if (health <= 0) health = 0; }
        }

        public bool IsDamaged
        {
            get { return isDamaged; }
            set
            {
                isDamaged = value;
                if (value == false)
                {
                    DamageStartTime = 0;
                }
            }
        }


        string direction = "Down";
        private bool secondAttack = false;
        private bool isStopped = false;
        private ItemForLink currentWeapon = ItemForLink.MagicalRod;

        public string LinkDirection { get => direction; set => direction = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
        public Boolean LocationInitialized { get => loc; set => loc = value; }
        public double DamageStartTime { get => damageStartTime; set => damageStartTime = value; }
        public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
        public bool IsSecondAttack { get => secondAttack; set => secondAttack = value; }
        public bool IsStopped { get => isStopped; set => isStopped = value; }
        public ItemForLink CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }
        private ItemForLink secondWeapon = ItemForLink.ArrowBow;
        public ItemForLink SecondaryWeapon { get => secondWeapon; set => secondWeapon = value; }
        public bool IsWalkingInPlace { get => isWalkingInPlace; set => isWalkingInPlace = value; }
        public Vector2 CurrentLocation { get => currentLocation; set => currentLocation = value; }
        public bool IsPickingUpItem { get => isPickingUpItem; set => isPickingUpItem = value; }
        public bool UseRing { get => useRing; set => useRing = value; }
        public float FullHealth { get => fullHealth; set => fullHealth = value; }
        public int Delay { get => delay; set => delay = value; }
        public bool Clock { get => clock; set => clock = value; }
        public bool LargeShield { get => largeShield; set => largeShield = value; }
        public bool DrawShield { get => drawShield; set => drawShield = value; }
        public bool IsShootingProjectile { get => isShooting; set => isShooting = value; }

        public LinkPlayerParent()
        {
        }
    }
}
