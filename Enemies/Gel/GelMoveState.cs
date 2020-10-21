using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Sprint3
{
    /// <summary>
    ///Moves gel either up or down to another tile and stops
    ///<para>Currently traverses entire window, subspace could be provided as rectangle</para>
    /// </summary>
    public class GelMoveState : IEnemyState
    {

        private Vector2 location;
        private Rectangle locationRectangle;
        private Rectangle nextLoc;
        private Rectangle lastPositionOnGrid;
        
        private const int tileColumns = 12;
        private const int tileRows = 7;
        private List<List<Rectangle>> gridTiles;
        private Point tileSize;
        private int currentRow;
        private int currentCol;


        private enum Direction { left, right, up, down };
        List<Direction> possibleDirections;
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;



        private Vector2 moveDirection;
        private Gel gelly;

        private bool stoppedMoving;
        private int coolDownClock;
        private readonly int[] coolDownTimes = { 10, 25,  50 };
        private int coolDownTime;

        private const int moveSpeed = 2;

        Random RandomNumber;

        private int timeSinceLastCollision = 0;

        public GelMoveState(Gel gel, Vector2 location, Game game)
        {
            this.location = location;
            gelly = gel;

            gelly.SetSprite(EnemySpriteFactory.Instance.CreateGelMoveSprite());
            

            gridTiles = GridGenerator.Instance.GetGrid();
            locationRectangle = GridGenerator.Instance.GetGridLocation(location);
            lastPositionOnGrid = locationRectangle;

            this.location = locationRectangle.Location.ToVector2();
            gelly.UpdateLocation(this.location);

            tileSize = GridGenerator.Instance.GetTileSize();

            currentCol = locationRectangle.X / tileSize.X;
            currentRow = locationRectangle.Y / tileSize.Y;

            stoppedMoving = true;

            RandomNumber = new Random();

            coolDownClock = coolDownTimes[RandomNumber.Next(0, coolDownTimes.Length)];

            possibleDirections = new List<Direction> { left, right, up, down };

            
        }

        public void Attack()
        {
            //does nothing
        }

        //choose new tile on current row or column and change movedirection
        public void ChangeDirection()
        {

            bool moveWithinRow = RandomNumber.Next(0, 2) == 0;
            bool moveWithinColumn = !moveWithinRow;

            int nextRow;
            int nextCol;
            
            if(possibleDirections.Count == 4) // can move in any direction
            {
                nextRow = moveWithinColumn ? RandomNumber.Next(0, tileRows) : currentRow;
                nextCol = moveWithinRow ? RandomNumber.Next(0, tileColumns) : currentCol;
            }
            else //block or wall is blocking path
            {

                if (locationRectangle.X % tileSize.X != 0 || locationRectangle.Y % tileSize.Y != 0)
                {
                    locationRectangle = lastPositionOnGrid; location = locationRectangle.Location.ToVector2();
                    currentCol = locationRectangle.X / locationRectangle.Width;
                    currentRow = locationRectangle.Y / locationRectangle.Height;
                    location = locationRectangle.Location.ToVector2();
                    gelly.UpdateLocation(location);
                }
              

                int rowMin = MinBound(up, currentRow), colMin = MinBound(left, currentCol);
                int rowMax = MaxBound(down, currentRow, tileRows), colMax = MaxBound(right, currentCol, tileColumns);

                nextRow = moveWithinColumn ? RandomNumber.Next(rowMin, rowMax) : currentRow;
                nextCol = moveWithinRow ? RandomNumber.Next(colMin, colMax) : currentCol;
            }
            

            nextLoc = gridTiles[nextRow][nextCol];

            moveDirection.Y = CompareTwoNums(nextRow, currentRow);
            moveDirection.X = CompareTwoNums(nextCol, currentCol);
            currentDirection = CalculateDirection();

        }

        public float CompareTwoNums(float num1, float num2)
        {
            if (num1 < num2) return -1;
            if (num1 > num2) return 1;
            return 0;
        }

        private Direction CalculateDirection()
        {
            if (moveDirection.X > 0) return right;
            else if (moveDirection.X < 0) return left;
            else if (moveDirection.Y > 0) return down;
            else return up;
        }

        private int MinBound(Direction dir, int current)
        {
            if (!possibleDirections.Contains(dir)) return current;
            return 0;
        }

        private int MaxBound(Direction dir, int current, int max)
        {
            if (!possibleDirections.Contains(dir)) return current;
            return max;
        }

        public void Update()
        {
            
            if (stoppedMoving && DoneWithCoolDown())
            {
                ResetCoolDownClock();
                ChangeDirection();
                stoppedMoving = false;

            }
            else if(locationRectangle.X == nextLoc.X && locationRectangle.Y == nextLoc.Y)
            {
                stoppedMoving = true;
                coolDownClock = Math.Max(0, coolDownClock - 1);
            }
            else
            {
                MoveOneUnit();                
            }

            //update location on grid
            currentCol = locationRectangle.X / locationRectangle.Width;
            currentRow = locationRectangle.Y / locationRectangle.Height;
            if (locationRectangle.X % tileSize.X == 0 && locationRectangle.Y % tileSize.Y == 0) lastPositionOnGrid = locationRectangle;

            //reset movement possibilities after a collision
            if (timeSinceLastCollision > 10) possibleDirections = new List<Direction> { left, right, up, down };
            else timeSinceLastCollision++;

        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            locationRectangle.Location = location.ToPoint();  
            gelly.UpdateLocation(location);
        }

        public void ResetCoolDownClock()
        {            
            coolDownClock = coolDownTimes[RandomNumber.Next(0,coolDownTimes.Length)];
            coolDownTime = coolDownClock;
        }

        public bool DoneWithCoolDown()
        {

            return coolDownClock <= 0 || coolDownClock == coolDownTime || coolDownTime == default;
        }

        

        public void Die()
        {
            //change to dying sprite
        }

        public void TakeDamage()
        {
            Die();
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            timeSinceLastCollision = 0;
            possibleDirections.Remove((Direction)collision.From());
            if (!possibleDirections.Contains(currentDirection)) ChangeDirection();
            
        }

        public void TakeDamage(int amount)
        {
           //
        }
    }
}
