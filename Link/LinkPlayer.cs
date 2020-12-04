using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5;
using Sprint5.Link;
using Sprint5.DifficultyHandling;

namespace Sprint5
{


    public class LinkPlayer : LinkPlayerParent
    {
        private const float HEALTH = 60;
        private const int NUM_OF_RUPEE = 0;

        public PlayerCollider collider { get; set; }
        public WeaponCollider weaponCollider { get; set; }
        public ShieldCollider ShieldCollider { get; set; }

        private float fullHealth = HEALTH;

        private const int max = 50;
        private const int increment = 5;
        private const int min = 0;

        private Game1 Game;

        private List<IItems> ItemsPlacedByLink = new List<IItems>();
        private bool IsPaused = false;

        public bool Paused { get => IsPaused; set => IsPaused = value; }

        public void RemovePlacedItem(IItems item)
        {
            if (itemsPlacedByLink.Contains(item) && item.IsExpired)
            {
                itemsPlacedByLink.Remove(item);
            }
        }
        public Rectangle Bounds { get => state.Bounds(); }
        public List<IItems> itemsPlacedByLink { get => ItemsPlacedByLink; set => ItemsPlacedByLink = value; }
        public bool isPaused { get => IsPaused; set => IsPaused = value; }
        public List<Direction> PossibleDirections { get; set; } = Directions.Default();

        public LinkPlayer(Game1 game)
        {
            sprite = SpriteFactory.Instance.CreateLinkSprite();
            hitbox = sprite.hitbox;
            state = new Stationary(this, sprite);

            collider = new PlayerCollider(this, HitboxAdjuster.Instance.AdjustHitbox(hitbox, .9f));
            weaponCollider = new WeaponCollider(HPAmount.OneHit, this);
            ShieldCollider = new ShieldCollider(this,HPAmount.Full_Heart);

            Counter = min;
            Game = game;
            DifficultyMultiplier.Instance.DetermineLinkHP(this);
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

                PossibleDirections = Directions.Default();
                Direction newDir = Directions.Parse(LinkDirection);
                if (newDir.Equals(CurrentDirection)) ShieldCollider.ChangeDirection(newDir);
                CurrentDirection = newDir;

                if (DamDir != Direction.none) push(DamDir);
                Delay--;

                PossibleDirections = Directions.Default();
            }
        }
        public void HandleObstacle(Collision col)
        {
            PossibleDirections.Remove(col.From);
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

        public void EnterSecretRoom() {
            currentLocation = new Vector2(GridGenerator.Instance.GetTileSize().X * 3,0) - Camera.Instance.Location;
            state = new MoveDown(this, sprite);
        }

        public void LeaveSecretRoom() {
            currentLocation = new Vector2(GridGenerator.Instance.GetTileSize().X * 8, GridGenerator.Instance.GetTileSize().Y *5 ) - Camera.Instance.Location;
            state = new MoveLeft(this, sprite);
        }

        public void knockback(Direction collideDir)
        {
            Counter = max;
        }

        public void stopKnockback()
        {
            Counter = min;
        }

        public void push(Direction direction)
        {
            if (Counter != max) {
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

            Counter -= increment;

            if (Counter <= min)
                this.stopKnockback();
        }
    }
}
