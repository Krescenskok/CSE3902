using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5;
using Sprint5.Link;

namespace Sprint5
{


    public class LinkPlayer : LinkPlayerParent
    {
 
        const float HEALTH = 60;

        const int NUM_OF_RUPEE = 0;

        private float fullHealth = HEALTH;


        public PlayerCollider collider;

        public List<IItems> itemsPlacedByLink = new List<IItems>();

        public List<Direction> possibleDirections = Directions.Default();

        public bool isPaused = false;

        public void RemovePlacedItem(IItems item)
        {
            if (itemsPlacedByLink.Contains(item) && item.IsExpired)
            {
                itemsPlacedByLink.Remove(item);
            }
        }

        public Rectangle Bounds { get => state.Bounds(); }


        public LinkPlayer()
        {
            sprite = (LinkSprite)SpriteFactory.Instance.CreateLinkSprite();
            hitbox = sprite.hitbox;
            state = new Stationary(this, sprite);
            collider = new PlayerCollider(this);
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
                Delay--;


                possibleDirections = Directions.Default();
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
            Clock = false;
            DrawShield = true;

        }

        public void Draw(Game game, SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (LocationInitialized == false)
            {
                LocationInitialized = true;
                CurrentLocation = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2) - Camera.Instance.Location;
            }

            state.Draw(spriteBatch, gameTime, CurrentLocation);
        }
    }
}
