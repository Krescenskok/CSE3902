using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sprint4
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

        private Dictionary<int, List<ICollider>> spatialMap;
        int rows = 3, col = 6;
        Point cellSize;
        int width;

        private CollisionHandler()
        {
            colliders = new List<ICollider>();
            removedColliders = new List<ICollider>();
            
            ListComparer<ICollider> listComparer = new ListComparer<ICollider>();
          
            collisions = new HashSet<List<ICollider>>(listComparer);

        }


       

        public void AddCollider(ICollider newCol, Layer layer)
        {
            newCol.layer = layer;
            colliders.Add(newCol);
        }
      

        public void RemoveCollider(ICollider col)
        {
            removedColliders.Add(col);
        }

        public void RemoveCollider(List<ICollider> colliders)
        {
            foreach (ICollider col in colliders) removedColliders.Add(col);
        }
        


        public void Initialize(Game game)
        {
            
            spatialMap = new Dictionary<int, List<ICollider>>(rows * col);
            for (int i = 0; i < rows * col; i++) spatialMap.Add(i, new List<ICollider>());

            cellSize.X = game.Window.ClientBounds.Width / col;
            cellSize.Y = game.Window.ClientBounds.Height / rows;
            width = game.Window.ClientBounds.Width / cellSize.X;
        }

        public void RoomChange()
        {
            for(int i = 0; i < colliders.Count; i++)
            {
                if(!(colliders[i].layer is PlayerLayer))
                {
                    removedColliders.Add(colliders[i]);
                }
            }

            colliders = colliders.Except(removedColliders).ToList();
            removedColliders.Clear();
        }

        public void ClearMap()
        {
            spatialMap.Clear();
            for (int i = 0; i < rows * col; i++) spatialMap.Add(i, new List<ICollider>());
        }

        public void ResetMap()
        {
            ClearMap();

        }

        private void RegisterCollider(ICollider col)
        {
            List<int> codes = GetCode(col.Bounds());
            foreach(int id in codes)
            {
                spatialMap[id].Add(col);
            }
        }

        private List<int> GetCode(Rectangle rect)
        {

            List<int> results = new List<int>();

            Point camOffset = Camera.Instance.Location.ToPoint(); ;
            Point topLeft = rect.Location + camOffset;
            Point topRight = new Point(rect.Right, rect.Top) + camOffset;
            Point bottomLeft = new Point(rect.Left, rect.Bottom) + camOffset;
            Point bottomRight = new Point(rect.Right, rect.Bottom) + camOffset;

            AddToList(results, topLeft);
            AddToList(results, topRight);
            AddToList(results, bottomLeft);
            AddToList(results, bottomRight);

         

            return results;
        }

        private void AddToList(List<int> list, Point location)
        {
            int spatialPosition = location.X / cellSize.X + (location.Y / cellSize.Y) * width;
            if (spatialPosition >= rows*col || spatialPosition < 0) spatialPosition = rows*col - 1;
            if (!list.Contains(spatialPosition)) list.Add(spatialPosition);
        }

        private List<ICollider> FindNearbyColliders(ICollider col)
        {
            List<ICollider> colliders = new List<ICollider>();
            List<int> codes = GetCode(col.Bounds());

            foreach(int id in codes)
            {
                colliders.AddRange(spatialMap[id]);
            }

            return colliders;
        }

        public void Update()
        {

            //add each collider to buckets in the spatial map
            for(int i = 0; i < colliders.Count; i++)
            {
                colliders[i].Update();
                RegisterCollider(colliders[i]);
            }

            int numCol = 0;

            //iterate through each collider and check if it collides with nearby colliders
            for (int i = 0; i < colliders.Count; i++)
            {
                List<ICollider> nearbyColliders = FindNearbyColliders(colliders[i]);

                nearbyColliders.AddRange(RoomWalls.Instance.Walls); //walls too big for spatial mapping

                foreach(ICollider other in nearbyColliders)
                {
                    List<ICollider> key = new List<ICollider> { colliders[i], other };


                    if (!colliders[i].Equals(other) && colliders[i].layer.CollidesWith(other) && ColliderOverlap(colliders[i], other)) //collision exists
                    {
                        Collision collision = CalculateSide(colliders[i], other);

                        numCol++;

                        if (collisions.Contains(key))   //collision was already happneing
                        {

                            colliders[i].HandleCollision(other, collision);

                        }
                        else  // collision is just starting
                        {

                            collisions.Add(key);
                            colliders[i].HandleCollision(other, collision);
                            colliders[i].HandleCollisionEnter(other, collision);
                        }

                    }
                    else if (collisions.Contains(key)) //collision has just ended
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

            //reset spatial map
            ClearMap();
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
            return new Collision(Direction.left,loc, other);
        }
        public static Collision RightCollision(Vector2 loc, ICollider other)
        {
            return new Collision(Direction.right,loc, other);
        }
        public static Collision TopCollision(Vector2 loc, ICollider other)
        {
            return new Collision(Direction.up, loc, other);
        }
        public static Collision BottomCollision(Vector2 loc, ICollider other)
        {
            return new Collision(Direction.down, loc, other);
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
