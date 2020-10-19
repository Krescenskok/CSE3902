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
        private Rectangle gridLocation;
        private Rectangle nextLoc;
        
        
        private const int tileColumns = 12;
        private const int tileRows = 7;
        private List<List<Rectangle>> gridTiles;
        private int currentRow;
        private int currentCol;


        
        private Vector2 moveDirection;
        private Gel gelly;

        private bool stoppedMoving;
        private int coolDownClock;
        private readonly int[] coolDownTimes = { 10, 25,  50 };
        private int coolDownTime;

        private const int moveSpeed = 2;

        Random RandomNumber;

        public GelMoveState(Gel gel, Vector2 location, Game game)
        {
            this.location = location;
            gelly = gel;

            gelly.SetSprite(EnemySpriteFactory.Instance.CreateGelMoveSprite());
            

            gridTiles = GridGenerator.Instance.GetGrid(game, tileColumns, tileRows);
            gridLocation = GridGenerator.Instance.GetGridLocation(location);

            this.location = gridLocation.Location.ToVector2();
            gelly.UpdateLocation(this.location);

            Point tileSize = GridGenerator.Instance.GetTileSize();

            currentCol = gridLocation.X / tileSize.X;
            currentRow = gridLocation.Y / tileSize.Y;

            stoppedMoving = true;

            RandomNumber = new Random();

            coolDownClock = coolDownTimes[RandomNumber.Next(0, coolDownTimes.Length)];
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

            int nextRow = moveWithinColumn ? RandomNumber.Next(0, tileRows) : currentRow;
            int nextCol = moveWithinRow ? RandomNumber.Next(0, tileColumns) : currentCol;

            nextLoc = gridTiles[nextRow][nextCol];

            moveDirection.Y = CompareTwoNums(nextRow, currentRow);
            moveDirection.X = CompareTwoNums(nextCol, currentCol);

            
        }

        public void Update()
        {
            
            if (stoppedMoving && DoneWithCoolDown())
            {
                ResetCoolDownClock();
                ChangeDirection();
                stoppedMoving = false;
            }
            else if(gridLocation.X == nextLoc.X && gridLocation.Y == nextLoc.Y)
            {
                stoppedMoving = true;
                currentCol = gridLocation.X / gridLocation.Width;
                currentRow = gridLocation.Y / gridLocation.Height;
                coolDownClock = Math.Max(0, coolDownClock - 1);
            }
            else
            {
                MoveOneUnit();                
            }

        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            gridLocation.Location = location.ToPoint();  
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

        public float CompareTwoNums(float num1, float num2)
        {            
            if (num1 < num2) return - 1;
            if (num1 > num2) return 1;
            return 0;
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
            throw new NotImplementedException();
        }

        public void TakeDamage(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
