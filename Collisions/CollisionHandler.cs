using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// class which detects all collisions in game and sends appropriate messages to colliding objects
    /// </summary>
    public class CollisionHandler
    {
        public static CollisionHandler Instance { get; } = new CollisionHandler();

        private List<ICollider> currentColliders;
        private Queue<ICollider> collidersToBeRemoved;
        private HashSet<int> collisions;

        private Dictionary<int, List<ICollider>> spatialMap;
        private int rows = 3, col = 6;
        private Point cellSize;
        private int width;

        private CollisionHandler()
        {
            currentColliders = new List<ICollider>();
            collidersToBeRemoved = new Queue<ICollider>();
            collisions = new HashSet<int>();
        }

        public void Initialize()
        {

            spatialMap = new Dictionary<int, List<ICollider>>(rows * col);
            for (int i = 0; i < rows * col; i++) spatialMap.Add(i, new List<ICollider>());

            Point screenSize = Camera.Instance.playArea.Size;

            cellSize.X = screenSize.X / col;
            cellSize.Y = screenSize.Y / rows;
            width = screenSize.X / cellSize.X;
        }

        public void Update()
        {

            for (int i = 0; i < currentColliders.Count; i++)
            {
                currentColliders[i].Update();
                RegisterCollider(currentColliders[i]);
            }


            for (int i = 0; i < currentColliders.Count; i++)
            {

                List<ICollider> nearbyColliders = FindNearbyColliders(currentColliders[i]);
                Collision collision = new Collision();

                foreach (ICollider other in nearbyColliders)
                {

                    long a = currentColliders[i].GetHashCode(), b = other.GetHashCode(); a++; b++;
                    long num = (a * b*(a *a - b *b) / (a*a + b*b));
                    int key = (int)num;


                    if (!currentColliders[i].Equals(other) && currentColliders[i].layer.CollidesWith(other) && ColliderOverlap(currentColliders[i], other)) //collision exists
                    {
                        collision = CalculateSide(currentColliders[i], other);

                        if (collisions.Contains(key))   //collision was already happneing
                        {

                            currentColliders[i].HandleCollision(other, collision);

                        }
                        else  // collision is just starting
                        {
          
                            collisions.Add(key);

                            currentColliders[i].HandleCollisionEnter(other, collision);
                            currentColliders[i].HandleCollision(other, collision);
                        }

                    }
                    else if (collisions.Contains(key)) //collision has just ended
                    {
                        collisions.Remove(key);
                        currentColliders[i].HandleCollisionExit(other, collision);
                    }


                }


            }



            while (collidersToBeRemoved.Count > 0)
            {
                currentColliders.Remove(collidersToBeRemoved.Dequeue());
            }

            ResetSpatialMap();
        }

        private bool ColliderOverlap(ICollider col1, ICollider col2)
        {
            return col1.Bounds().Intersects(col2.Bounds());
        }

        public void AddCollider(ICollider newCol, Layer layer)
        {
            newCol.layer = layer;
            currentColliders.Add(newCol);
        }
      

        public void RemoveCollider(ICollider col)
        {
            collidersToBeRemoved.Enqueue(col);
        }

        public void RemoveCollider(List<ICollider> colliders)
        {
            foreach (ICollider col in colliders) collidersToBeRemoved.Enqueue(col);
        }

        
        public void RoomChange()
        {
            for(int i = 0; i < currentColliders.Count; i++)
            {
                
                if(!(currentColliders[i].layer.AttachedToPlayer))
                {
                    collidersToBeRemoved.Enqueue(currentColliders[i]);
                }
            }

            currentColliders.RemoveAll(collidersToBeRemoved.Contains);
            collidersToBeRemoved.Clear();
        }


        #region spatial mapping
        private List<ICollider> FindNearbyColliders(ICollider col)
        {
            List<ICollider> colliders = new List<ICollider>();
            List<int> codes = GetCode(col.Bounds());

            foreach (int id in codes)
            {
                colliders.AddRange(spatialMap[id]);
            }

            return colliders;
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
            
            if(results.Count > 2)
            {
                for (int i = results[0] + 1; i < results[1]; i++)
                {
                    results.Add(i);
                }
            }
            if(results.Count >= 4)
            {
                for (int i = results[0] + col; i < results[2]; i += col)
                {
                    results.Add(i);
                }
                for (int i = results[1] + col; i < results[3]; i += col)
                {
                    results.Add(i);
                }
                for (int i = results[2] + 1; i < results[3]; i++)
                {
                    results.Add(i);
                }
            }

           
            return  results.Distinct().ToList();;
        }

        private void AddToList(List<int> list, Point location)
        {
            int spatialPosition = location.X / cellSize.X + (location.Y / cellSize.Y) * width;
            if (spatialPosition >= rows*col || spatialPosition < 0) spatialPosition = rows*col - 1;
            list.Add(spatialPosition);
        }

        public void ResetSpatialMap()
        {
            spatialMap.Clear();
            for (int i = 0; i < rows * col; i++) spatialMap.Add(i, new List<ICollider>());
        }
        #endregion


        #region calculate direction of collisions
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

        #endregion


        #region new list equality comparer
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
        #endregion

    }
}
