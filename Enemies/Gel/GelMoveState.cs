using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Sprint4
{
    /// <summary>
    ///Moves gel either up or down to another tile and stops
    ///<para>Currently traverses entire window, subspace could be provided as rectangle</para>
    /// </summary>
    public class GelMoveState : IEnemyState
    {
        private Gel gelly;
        private Vector2 location;
        

        private Rectangle currentSpace, targetSpaceOnGrid, nextSpaceOnGrid,lastSpaceOnGrid;
        private List<Rectangle> pathSpaces;
        
        
        private const int tileColumns = 12;
        private const int tileRows = 7;
        private List<List<Rectangle>> gridTiles;
        private Point tileSize;
        private int currentRow, currentCol;
       

        private enum Direction { left, right, up, down };
        List<Direction> possibleDirections;
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;
        private Vector2 moveDirection;

        private bool stoppedMoving;
        private int coolDownClock, coolDownTime;
        private readonly int[] coolDownTimes = { 10, 25,  50 };

        private const float moveSpeed = 2;
        private float speed = moveSpeed;
        private float savedSpeed = moveSpeed;
        private const int stunTime = 120;
        private int stunClock = 0;
        Random RandomNumber;

        public GelMoveState(Gel gel, Vector2 location, Game game)
        {
            this.location = location;
            gelly = gel;

            gelly.SetSprite(EnemySpriteFactory.Instance.CreateGelMoveSprite());
            

            gridTiles = GridGenerator.Instance.GetGrid();
            currentSpace = GridGenerator.Instance.GetGridLocation(location);
            tileSize = GridGenerator.Instance.GetTileSize();
           
            

            lastSpaceOnGrid = currentSpace;
            nextSpaceOnGrid = currentSpace;
            this.location = currentSpace.Location.ToVector2();
            gelly.UpdateLocation(this.location);

            currentCol = currentSpace.X / tileSize.X;
            currentRow = currentSpace.Y / tileSize.Y;

            stoppedMoving = true;
            RandomNumber = new Random();

            coolDownClock = coolDownTimes[RandomNumber.Next(0, coolDownTimes.Length)];

            possibleDirections = new List<Direction> { left, right, up, down };
        }

        public void Update()
        {

            if (DoneBeingStunned()) speed = savedSpeed;

            if (stoppedMoving && DoneWithCoolDown())
            {
                ResetCoolDownClock();
                ChangeDirection();
                stoppedMoving = false;

            }
            else if (Arrived())
            {
                stoppedMoving = true;
                coolDownClock = Math.Max(0, coolDownClock - 1);
               
            }
            else
            {
                MoveOneUnit();
                
            }

            //update location on grid
            currentCol = currentSpace.X / currentSpace.Width;
            currentRow = currentSpace.Y / currentSpace.Height;


            //Update last space visited and next space to visit on grid
            
            if (currentSpace.Intersects(nextSpaceOnGrid))
            {
                lastSpaceOnGrid = nextSpaceOnGrid;
                int currentIndex = pathSpaces.IndexOf(nextSpaceOnGrid);
                if (currentIndex < pathSpaces.Count - 1) nextSpaceOnGrid = pathSpaces[currentIndex + 1];
            }
        }

        //Changes direction if block or wall is in the way and gel have finished moving to its next space
        public void CheckIfBlockingPath(ICollider col, Collision collision)
        {

            possibleDirections.Remove((Direction)collision.From());
            if (!possibleDirections.Contains(currentDirection) && LiesOnPath(col.Bounds()) && lastSpaceOnGrid == currentSpace)
            {

                ChangeDirection();

                
            }

        }

        //choose new tile on current row or column and change movedirection
        public void ChangeDirection()
        {
            bool leftRightOpen = possibleDirections.Contains(left) || possibleDirections.Contains(right);
            bool upDownOpen = possibleDirections.Contains(up) || possibleDirections.Contains(down);

            bool moveWithinRow = (RandomNumber.Next(0, 2) == 0 && leftRightOpen) || !upDownOpen;
            bool moveWithinColumn = !moveWithinRow;

            int nextRow = currentRow, nextCol = currentCol;
            
            if(possibleDirections.Count == 4) // path is open in all directions
            {
                while(currentCol == nextCol && currentRow == nextRow)
                {
                    nextRow = moveWithinColumn ? RandomNumber.Next(0, tileRows) : currentRow;
                    nextCol = moveWithinRow ? RandomNumber.Next(0, tileColumns) : currentCol;
                }
            }
            else //block or wall is blocking path
            {
                int rowMin = MinBound(up, currentRow), colMin = MinBound(left, currentCol);
                int rowMax = MaxBound(down, currentRow, tileRows), colMax = MaxBound(right, currentCol, tileColumns);

                while (currentCol == nextCol && currentRow == nextRow)
                {
                    nextRow = moveWithinColumn ? RandomNumber.Next(rowMin, rowMax) : currentRow;
                    nextCol = moveWithinRow ? RandomNumber.Next(colMin, colMax) : currentCol;
                }
                
            }
            
            //determine next space to move to
            targetSpaceOnGrid = gridTiles[nextRow][nextCol];

            //set move vector to increment location by
            moveDirection.Y = CompareTwoNums(nextRow, currentRow);
            moveDirection.X = CompareTwoNums(nextCol, currentCol);
            currentDirection = CalculateDirection();

            //get list of spaces between current space and destination
            pathSpaces = GridGenerator.Instance.GetStraightPath(currentSpace, targetSpaceOnGrid);
            nextSpaceOnGrid = pathSpaces[1];
            currentSpace = pathSpaces[0];
            
            //adjust speed so that gel doesn't skip over last space
            if (moveWithinRow && (targetSpaceOnGrid.X - currentSpace.X) % moveSpeed != 0) speed = moveSpeed - 1;
            else if (moveWithinColumn && tileSize.Y % moveSpeed != 0) speed = moveSpeed - 1;
            else speed = moveSpeed;
        }

        #region //helper methods
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
        
        private bool Arrived()
        {
            return currentSpace.X == targetSpaceOnGrid.X && currentSpace.Y == targetSpaceOnGrid.Y;
                    
        }
        

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * speed;
            location.Y += moveDirection.Y * speed;
            currentSpace.Location = location.ToPoint();  
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
        #endregion



        public bool LiesOnPath(Rectangle other)
        {

            for (int i = pathSpaces.IndexOf(lastSpaceOnGrid); i < pathSpaces.Count;i++)
            {
                if(pathSpaces[i].Intersects(other) || pathSpaces[i].Contains(other))
                {
                    return true;
                }
            }
            return false;
        }
        public void FreeToMove()
        {
            possibleDirections = new List<Direction> { left, right, up, down };
        }

        public void TakeDamage(int amount)
        {
            gelly.TakeDamage(amount);
        }

        #region //unused methods
        public void Attack()
        {
            //does nothing
        }
        public void MoveAwayFromCollision(Collision collision)
        {

            //using CheckIfBlockingPath for this implementation

        }
        public void Die()
        {
            //do nothing
        }
        #endregion

        public void Stun()
        {
            stunClock = stunTime;
            if (speed > 0) savedSpeed = speed;

           
        }

        public bool DoneBeingStunned()
        {
            bool currentlyCountingDown = stunClock > 0;
            if (currentlyCountingDown)
            {
                stunClock--; speed = 0;
            }
            return currentlyCountingDown && stunClock <= 0;
        }
    }
}
