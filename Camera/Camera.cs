using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Diagnostics;

namespace Sprint5
{
    public class Camera
    {
        public static Camera Instance { get; } = new Camera();

        private BlackScreenSprite screenFade;
        private Color fadeColor = Color.Transparent;
        private bool fadeFlag = false;
        private static int fader = 5;

        private Matrix transform;
        public Matrix Transform { get => transform; }

        private Vector3 Target;
        private Vector3 direction = Vector3.Up;

        private int screenWidth;
        private int screenHeight;

        private int nextRoom;

        private int currentRoom;

        private bool loadNextRoom = false;
        private bool wallmasterSetBack = false;

        private Game1 game;

        public Vector2 Location { get => location; }
        private Vector2 location;

        public Viewport gameView;
        public Rectangle playArea;

        public Viewport EntireView { get; set; }
        public Rectangle EntireArea { get; set; }

        public Viewport HUDView;
        public Rectangle HUDArea;

        private Direction currentDirection;

        private Rectangle targetgameView;
        private Rectangle targetHUDLocation;

        private bool inventoryOpen = false, inventoryStillMoving = false;

        private const int scrollSpeed = 10;

        private bool linkHasEntered = false;
        private Vector2 linkEnterPosition;
        private const int linkEnterSpeed = -2;

        private LinkPlayer player;

        private bool IsInventory;



        public void Load(Game game)
        {
            this.game = game as Game1;
            screenHeight = game.Window.ClientBounds.Height;
            screenWidth = game.Window.ClientBounds.Width;
            player = this.game.LinkPlayer;


            transform = Matrix.Identity;

            Point topLeftCorner = new Point(screenWidth / 10, screenHeight / 4);
            Point size = new Point(screenWidth - topLeftCorner.X * 2, screenHeight - topLeftCorner.Y);

            Point HUDPoint = new Point(topLeftCorner.X, topLeftCorner.Y - screenHeight);
            Point HUDSize = new Point(size.X, screenHeight);

            Point EntirePoint = new Point(0, 0);
            Point EntireSize = new Point(screenWidth, screenHeight);



            playArea = new Rectangle(topLeftCorner, size);
            gameView = new Viewport(playArea);

            HUDArea = new Rectangle(HUDPoint, HUDSize);
            HUDView = new Viewport(HUDArea);

            EntireArea = new Rectangle(EntirePoint, EntireSize);
            EntireView = new Viewport(EntireArea);

            targetgameView = playArea;
            targetHUDLocation = HUDArea;

            transform.Translation = new Vector3(-size.X * 2, -size.Y * 5, 0);

            Target = transform.Translation;
            location = new Vector2(transform.M41, transform.M42);

            currentRoom = 1;

            screenFade = SpriteFactory.Instance.CreateBlackScreen();
        }

        private Camera()
        {
        }


        //changes target locations for HUD and game viewports. Update will move the actual location
        public void OpenCloseInventory()
        {

            if (HUDOpenCloseFinished())
            {
               

                int moveDirection = inventoryOpen ? -1 : 1;

                Point newHUDLocation = new Point(HUDArea.Location.X, HUDArea.Location.Y + screenHeight * moveDirection);
                targetHUDLocation.Location = newHUDLocation;

                Point newgameViewLocation = new Point(playArea.Location.X, playArea.Location.Y + playArea.Size.Y * moveDirection);
                targetgameView.Location = newgameViewLocation;

                inventoryOpen = moveDirection == 1;
                if (inventoryOpen) 
                {
                    IsInventory = game.State.Id == StateId.Pause; 
                    StartInventory();
                } 
            }


        }

        public void MoveToRoom(int roomNum)
        {
            Target = RoomCoordinate.position(roomNum, playArea.Width, playArea.Height);
            transform.Translation = Target;
            location.X = transform.M41;
            location.Y = transform.M42;
            player.currentLocation = new Vector2(game.Window.ClientBounds.X / 2, game.Window.ClientBounds.Y / 2) - location;
            RoomSpawner.Instance.RoomChange(game, roomNum);
            if (nextRoom == 18) { player.EnterSecretRoom(); }
            else if (currentRoom == 18 && nextRoom == 17) player.LeaveSecretRoom();

            currentRoom = roomNum;
        }

        public void BackToSquareOne()
        {
            MoveToRoom(1);
            player.currentLocation = RoomDoors.Instance.LinkStartLocation;
            player.LocationInitialized = true;
            player.state = new MoveUp(player, player.sprite);
            direction = Vector3.Up;
            wallmasterSetBack = true;
            nextRoom = 1;
            RoomDoors.Instance.OpenDoor(6);
        }

