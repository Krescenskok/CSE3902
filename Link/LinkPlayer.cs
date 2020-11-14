using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4;
using Sprint4.Link;

namespace Sprint4.Link
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
        const float HEALTH = 60;
        const int NUM_OF_RUPEE = 0;


        bool loc = false;
        public Vector2 currentLocation;
        private bool isAttacking = false;
        private bool isDamaged = false;
        private double damageStartTime;
        private float health = HEALTH;
        public bool isWalkingInPlace = false;
        private bool isPickingUpItem = false;
        private int numOfRupee = NUM_OF_RUPEE;
        private bool useRing = false;
        private float fullHealth = HEALTH;
        private int delay;
        private bool clock = false;
        private bool largeShield = false;
        public List<IItems> itemsPickedUp;
        private bool drawShield = true;

        private PlayerCollider collider;

        public List<IItems> itemsPlacedByLink = new List<IItems>();

        public List<Direction> possibleDirections = Directions.Default();

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
                return state.Bounds();
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

        public void HandleObstacle(Collision col)
        {
            possibleDirections.Remove(col.From);
        }

        public Vector2 CurrentLocation { get => currentLocation; set => currentLocation = value; }

        public bool IsPickingUpItem { get => isPickingUpItem; set => isPickingUpItem = value; }

        public int NumOfRupee { get => numOfRupee; set => numOfRupee = value; }

        public bool UseRing { get => useRing; set => useRing = value; }

        public float FullHealth { get => fullHealth; set => fullHealth = value; }

        public int Delay { get => delay; set => delay = value; }
        public bool Clock { get => clock; set => clock = value; }
        public bool LargeShield { get => largeShield; set => largeShield = value; }
        public bool DrawShield { get => drawShield; set => drawShield = value; }

        public LinkPlayer()
        {

            state = new Stationary(this);
            collider = new PlayerCollider(this);

        }
        public void Update(GameTime gameTime)
        {

            CurrentLocation = state.Update(gameTime, CurrentLocation);
            delay--;


            possibleDirections = Directions.Default();
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
            LocationInitialized = false;
            IsAttacking = false;
            IsDamaged = false;
            IsStopped = false;
            IsWalkingInPlace = false;
            IsPickingUpItem = false;
            LargeShield = false;
            UseRing = false;
            Health = HEALTH;
            NumOfRupee = NUM_OF_RUPEE;
            FullHealth = HEALTH;
            Delay = 0;
            clock = false;
            DrawShield = true;
            if (itemsPickedUp != null && itemsPickedUp.Count > 0)
            {
                itemsPickedUp.Clear();
            }


        }

        public void Draw(Game game, SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (loc == false)
            {
                loc = true;
                CurrentLocation = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2) - Camera.Instance.Location;
            }

            state.Draw(spriteBatch, gameTime, CurrentLocation);
        }





    }
}
