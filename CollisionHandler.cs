using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// class which detects all collisions in game and sends appropriate messages to colliding objects
    /// </summary>
    public class CollisionHandler
    {
        public static CollisionHandler Instance { get; } = new CollisionHandler();


        /// <summary>
        /// All colliders currently in game
        /// </summary>
        private List<ICollider> colliders;

        private List<ICollider> removedColliders;

        
        /// <summary>
        /// All collisions that have happened since last Update
        /// </summary>
        private HashSet<List<ICollider>> collisions;

        private CollisionHandler()
        {
            colliders = new List<ICollider>();
            removedColliders = new List<ICollider>();
            
            ListComparer<ICollider> listComparer = new ListComparer<ICollider>();
            collisions = new HashSet<List<ICollider>>(listComparer);
            
        }


       

        public void AddCollider(ICollider newCol)
        {
            colliders.Add(newCol);
        }

        public void RemoveCollider(ICollider col)
        {
            removedColliders.Add(col);
        }

        public void Update()
        {

            //iterate through each pair of colliders, determine if they collide, handle collision
            for(int i = 0; i < colliders.Count; i++)
            {
                for(int j = 0; j < colliders.Count; j++)
                {

                    
                    List<ICollider> key = new List<ICollider> { colliders[i], colliders[j] };

                    

                    if (!colliders[i].Equals(colliders[j]) && ColliderOverlap(colliders[i],colliders[j])) //collision exists
                    {
                        Collision collision = CalculateSide(colliders[i],colliders[j]);
                        colliders[i].HandleCollision(colliders[j], collision);


                        if (collisions.Contains(key))   //collision was already happneing
                        {

                            colliders[i].HandleCollision(colliders[j], collision);

                        }
                        else  // collision is just starting
                        {
                            
                            collisions.Add(key);
                            colliders[i].HandleCollision(colliders[j], collision);
                            colliders[i].HandleCollisionEnter(colliders[j], collision);
                        }

                    }
                    else if(collisions.Contains(key)) //collision has just ended
                    {
                        collisions.Remove(key);
                    }

                    
                }
            }


            //remove colliders only after all collisions have been checked to prevent for loop error
            for(int i = 0; i < removedColliders.Count; i++)
            {
                colliders.Remove(removedColliders[i]);
            }

            removedColliders.Clear();
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

            if (leftSide) result =  LeftCollision(collisionPoint, col2);
            if (rightSide) result = RightCollision(collisionPoint, col2);
            if (topSide) result = TopCollision(collisionPoint, col2);
            if (bottomSide) result = BottomCollision(collisionPoint, col2);

            

            return result;

        }

        public static Collision LeftCollision(Vector2 loc, ICollider other)
        {
            return new Collision(Collision.Direction.left,loc, other);
        }
        public static Collision RightCollision(Vector2 loc, ICollider other)
        {
            return new Collision(Collision.Direction.right,loc, other);
        }
        public static Collision TopCollision(Vector2 loc, ICollider other)
        {
            return new Collision(Collision.Direction.up, loc, other);
        }
        public static Collision BottomCollision(Vector2 loc, ICollider other)
        {
            return new Collision(Collision.Direction.down, loc, other);
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
