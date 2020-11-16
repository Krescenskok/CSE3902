using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace Sprint4
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

        private Direction currentDirection;

        private LinkPlayer player;

        public void Load(Game game)
        {
            this.game = game;
            screenHeight = game.Window.ClientBounds.Height;
            screenWidth = game.Window.ClientBounds.Width;

            

            transform = Matrix.Identity;

            
            

            Point topLeftCorner = new Point(screenWidth / 10, screenHeight / 4);
            Point size = new Point(screenWidth - topLeftCorner.X * 2, screenHeight - topLeftCorner.Y);




            playArea = new Rectangle(topLeftCorner, size);
            gameView = new Viewport(playArea);

            transform.Translation = new Vector3(-size.X * 2, -size.Y * 5, 0);

            Target = transform.Translation;
            location = new Vector2(transform.M41, transform.M42);
        }

        private Camera()
        {
        }

        public void OpenMenu()
        {
            //code for extending menu view
        }

        public void ScrollUp(int roomNum)
        {
            Target = Transform.Translation + Vector3.Up * playArea.Height;
           
            direction = Vector3.Up;

            nextRoom = roomNum;

            Game1 game1 = game as Game1;
            game1.isPaused = true;

            currentDirection = Direction.up;
        }

        public void ScrollDown(int roomNum)
        {
            Target = Transform.Translation + Vector3.Down* playArea.Height;

            direction = Vector3.Down;

            nextRoom = roomNum;

            Game1 game1 = game as Game1;
            game1.isPaused = true;

            currentDirection = Direction.down;
        }

        public void ScrollRight(int roomNum)
        {
            Target = Transform.Translation + Vector3.Left * playArea.Width;

            direction = Vector3.Left;
            nextRoom = roomNum;

            Game1 game1 = game as Game1;
            game1.isPaused = true;

            currentDirection = Direction.right;
        }

        public void ScrollLeft(int roomNum)
        {
            Target = Transform.Translation + Vector3.Right * playArea.Width;

            direction = Vector3.Right;
            nextRoom = roomNum;

            Game1 game1 = game as Game1;
            game1.isPaused = true;

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

        public void Update()
        {
            //!transform.Translation.Equals(Target)
            if (!DoneScrolling())
            {
                Vector3 newPos = Transform.Translation + direction  * 10;
                Matrix.CreateTranslation(ref newPos, out transform);

                

                loadNextRoom = true;
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
