using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5;
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

    public enum damageMove
    {
        up,
        down,
        left,
        right,
        none
    }



    public class LinkPlayer
    {
        public ILinkState state;
        const int max = 50;
        const int increment = 5;
        const int min = 0;
        const float HEALTH = 60;
        const int NUM_OF_RUPEE = 0;


        bool loc = false;
        private Direction damDir = Direction.none;
        private int counter;
        public Vector2 currentLocation;
        private bool isAttacking = false;
        private bool isDamaged = false;
        private double damageStartTime;
        private float health = HEALTH;
        private int moveHorizontal;
        private int moveVertical;
        public bool isWalkingInPlace;
        private bool isPickingUpItem = false;
        private bool useRing = false;
        private float fullHealth = HEALTH;
        private int delay;
        private bool clock = false;
        private bool largeShield = false;
        private bool drawShield = true;

        private bool isDead = false;

        public LinkSprite sprite;
        public Rectangle hitbox;


        public PlayerCollider collider;
        public WeaponCollider weaponCollider;
        private ShieldCollider shieldCollider;
        public ShieldCollider ShieldCollider { get => shieldCollider; set => shieldCollider = value; }

        public List<Direction> possibleDirections = Directions.Default();
        public Direction currentDirection;

        public bool isPaused = false;

        private bool isShooting = false;
        public bool IsShootingProjectile
        {
            get => isShooting;
            set => isShooting = value;
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

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public float Health
        {
            get { return health; }
            set { 
                health = value;
                if (health <= 0) { health = 0; Sounds.Instance.LinkDeath(); }
                else if (health <= HPAmount.OneHeart) Sounds.Instance.StartLowHealthLoop();
                else Sounds.Instance.StopLowHealthLoop();
            }
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

        public Direction DamDir { get => damDir; set => damDir = value; }
        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }

        private bool secondAttack = false;
        public bool IsSecondAttack
        {
            get { return secondAttack; }
            set { secondAttack = value; }
        }

        private bool isStopped = false;


        public bool IsStopped
        {
            get { return isStopped; }
            set { isStopped = value; }
        }


        private ItemForLink currentWeapon = ItemForLink.Shield;

        public ItemForLink CurrentWeapon
        {
            get { return currentWeapon; }
            set { currentWeapon = value; }

        }

        private ItemForLink secondWeapon = ItemForLink.ArrowBow;
        public ItemForLink SecondaryWeapon
        {
            get { return secondWeapon; }
            set { secondWeapon = value; }
        }

        public bool IsWalkingInPlace
        {
            get { return isWalkingInPlace; }
            set { isWalkingInPlace = value; }
        }

        public Vector2 CurrentLocation { get => currentLocation; set => currentLocation = value; }

        public bool IsPickingUpItem { get => isPickingUpItem; set => isPickingUpItem = value; }

        public bool UseRing { get => useRing; set => useRing = value; }

        public float FullHealth { get => fullHealth; set => fullHealth = value; }

        public int Delay { get => delay; set => delay = value; }
        public bool Clock { get => clock; set => clock = value; }
        public bool LargeShield { get => largeShield; set => largeShield = value; }
        public bool DrawShield { get => drawShield; set => drawShield = value; }
        public bool moveFromDamage { get => moveFromDamage; set => moveFromDamage = value; }

        public LinkPlayer(Game game)
        {

            sprite = SpriteFactory.Instance.CreateLinkSprite();
            
            hitbox = sprite.hitbox;
            hitbox.Location = currentLocation.ToPoint();
            hitbox = HitboxAdjuster.Instance.AdjustHitbox(hitbox, .9f);

            state = new Stationary(this, sprite);
            collider = new PlayerCollider(this);


            weaponCollider = new WeaponCollider(HPAmount.OneHit, this);
            shieldCollider = new ShieldCollider(this,HPAmount.OneHeart);

        }


        public void Update(GameTime gameTime)
        {
            if (sprite != null)
            { 
                hitbox = sprite.hitbox;
                int sizeX = hitbox.Size.X;
                int sizeY = hitbox.Size.Y;

                CurrentLocation = state.Update(gameTime, CurrentLocation);
                hitbox = new Rectangle(CurrentLocation.ToPoint(), new Point(sizeX, sizeY));

                if (damDir != Direction.none) this.push(damDir);
                delay--;


                possibleDirections = Directions.Default();
                Direction newDir = Directions.Parse(LinkDirection);
                if (newDir.Equals(currentDirection)) shieldCollider.ChangeDirection(newDir);
                currentDirection = newDir;
            }
        }
        public void HandleObstacle(Collision col)
        {
            possibleDirections.Remove(col.From);
        }
        public void MovingLeft()
        {
            if (!(state is MoveLeft))
                state = new MoveLeft(this, sprite);
            LinkDirection = "Left";
            
        }

        public void MovingRight()
        {
            if (!(state is MoveRight))
                state = new MoveRight(this, sprite);
            LinkDirection = "Right";
        }

        public void MovingUp()
        {
            if (!(state is MoveUp))
                state = new MoveUp(this, sprite);
            LinkDirection = "Up";
        }

        public void MovingDown()
        {
            if (!(state is MoveDown))
                state = new MoveDown(this, sprite);
            LinkDirection = "Down";
        }

        public void Stationary()
        {
            state = new Stationary(this, sprite);
        }

        public void Reset()
        {
            state = new Stationary(this, sprite);
            LocationInitialized = false;
            IsAttacking = false;
            IsDamaged = false;
            IsStopped = false;
            IsWalkingInPlace = false;
            IsPickingUpItem = false;
            LargeShield = false;
            UseRing = false;
            Health = HEALTH;
            FullHealth = HEALTH;
            Delay = 0;
            clock = false;
            DrawShield = true;

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

        public void knockback(Direction collideDir)
        {
            damDir = collideDir;
            counter = max;
        }

        public void stopKnockback()
        {
            damDir = Direction.none;
            counter = min;
        }

        public void push(Direction direction)
        {
            if (counter != max) {
                switch (direction)
                {
                    case Direction.right:
                        currentLocation.X += increment;
                        break;
                    case Direction.left:
                        currentLocation.X -= increment;
                        break;
                    case Direction.up:
                        currentLocation.Y -= increment;
                        break;
                    case Direction.down:
                        currentLocation.Y += increment;
                        break;
                }
            }

            counter -= increment;

            if (counter <= min) 
            {
                this.stopKnockback();
            }
        }
    }
}
