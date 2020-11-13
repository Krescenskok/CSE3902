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
        private Vector3 Direction = Vector3.Up;

        private int screenWidth;
        private int screenHeight;

        private int nextRoom;
        private bool loadNextRoom = false;

        private Game game;

        public  Vector2 Location { get => location; }
        private Vector2 location;

        public void Load(Game game)
        {
            this.game = game;
            screenHeight = game.Window.ClientBounds.Height;
            screenWidth = game.Window.ClientBounds.Width;


            transform = Matrix.Identity;
            transform.Translation = new Vector3(-screenWidth * 2, -screenHeight * 5, 0);
            transform.Translation = Vector3.Zero;
            Target = transform.Translation;
            location = new Vector2(transform.M41, transform.M42);
        }

        private Camera()
        {
        }

        public void ScrollUp(int roomNum)
        {
            Target = Transform.Translation + Vector3.Up * screenHeight;
           
            Direction = Vector3.Up;

            nextRoom = roomNum;
        }

        public void ScrollDown(int roomNum)
        {
            Target = Transform.Translation + Vector3.Down* screenHeight;

            Direction = Vector3.Down;

            nextRoom = roomNum;
        }

        public void ScrollLeft(int roomNum)
        {
            Target = Transform.Translation + Vector3.Left * screenWidth;

            Direction = Vector3.Left;
            nextRoom = roomNum;
        }

        public void ScrollRight(int roomNum)
        {
            Target = Transform.Translation + Vector3.Right * screenWidth;

            Direction = Vector3.Right;
            nextRoom = roomNum;
        }

        public void Update()
        {

            if (!transform.Translation.Equals(Target))
            {
                Vector3 newPos = Transform.Translation + Direction  * 10;
                Matrix.CreateTranslation(ref newPos, out transform);

                loadNextRoom = true;
            }else if (loadNextRoom)
            {
               
                
                RoomSpawner.Instance.RoomChange(game, nextRoom);
                loadNextRoom = false;
            }

            location.X = transform.M41;
            location.Y = transform.M42;




        }


    }
}
