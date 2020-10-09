using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Sprint2
{
   /// <summary>
   /// Author: JT Thrash
   /// </summary>
    public class WallMasterMoveState : IEnemyState
    {

        private Vector2 location;
        private Rectangle gridLocation;
        private Rectangle nextLoc;

        private const int tileColumns = 12;
        private const int tileRows = 7;
        private List<List<Rectangle>> gridTiles;
        private List<Rectangle> outsideArea;
        private List<Rectangle> movementPattern;
        
        private int currentRow;
        private int currentCol;

        private Vector2 moveDirection;
        private WallMaster master;

        private const int moveSpeed = 1;

        Random RandomNumber;

        private bool movesOnRow;
        private int nextRectangle = 0;
        
        
        public WallMasterMoveState(Vector2 location, Game game, WallMaster master)
        {
            this.location = location;
            this.master = master;

            this.master.SetSprite(EnemySpriteFactory.Instance.CreateWallMasterSprite());

            RandomNumber = new Random();

            gridTiles = GridGenerator.Instance.GetGrid(game, tileColumns, tileRows);
            gridLocation = GridGenerator.Instance.GetGridLocation(location);

            Point tileSize = GridGenerator.Instance.GetTileSize();

            currentCol = gridLocation.X / tileSize.X;
            currentRow = gridLocation.Y / tileSize.Y;

            outsideArea = new List<Rectangle>();

            movesOnRow = currentRow == 0 || currentRow == tileRows - 1;

            //create area outside of playing area where hand will lurk
            Point position;
            if(currentRow == 0)
            {
                for(int i = 0; i < tileColumns; i++)
                {
                    position = new Point(gridTiles[0][i].X, gridTiles[0][i].Y - tileSize.Y);
                    outsideArea.Add(new Rectangle(position, tileSize));
                   
                }
               
                
            }else if(currentRow == tileRows - 1)
            {
                for (int i = 0; i < tileColumns; i++)
                {
                    position = new Point(gridTiles[tileRows-1][i].X, gridTiles[tileRows-1][i].Y + tileSize.Y);
                    outsideArea.Add(new Rectangle(position, tileSize));
                    
                }
              
            }
            else if(currentCol == 0)
            {
                for (int i = 0; i < tileRows; i++)
                {
                    position = new Point(gridTiles[i][0].X - tileSize.X, gridTiles[i][0].Y);
                    outsideArea.Add(new Rectangle(position, tileSize));
                    
                }
                
            }
            else if(currentCol == tileColumns - 1)
            {
                for (int i = 0; i < tileRows; i++)
                {
                    position = new Point(gridTiles[i][tileColumns-1].X + tileSize.X, gridTiles[i][tileColumns-1].Y);
                    outsideArea.Add(new Rectangle(position, tileSize));
                    
                }

            }
            else // use upper wall by default
            {
                movesOnRow = true;
                currentRow = 0;
                gridLocation.Y = 0;             
                for (int i = 0; i < tileColumns; i++)
                {
                    position = new Point(gridTiles[0][i].X, gridTiles[0][i].Y - tileSize.Y);
                    outsideArea.Add(new Rectangle(position, tileSize));
                    
                }
               
            }
            

            movementPattern = new List<Rectangle>();
            if (movesOnRow)
            {
                movementPattern.Add(gridLocation);
                gridLocation = outsideArea[currentCol];
                movementPattern.Add(gridLocation);

                int rand = RandomNumber.Next(0, tileColumns);

                movementPattern.Add(outsideArea[rand]);
                movementPattern.Add(gridTiles[currentRow][rand]);
            }
            else
            {
                movementPattern.Add(gridLocation);
                gridLocation = outsideArea[currentRow];
                movementPattern.Add(gridLocation);

                int rand = RandomNumber.Next(0, tileRows);

                movementPattern.Add(outsideArea[rand]);
                movementPattern.Add(gridTiles[rand][currentCol]);
            }
            
            this.location = gridLocation.Location.ToVector2();
            this.master.UpdateLocation(this.location);
            nextRectangle = 2;
        }

        public void TakeDamage()
        {
            //take damage
        }

        public void ChangeDirection()
        {
            if (nextRectangle >= movementPattern.Count)
                nextRectangle = 0;

            nextLoc = movementPattern[nextRectangle];
            nextRectangle++;

            moveDirection.Y = CompareTwoNums(nextLoc.Y, gridLocation.Y);
            moveDirection.X = CompareTwoNums(nextLoc.X, gridLocation.X);

        }

        public float CompareTwoNums(float num1, float num2)
        {
            if (num1 < num2) return -1;
            if (num1 > num2) return 1;
            return 0;
        }

        public void Die()
        {
            //die
        }

        public void Attack()
        {
           //do nothing
        }

        public void Update()
        {
            bool arrivedAtLocation = gridLocation.X == nextLoc.X && gridLocation.Y == nextLoc.Y;
            if (arrivedAtLocation || moveDirection == default) { ChangeDirection(); } else { MoveOneUnit(); }
        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            gridLocation.Location = location.ToPoint();
            master.UpdateLocation(location);
        }
    }
}
