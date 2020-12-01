using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class WallMasterMoveState : IEnemyState
    {
        private Game game;
        private Vector2 location;
        private Rectangle currentSpace;

        private const int tileColumns = 12;
        private const int tileRows = 7;
        private List<List<Rectangle>> gridTiles;
        private List<Rectangle> outsideArea;

        private List<Rectangle> insideArea;

        private List<Rectangle> leftWall, rightWall, topWall, bottomWall;
        private List<Rectangle> leftInside, rightInside, topInside, bottomInside;

        private Dictionary<Rectangle, Rectangle> insideSpaces;
        private Dictionary<Rectangle, Rectangle> wallSpaces;

        private Rectangle targetSpace;

        private WallMaster master;


        private const int normalMovingSpeed = 1;
        private int moveSpeed = normalMovingSpeed;

        private const int stunTime = 120;
        private int stunClock = 0;
        private bool permaStun = false;

        Random RandomNumber;


        private enum Wall {left,right,top,bottom };
        Wall left = Wall.left, right = Wall.right, top = Wall.top, bottom = Wall.bottom;
        Wall currentWall;

        private Dictionary<Wall, List<Rectangle>> wallMap;
        private Dictionary<Wall, List<Rectangle>> insideMap;

        private bool inWall = true;
        private bool colliderOn;

        private Vector2 playerPosition;
        private Rectangle playerSpace;

        private bool attacking = false;

        public Vector2 Location { get => location; }
        
        public WallMasterMoveState(Vector2 location, Game game, WallMaster master)
        {
            this.location = location;
            this.master = master;
            this.game = game;

            ISprite sprite = EnemySpriteFactory.Instance.CreateWallMasterSprite("");
            WallMasterSprite wSprite = (WallMasterSprite)sprite;
            

            Rectangle spriteSize = wSprite.GetRectangle();

            RandomNumber = new Random();

            gridTiles = GridGenerator.Instance.GetGrid();
            Point tileSize = GridGenerator.Instance.GetTileSize();

            outsideArea = new List<Rectangle>();
            insideArea = new List<Rectangle>();
            leftWall = new List<Rectangle>();
            rightWall = new List<Rectangle>();
            topWall = new List<Rectangle>();
            bottomWall = new List<Rectangle>();
            topInside = new List<Rectangle>();
            bottomInside = new List<Rectangle>();
            rightInside = new List<Rectangle>();
            leftInside = new List<Rectangle>();
            insideSpaces = new Dictionary<Rectangle, Rectangle>();
            wallSpaces = new Dictionary<Rectangle, Rectangle>();

            wallMap = new Dictionary<Wall, List<Rectangle>> { { left, leftWall }, { right, rightWall },{ top, topWall },{ bottom, bottomWall } };
            insideMap = new Dictionary<Wall, List<Rectangle>> { { left, leftInside }, { right, rightInside }, { top, topInside }, { bottom, bottomInside } };

            //create area outside of playing area where hand will lurk
            Point position;
            Rectangle wallRect;
            Rectangle insideRect;

            //top wall
            for (int i = 0; i < tileColumns; i++)
            {
                position = new Point(gridTiles[0][i].X, gridTiles[0][i].Y - tileSize.Y * 2);

                
                wallRect = new Rectangle(position, tileSize);
                insideRect = gridTiles[0][i];
                TopOffset(spriteSize, ref wallRect, ref insideRect);

                outsideArea.Add(wallRect); topWall.Add(wallRect);
                insideArea.Add(insideRect); topInside.Add(insideRect);

            }
            //right wall
            for (int i = 0; i < tileRows; i++)
            {
                position = new Point(gridTiles[i][tileColumns - 1].X + tileSize.X * 2, gridTiles[i][tileColumns - 1].Y);
                wallRect = new Rectangle(position, tileSize);
                insideRect = gridTiles[i][tileColumns - 1];
                RightOffset(spriteSize, ref wallRect, ref insideRect);

                outsideArea.Add(wallRect); rightWall.Add(wallRect);
                insideArea.Add(insideRect); rightInside.Add(insideRect);
            }
            //bottom wall
            for (int i = 0; i < tileColumns; i++)
            {
                position = new Point(gridTiles[tileRows - 1][i].X, gridTiles[tileRows - 1][i].Y + tileSize.Y * 2);
                wallRect = new Rectangle(position, tileSize);
                
                insideRect = gridTiles[tileRows - 1][i];

                BottomOffset(spriteSize, ref wallRect, ref insideRect);
                

                outsideArea.Add(wallRect); bottomWall.Add(wallRect);
                insideArea.Add(insideRect); bottomInside.Add(insideRect);
            }
            //left wall
            for (int i = 0; i < tileRows; i++)
            {
                position = new Point(gridTiles[i][0].X - tileSize.X * 2, gridTiles[i][0].Y);
                wallRect = new Rectangle(position, tileSize);
                insideRect = gridTiles[i][0];
                LeftOffset(spriteSize, ref wallRect, ref insideRect);

                outsideArea.Add(wallRect); leftWall.Add(wallRect);
                insideArea.Add(insideRect); leftInside.Add(insideRect);
            }
            
           
            for(int i = 0; i < outsideArea.Count; i++)
            {
                insideSpaces.Add(outsideArea[i], insideArea[i]);
                if(!wallSpaces.ContainsKey(insideArea[i])) wallSpaces.Add(insideArea[i], outsideArea[i]);
            }


            int randomSpace = RandomNumber.Next(0, outsideArea.Count);
            currentSpace = outsideArea[randomSpace];

            if (leftWall.Contains(currentSpace)) currentWall = left;
            else if (rightWall.Contains(currentSpace)) currentWall = right;
            else if (topWall.Contains(currentSpace)) currentWall = top;
            else currentWall = bottom;

            this.master.SetSprite(EnemySpriteFactory.Instance.CreateWallMasterSprite(currentWall.ToString()));

            randomSpace = RandomNumber.Next(0, wallMap[currentWall].Count);
            targetSpace = wallMap[currentWall][randomSpace];

            

            this.location = currentSpace.Location.ToVector2();
            this.master.UpdateLocation(this.location);
            
        }

        public void TakeDamage()
        {
            //take damage
        }

        public void ChangeDirection()
        {
            int randomSpace = RandomNumber.Next(0, wallMap[currentWall].Count);
            targetSpace = wallMap[currentWall][randomSpace];

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
            if (!inWall)
            {
                targetSpace = NearestSpace(location, outsideArea);
                master.state = new WallMasterGrabbingLinkState(location, master, targetSpace, currentWall.ToString(), game);
            }
            
        }

        public void Update()
        {
            if (inWall && colliderOn) ToggleCollider(false);
          

            if (Arrived(currentSpace,targetSpace)) {

                inWall = !insideArea.Contains(targetSpace);
                if (inWall) ChangeDirection();
                if (!inWall && playerSpace == targetSpace) { Hide(); attacking = false; }

                ToggleCollider(!inWall);

            } else {
                
                MoveOneUnit(); 
            }

            if (stunClock > 0) stunClock--;
            else if (stunClock <= 0 && moveSpeed == 0) moveSpeed = normalMovingSpeed;

            if (attacking) MoveToPlayer();
        }

        private void ToggleCollider(bool on) {
            if (on)
                CollisionHandler.Instance.AddCollider(master.Colliders[0], Layers.Enemy);
            else
                CollisionHandler.Instance.RemoveCollider(master.Colliders[0]);

            colliderOn = on;
        }

        public void Hide()
        {
            Rectangle current = NearestSpace(currentSpace.Location.ToVector2(), insideArea);
            targetSpace = wallSpaces[current];
        }

        public void MoveOneUnit()
        {
            location = MoveToward(location, targetSpace.Location.ToVector2());
            currentSpace.Location = location.ToPoint();
            master.UpdateLocation(location);
            
        }


        public Vector2 MoveToward(Vector2 position, Vector2 target)
        {
            Vector2 direction = target - position;
            direction.Normalize();
            position = Vector2.Add(position, direction * moveSpeed);

            return position;
        }

        private bool Arrived(Rectangle position, Rectangle target)
        {
            float dist = Vector2.Distance(position.Center.ToVector2(), target.Center.ToVector2());
            
            
            return dist <= 1;
        }

        public void MoveToPlayer()
        {
            
            Rectangle current = NearestSpace(currentSpace.Location.ToVector2(), wallMap[currentWall]);
         
            if (inWall) targetSpace = insideSpaces[current];
            else targetSpace = playerSpace;

        }

        public void LockOnToPlayerPosition(Collision col)
        {
            if(inWall && !attacking)
            {
                playerPosition = col.Location;
                List<Rectangle> insideWall = insideMap[currentWall];

                Rectangle playerPos = NearestSpace(playerPosition, insideWall);
                playerSpace = NearestSpace(playerPosition, insideWall);

                attacking = true;
            }
            
            
        }
        public void MoveAwayFromCollision(Collision collision)
        {
            //does not move from collisions
        }

        private Rectangle NearestSpace(Vector2 target, List<Rectangle> spaces)
        {
            Rectangle nearest = new Rectangle();
            float minDist = int.MaxValue;

            
            foreach(Rectangle rect in spaces)
            {
                float dist = Vector2.Distance(target, rect.Location.ToVector2());
                if(dist < minDist)
                {
                    nearest = rect;
                    minDist = dist;
                }
            }

            return nearest;
        }

        public Rectangle CombineRectangles(List<Rectangle> rectList)
        {
            Rectangle bigRect = rectList[0];

            for(int i = 0; i < rectList.Count;i++)
            {
                bigRect = Rectangle.Union(bigRect, rectList[i]);
            }

            return bigRect;
        }

        public Rectangle TrackingArea()
        {
            if (currentWall == right) return CombineRectangles(rightInside);
            else if (currentWall == left) return CombineRectangles(leftInside);
            else if (currentWall == top) return CombineRectangles(topInside);
            else return CombineRectangles(bottomInside);
        }

        public Rectangle OutsideArea()
        {
            if (currentWall == right) return CombineRectangles(rightWall);
            else if (currentWall == left) return CombineRectangles(leftWall);
            else if (currentWall == top) return CombineRectangles(topWall);
            else return CombineRectangles(bottomWall);
        }


        public void LeftOffset(Rectangle rect, ref Rectangle wall, ref Rectangle inside)
        {
            rect.Location = wall.Location;
            int xOffset = wall.Right - rect.Right;
            Vector2 newLoc = Vector2.Add(wall.Location.ToVector2(), new Vector2(xOffset, 0));
            wall.Location = newLoc.ToPoint();

            rect.Location = inside.Location;
            newLoc = Vector2.Subtract(inside.Location.ToVector2(), new Vector2(xOffset, 0) /  2);
            inside.Location = newLoc.ToPoint();
        }

        public void RightOffset(Rectangle rect, ref Rectangle wall, ref Rectangle inside)
        {
            rect.Location = wall.Location;
            int xOffset = rect.Right - wall.Right;
            Vector2 newLoc = Vector2.Add(wall.Location.ToVector2(), new Vector2(xOffset, 0));
            wall.Location = newLoc.ToPoint();

            rect.Location = inside.Location;
            newLoc = Vector2.Subtract(inside.Location.ToVector2(), new Vector2(xOffset, 0));
            inside.Location = newLoc.ToPoint();
        }
        public void TopOffset(Rectangle rect,ref  Rectangle wall, ref Rectangle inside)
        {
            rect.Location = wall.Location;
            int yOffset = wall.Bottom - rect.Bottom;
            Vector2 newLoc = Vector2.Add(wall.Location.ToVector2(), new Vector2(0, yOffset));
            wall.Location = newLoc.ToPoint();

            rect.Location = inside.Location;
            newLoc = Vector2.Subtract(inside.Location.ToVector2(), new Vector2(0, yOffset) );
            inside.Location = newLoc.ToPoint();
        }
        public void BottomOffset(Rectangle rect, ref Rectangle wall, ref Rectangle inside)
        {
            rect.Location = wall.Location;
            int yOffset = rect.Bottom - wall.Bottom;
            Vector2 newLoc = Vector2.Add(wall.Location.ToVector2(), new Vector2(0, yOffset));
            wall.Location = newLoc.ToPoint();

            rect.Location = inside.Location;
            newLoc = Vector2.Subtract(inside.Location.ToVector2(), new Vector2(0, yOffset));
            inside.Location = newLoc.ToPoint();
        }

        public void TakeDamage(int amount)
        {
            
        }

        public void Stun(bool permanent)
        {
            moveSpeed = 0;
            stunClock = permanent || permaStun ? int.MaxValue : stunTime;
            permaStun = permaStun ? true : permaStun;
        }
    }
}
