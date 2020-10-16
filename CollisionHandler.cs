using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Sprint3
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

            float b_collision = rect2.Bottom - rect1.Y;
            float t_collision = rect1.Bottom - rect2.Y;
            float l_collision = rect1.Right - rect2.X;
            float r_collision = rect2.Right - rect1.X;

            bool rightSide = l_collision < r_collision && l_collision < t_collision && l_collision < b_collision;
            bool leftSide = r_collision < l_collision && r_collision < t_collision && r_collision < b_collision;
            bool topSide = b_collision < t_collision && b_collision < l_collision && b_collision < r_collision;
            bool bottomSide = t_collision < b_collision && t_collision < l_collision && t_collision < r_collision;
            
            //determine rect2 position relative to rect1//

            Rectangle overlap = Rectangle.Intersect(rect1, rect2);
            Vector2 collisionPoint = overlap.Center.ToVector2();

            if (leftSide) return LeftCollision(collisionPoint);
            if (rightSide) return RightCollision(collisionPoint);
            if (topSide) return TopCollision(collisionPoint);
            if (bottomSide) return BottomCollision(collisionPoint);

            return LeftCollision(collisionPoint);

            //throw new NullReferenceException("collision direction could not be calculated");
        }

        public static Collision LeftCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.left,loc);
        }
        public static Collision RightCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.right,loc);
        }
        public static Collision TopCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.up, loc);
        }
        public static Collision BottomCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.down, loc);
        }
    }
}
