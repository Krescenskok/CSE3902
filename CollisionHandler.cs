using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sprint2Final
{
    //class which detects all collisions in game and sends appropriate messages to colliding objects
    public class CollisionHandler
    {
        public static CollisionHandler Instance { get; } = new CollisionHandler();

        private CollisionHandler()
        {

        }


        private List<ICollider> colliders = new List<ICollider>();

        public void AddCollider(ICollider newCol)
        {
            colliders.Add(newCol);
        }

        public void Update()
        {

            //iterate through each pair of colliders, determine if they collide, handle collision
            for(int i = 0; i < colliders.Count; i++)
            {
                for(int j = 0; j < colliders.Count; j++)
                {
                   if(!colliders[i].Equals(colliders[j]) && ColliderOverlap(colliders[i],colliders[j]))
                    {
                        Collision collision = CalculateSide(colliders[i],colliders[j]);
                        colliders[i].HandleCollision(colliders[j], collision);
                       
                    }
                }
            }
        }

        private bool ColliderOverlap(ICollider col1, ICollider col2)
        {
            return col1.Bounds().Intersects(col2.Bounds());
        }


        private Collision CalculateSide(ICollider col1, ICollider col2)
        {
            Rectangle rect1 = col1.Bounds();
            Rectangle rect2 = col2.Bounds();

            float dX = Math.Abs(rect1.X - rect2.X);
            float dY = Math.Abs(rect1.Y - rect2.Y);


            //determine rect2 position relative to rect1//

            bool leftSide = dY < dX && rect1.X > rect2.X;
            bool rightSide = dY < dX && rect1.X < rect2.X;

            bool topSide = dY > dX && rect1.Y < rect2.Y;
            bool bottomSide = dY > dX && rect1.Y > rect2.Y;

            if(leftSide) return LeftCollision();
            if (rightSide) return RightCollision();
            if (topSide) return TopCollision();
            if (bottomSide) return BottomCollision();

            return null;
        }

        public static Collision LeftCollision()
        {
            return new Collision(Collision.Direction.left);
        }
        public static Collision RightCollision()
        {
            return new Collision(Collision.Direction.left);
        }
        public static Collision TopCollision()
        {
            return new Collision(Collision.Direction.left);
        }
        public static Collision BottomCollision()
        {
            return new Collision(Collision.Direction.left);
        }
    }
}
