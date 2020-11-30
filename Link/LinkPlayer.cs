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

        private float fullHealth = HEALTH;

        private const int max = 50;
        private const int increment = 5;
        private const int min = 0;

        private PlayerCollider collider;

        private Game1 Game;

        private List<IItems> ItemsPlacedByLink = new List<IItems>();

       

        private List<Direction> PossibleDirections = Directions.Default();

        private bool IsPaused = false;

        public void RemovePlacedItem(IItems item)
        {
            if (itemsPlacedByLink.Contains(item) && item.IsExpired)
            {
                itemsPlacedByLink.Remove(item);
            }
        }

        public damageMove DamDir { get => damDir; set => damDir = value; }
        public Rectangle Bounds { get => state.Bounds(); }
        public List<IItems> itemsPlacedByLink { get => ItemsPlacedByLink; set => ItemsPlacedByLink = value; }
        public bool isPaused { get => IsPaused; set => IsPaused = value; }
        public List<Direction> possibleDirections { get => PossibleDirections; set => PossibleDirections = value; }

        public LinkPlayer(Game1 game)
        {
            sprite = (LinkSprite)SpriteFactory.Instance.CreateLinkSprite();
            hitbox = sprite.hitbox;
            state = new Stationary(this, sprite);
            collider = new PlayerCollider(this);
            counter = min;
            Game = game;
            damDir = damageMove.none;
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
                if (damDir != damageMove.none) this.push(damDir);
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
        
        public void moveCentral() {
            currentLocation = new Vector2(GridGenerator.Instance.GetTileSize().X * 3, GridGenerator.Instance.GetTileSize().Y * 2) - Camera.Instance.Location; 
        }

        public void moveSecret() { 
            currentLocation = new Vector2(GridGenerator.Instance.GetTileSize().X * 7, GridGenerator.Instance.GetTileSize().Y *5 ) - Camera.Instance.Location;
            System.Diagnostics.Debug.WriteLine(currentLocation);
        }

        public void knockback(damageMove collideDir)
        {
            damDir = collideDir;
            counter = max;
        }

        public void stopKnockback()
        {
            damDir = damageMove.none;
            counter = min;
        }

        public void push(damageMove direction)
        {
            if (counter != max) {
                switch (direction)
                {
                    case damageMove.right:
                        currentLocation.X += increment;
                        break;
                    case damageMove.left:
                        currentLocation.X -= increment;
                        break;
                    case damageMove.up:
                        currentLocation.Y -= increment;
                        break;
                    case damageMove.down:
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
