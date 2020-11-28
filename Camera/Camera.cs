using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class Camera 
    {
        public static Camera Instance { get; } = new Camera();

        private Matrix transform;
        public  Matrix Transform { get => transform; }

        private Vector3 Target;
        private Vector3 direction = Vector3.Up;

        private int screenWidth;
        private int screenHeight;

        private int nextRoom;
        
        private bool loadNextRoom = false;

        private Game game;

        public  Vector2 Location { get => location; }
        private Vector2 location;

        public Viewport gameView;
        public Rectangle playArea;

        public Viewport HUDView;
        public Rectangle HUDArea;

        private Direction currentDirection;

        private Rectangle targetGameView;
        private Rectangle targetHUDLocation;

        private bool inventoryOpen = false, inventoryStillMoving = false;

        private const int scrollSpeed = 10;

        private bool linkHasEntered = false;
        private Vector2 linkEnterPosition;
        private const int linkEnterSpeed = -2;

        private LinkPlayer player;

       

        public void Load(Game game)
        {
            this.game = game;
            screenHeight = game.Window.ClientBounds.Height;
            screenWidth = game.Window.ClientBounds.Width;
            player = (game as Game1).LinkPlayer;
            

            transform = Matrix.Identity;

            Point topLeftCorner = new Point(screenWidth / 10, screenHeight / 4);
            Point size = new Point(screenWidth - topLeftCorner.X * 2, screenHeight - topLeftCorner.Y);

            Point HUDPoint = new Point(topLeftCorner.X, topLeftCorner.Y - screenHeight);
            Point HUDSize = new Point(size.X , screenHeight);
           


            playArea = new Rectangle(topLeftCorner, size);
            gameView = new Viewport(playArea);

            HUDArea = new Rectangle(HUDPoint, HUDSize);
            HUDView = new Viewport(HUDArea);

            targetGameView = playArea;
            targetHUDLocation = HUDArea;

            transform.Translation = new Vector3(-size.X * 2, -size.Y * 5, 0);

            Target = transform.Translation;
            location = new Vector2(transform.M41, transform.M42);
        }

        private Camera()
        {
        }

        //code for extending menu view
        public void OpenCloseInventory()
        {
            if (HUDOpenCloseFinished())
            {
                if (!inventoryOpen)
                {
                    Point newHUDLocation = new Point(HUDArea.Location.X, HUDArea.Location.Y + screenHeight);
                    targetHUDLocation.Location = newHUDLocation;

                    Point newGameViewLocation = new Point(playArea.Location.X, playArea.Location.Y + playArea.Size.Y);
                    targetGameView.Location = newGameViewLocation;

                    inventoryOpen = true;
                }
                else
                {
                    Point newHUDLocation = new Point(HUDArea.Location.X, HUDArea.Location.Y - screenHeight);
                    targetHUDLocation.Location = newHUDLocation;

                    Point newGameViewLocation = new Point(playArea.Location.X, playArea.Location.Y - playArea.Size.Y);
                    targetGameView.Location = newGameViewLocation;

                    inventoryOpen = false;
                }

              
            }
           
            
        }

        public void MoveToRoom(int roomNum)
        {
            Target = RoomCoordinate.position(roomNum, playArea.Width,playArea.Height);
            transform.Translation = Target;
            location.X = transform.M41;
            location.Y = transform.M42;
            player.currentLocation = new Vector2(game.Window.ClientBounds.X / 2, game.Window.ClientBounds.Y / 2) - location;
            RoomSpawner.Instance.RoomChange(game, roomNum);
           
        }



        public void ScrollUp(int roomNum)
        {
            Target = Transform.Translation + Vector3.Up * playArea.Height;
           
            direction = Vector3.Up;

            nextRoom = roomNum;

            
            (game as Game1).isPaused = true;
            

            currentDirection = Direction.up;
        }

        public void ScrollDown(int roomNum)
        {
            Target = Transform.Translation + Vector3.Down* playArea.Height;

            direction = Vector3.Down;

            nextRoom = roomNum;

            (game as Game1).isPaused = true;

            currentDirection = Direction.down;
        }

        public void ScrollRight(int roomNum)
        {
            Target = Transform.Translation + Vector3.Left * playArea.Width;

            direction = Vector3.Left;
            nextRoom = roomNum;
            (game as Game1).isPaused = true;

            currentDirection = Direction.right;
        }

        public void ScrollLeft(int roomNum)
        {
            Target = Transform.Translation + Vector3.Right * playArea.Width;

            direction = Vector3.Right;
            nextRoom = roomNum;
            (game as Game1).isPaused = true;

            currentDirection = Direction.left;
        }

        private bool DoneScrolling()
        {
            return currentDirection == Direction.down && transform.Translation.Y < Target.Y
                || currentDirection == Direction.up && transform.Translation.Y > Target.Y
                || currentDirection == Direction.left && transform.Translation.X >Target.X
                || currentDirection == Direction.right && transform.Translation.X < Target.X
                || transform.Translation.Equals(Target);
        }

        private bool HUDOpenCloseFinished()
        {
            return playArea.Size.Equals(targetGameView.Size) && playArea.Location.Equals(targetGameView.Location)
                && HUDArea.Size.Equals(targetHUDLocation.Size) && HUDArea.Location.Equals(HUDArea.Location);
        }

        private void MoveViewports()
        {
            Vector2 currentLoc = playArea.Location.ToVector2();
            Vector2 moveAmount = targetGameView.Location.ToVector2() - currentLoc;
            moveAmount = Vector2.Normalize(moveAmount) * scrollSpeed;
            currentLoc = Vector2.Add(currentLoc, moveAmount);
            playArea.Location = currentLoc.ToPoint();
            gameView.Bounds = playArea;

            

            currentLoc = HUDArea.Location.ToVector2();
            moveAmount = targetHUDLocation.Location.ToVector2() - currentLoc;
            moveAmount = Vector2.Normalize(moveAmount) * scrollSpeed;
            currentLoc = Vector2.Add(currentLoc, moveAmount);
            HUDArea.Location = currentLoc.ToPoint();
            HUDView.Bounds = HUDArea;
        }

        

        public void Update()
        {

            if (!HUDOpenCloseFinished())
            {
                MoveViewports();
                inventoryStillMoving = true;
                
            }else if(inventoryOpen && !(game as Game1).isPaused)
            {
                (game as Game1).isPaused = true;
            }else if(!inventoryOpen && (game as Game1).isPaused && inventoryStillMoving)
            {
                (game as Game1).isPaused = false;
                inventoryStillMoving = false;
            }

           

            if (!DoneScrolling())
            {
                Vector3 newPos = Transform.Translation + direction  * scrollSpeed;
                Matrix.CreateTranslation(ref newPos, out transform);

                int yOffset = direction.Y > 0 ? GridGenerator.Instance.offsetYBottom : GridGenerator.Instance.Offset.Y;
                int xOffset = GridGenerator.Instance.Offset.X;
              

                loadNextRoom = true;
                linkHasEntered = false;
                linkEnterPosition = player.currentLocation + new Vector2(direction.X * -xOffset, direction.Y * -yOffset);

            }else if (loadNextRoom && !linkHasEntered)
            {

                Vector2 direction2D = new Vector2(direction.X, direction.Y) * linkEnterSpeed;
                player.currentLocation += direction2D;

                linkHasEntered = Vector2.Distance(player.currentLocation,linkEnterPosition) <= 1;
                

            }else if (loadNextRoom)
            {
                Game1 game1 = game as Game1;
                game1.isPaused = false;
                
                RoomSpawner.Instance.RoomChange(game, nextRoom);
                loadNextRoom = false;
            }

            location.X = transform.M41;
            location.Y = transform.M42;




        }


    }
}
