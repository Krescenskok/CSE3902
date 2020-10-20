using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public enum Weapon
    {
        Shield,
        WoodenSword,
        Sword,
        MagicalRod,

    }
    public class LinkPlayer : ILinkState
    {
        public ILinkState state;

        bool loc = false;
        Vector2 currentLocation;
        private bool isAttacking = false;
        private bool isDamaged = false;
        private double damageStartTime;
        private float health = 100;



        public Rectangle Bounds
        {
            get {

                Rectangle t =  new Rectangle((int)currentLocation.X, (int)currentLocation.Y, 32, 32);
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


        private Weapon currentWeapon = Weapon.Shield;


        public Weapon CurrentWeapon
        {
            get { return currentWeapon; }
            set { currentWeapon = value; }

        }


        public LinkPlayer()
        {

            state = new Stationary(this);

        }
        public void Update(GameTime gameTime)
        {

            currentLocation = state.Update(gameTime, currentLocation);
        }

        public void MovingLeft()
        {
            state.MovingLeft();
        }

        public void MovingRight()
        {
            state.MovingRight();
        }

        public void MovingUp()
        {
            state.MovingUp();
        }

        public void MovingDown()
        {
            state.MovingDown();
        }

        public void Reset()
        {
            state.Stationary();
            Health = 100;

            LocationInitialized = false;
            IsAttacking = false;
            IsDamaged = false;
            IsStopped = false;

          
        }

        public void Draw(Game game, SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (loc == false)
            {
                loc = true;
                currentLocation = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            }

            state.Draw(spriteBatch, gameTime, currentLocation);
        }

        public void Stationary()
        {
            state.Stationary();
        }

        public Vector2 Update(GameTime gameTime, Vector2 location)
        {

            return location;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location)
        {

        }


    }
}