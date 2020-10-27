using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Items;

namespace Sprint3.Link
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


    public class LinkPlayer
    {
        public ILinkState state;

        bool loc = false;
        public Vector2 currentLocation;
        private bool isAttacking = false;
        private bool isDamaged = false;
        private double damageStartTime;
        private float health = 60;
        public bool isWalkingInPlace = false;
        private bool isPickingUpItem = false;
        private int numOfRupee = 0;
        private bool useRing = false;
        private int fullHealth = 60;
        private int delay = 2;
        private bool clock = false;
        public List<IItems> itemsPickedUp;

        private PlayerCollider collider;

        public List<IItems> itemsPlacedByLink = new List<IItems>();
        public void RemovePlacedItem(IItems item)
        {
            if (itemsPlacedByLink.Contains(item))
            {
                itemsPlacedByLink.Remove(item);
            }
            
        }


        string direction = "Down";
        public string LinkDirection
        {
            get { return direction; }
            set { direction = value; }
        }

        public Rectangle Bounds
        {
            get
            {

                Rectangle t = new Rectangle((int)CurrentLocation.X, (int)CurrentLocation.Y, 32, 32);
                return t;
            }
        }

        public float Health
        {
            get { return health; }
            set { health = value; if (health <= 0) health = 0; }
        }

        public Boolean LocationInitialized
        {
            get { return loc; }
            set { loc = value; }
        }

        public double DamageStartTime
        {
            get { return damageStartTime; }
            set { damageStartTime = value; }
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

        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }


        private bool isStopped = false;


        public bool IsStopped
        {
            get { return isStopped; }
            set { isStopped = value; }
        }


        private ItemForLink currentWeapon = ItemForLink.WoodenSword;


        public ItemForLink CurrentWeapon
        {
            get { return currentWeapon; }
            set { currentWeapon = value; }

        }

        public bool IsWalkingInPlace
        {
            get { return isWalkingInPlace; }
            set { isWalkingInPlace = value; }
        }

        public Vector2 CurrentLocation { get => currentLocation; set => currentLocation = value; }

        public bool IsPickingUpItem { get => isPickingUpItem; set => isPickingUpItem = value; }

        public int NumOfRupee { get => numOfRupee; set => numOfRupee = value; }

        public bool UseRing { get => useRing; set => useRing = value; }

        public int FullHealth { get => fullHealth; set => fullHealth = value; }

        public int Delay { get => delay; set => delay = value; }
        public bool Clock { get => clock; set => clock = value; }

        public LinkPlayer()
        {

            state = new Stationary(this);
            collider = new PlayerCollider(this);

        }
        public void Update(GameTime gameTime)
        {

            CurrentLocation = state.Update(gameTime, CurrentLocation);
            delay--;

        }

        public void MovingLeft()
        {
            if (!(state is MoveLeft))
                state = new MoveLeft(this);
            LinkDirection = "Left";
        }

        public void MovingRight()
        {
            if (!(state is MoveRight))
                state = new MoveRight(this);
            LinkDirection = "Right";
        }

        public void MovingUp()
        {
            if (!(state is MoveUp))
                state = new MoveUp(this);
            LinkDirection = "Up";
        }

        public void MovingDown()
        {
            if (!(state is MoveDown))
                state = new MoveDown(this);
            LinkDirection = "Down";
        }

        public void Stationary()
        {
            state = new Stationary(this);
        }

        public void Reset()
        {
            state = new Stationary(this);
            Health = 100;

            LocationInitialized = false;
            IsAttacking = false;
            IsDamaged = false;
            IsStopped = false;
            IsWalkingInPlace = false;
            IsPickingUpItem = false;


        }

        public void Draw(Game game, SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (loc == false)
            {
                loc = true;
                CurrentLocation = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            }

            state.Draw(spriteBatch, gameTime, CurrentLocation);
        }





    }
}
