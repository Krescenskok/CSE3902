using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sprint3
{
    public class CollisionHandler
    {
        public static CollisionHandler Instance { get; } = new CollisionHandler();

        private List<ICollider> colliders;


        private HashSet<List<ICollider>> collisions;

        private CollisionHandler()
        {
            colliders = new List<ICollider>();

            ListComparer<ICollider> listComparer = new ListComparer<ICollider>();
            collisions = new HashSet<List<ICollider>>(listComparer);

        }




        public void AddCollider(ICollider newCol)
        {
            colliders.Add(newCol);
        }

        public void Update()
        {

            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = 0; j < colliders.Count; j++)
                {

                    List<ICollider> key = new List<ICollider> { colliders[i], colliders[j] };



                    if (!colliders[i].Equals(colliders[j]) && ColliderOverlap(colliders[i], colliders[j])) 
                    {
                        Collision collision = CalculateSide(colliders[i], colliders[j]);
                        colliders[i].HandleCollision(colliders[j], collision);


                        if (collisions.Contains(key))  
                        {

                            colliders[i].HandleCollision(colliders[j], collision);

                        }
                        else  
                        {

                            collisions.Add(key);
                            colliders[i].HandleCollision(colliders[j], collision);
                            colliders[i].HandleCollisionEnter(colliders[j], collision);
                        }

                    }
                    else if (collisions.Contains(key)) 
                    {
                        collisions.Remove(key);
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

            bool rightSide = l_collision <= r_collision && l_collision <= t_collision && l_collision <= b_collision;
            bool leftSide = r_collision <= l_collision && r_collision <= t_collision && r_collision <= b_collision;
            bool topSide = b_collision <= t_collision && b_collision <= l_collision && b_collision <= r_collision;
            bool bottomSide = t_collision <= b_collision && t_collision <= l_collision && t_collision <= r_collision;



            Rectangle overlap = Rectangle.Intersect(rect1, rect2);
            Vector2 collisionPoint = overlap.Center.ToVector2();

            Collision result = new Collision();

            if (leftSide) result = LeftCollision(collisionPoint);
            if (rightSide) result = RightCollision(collisionPoint);
            if (topSide) result = TopCollision(collisionPoint);
            if (bottomSide) result = BottomCollision(collisionPoint);



            return result;

        }

        public static Collision LeftCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.left, loc);
        }
        public static Collision RightCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.right, loc);
        }
        public static Collision TopCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.up, loc);
        }
        public static Collision BottomCollision(Vector2 loc)
        {
            return new Collision(Collision.Direction.down, loc);
        }



        class ListComparer<T> : IEqualityComparer<List<T>>
        {
            public bool Equals(List<T> x, List<T> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(List<T> obj)
            {
                return 0;
            }
        }
    }
}