        public void BlackScreenTransition()
        {
            fadeColor.A = (byte)((fadeColor.A + fader) % 255);
            fadeFlag = fadeColor.A != 0;
            if (fadeFlag) fader = fadeColor.A == 254 ? -fader : fader;
            else { MoveToRoom(nextRoom); EndTransition(); }
        }



        public void Transition(int room) { BlackScreenTransition(); nextRoom = room; StartTransition(); }


        public void Scroll(int roomNum, string dir)
        {
            Vector3 scrollDir = Directions.ParseVector(dir);
            int scrollLength = scrollDir.X == 0 ? playArea.Height : playArea.Width;
            Target = Transform.Translation + scrollDir * scrollLength;

            direction = scrollDir;

            nextRoom = roomNum;

            StartTransition();



            currentDirection = Directions.Parse(dir);
        }



        private bool DoneScrolling()
        {
            return currentDirection == Direction.down && transform.Translation.Y < Target.Y
                || currentDirection == Direction.up && transform.Translation.Y > Target.Y
                || currentDirection == Direction.left && transform.Translation.X > Target.X
                || currentDirection == Direction.right && transform.Translation.X < Target.X
                || transform.Translation.Equals(Target);
        }

        private bool HUDOpenCloseFinished()
        {
            return playArea.Size.Equals(targetgameView.Size) && playArea.Location.Equals(targetgameView.Location)
                && HUDArea.Size.Equals(targetHUDLocation.Size) && HUDArea.Location.Equals(HUDArea.Location);
        }

        private void MoveViewports(ref Rectangle area, Rectangle targetArea, ref Viewport newPort)
        {
            Vector2 currentLoc = area.Location.ToVector2();
            Vector2 moveAmount = targetArea.Location.ToVector2() - currentLoc;
            moveAmount = Vector2.Normalize(moveAmount) * scrollSpeed;
            currentLoc = Vector2.Add(currentLoc, moveAmount);
            area.Location = currentLoc.ToPoint();
            newPort.Bounds = area;
        }

        private void GetLinkEnterPosition(Vector3 dir)
        {
            int yOffset = dir.Y > 0 ? GridGenerator.Instance.offsetYBottom : GridGenerator.Instance.Offset.Y;
            int xOffset = GridGenerator.Instance.Offset.X;
            linkEnterPosition = player.currentLocation + new Vector2(direction.X * -xOffset, direction.Y * -yOffset);
        }

        private void Move()
        {
            Vector3 newPos = Transform.Translation + direction * scrollSpeed;
            Matrix.CreateTranslation(ref newPos, out transform);
        }


        public void Update()
        {

            if (!HUDOpenCloseFinished())
            {
                MoveViewports(ref playArea, targetgameView, ref gameView);
                MoveViewports(ref HUDArea, targetHUDLocation, ref HUDView);
                inventoryStillMoving = true;

            }

            else if (!inventoryOpen && game.State.Id == StateId.Inventory && inventoryStillMoving)
            {
                EndInventory();

                inventoryStillMoving = false;
            }


                bool doneScrolling = DoneScrolling();

                if (wallmasterSetBack || !doneScrolling)
                {
                    GetLinkEnterPosition(direction);
                    loadNextRoom = true;
                    linkHasEntered = false;
                    wallmasterSetBack = false;
                }

                if (!doneScrolling)
                {
                    Move();

                } else if (loadNextRoom && !linkHasEntered)
                {

                    Vector2 direction2D = new Vector2(direction.X, direction.Y) * linkEnterSpeed;
                    player.currentLocation += direction2D;

                    linkHasEntered = Vector2.Distance(player.currentLocation, linkEnterPosition) <= 1;


                } else if (loadNextRoom)
                {

                    EndTransition();
                    RoomSpawner.Instance.RoomChange(game, nextRoom);

                    currentRoom = nextRoom;
                    loadNextRoom = false;
                }

                location.X = transform.M41;
                location.Y = transform.M42;


                if (fadeFlag)
                {
                    BlackScreenTransition();
                }

            }

            public void Draw(SpriteBatch batch)
            {

                screenFade.Draw(batch, Vector2.Zero, 0, fadeColor);
            }

            private void StartInventory() { game.State.Id = StateId.Inventory; IsInventory = true; }
            private void StartTransition() { game.State.Id = StateId.Transition; }
            private void EndInventory() { game.State.Id = StateId.Gameplay; IsInventory = false; }
            private void EndTransition() { game.State.Id = StateId.Gameplay; }
        


}
    }

