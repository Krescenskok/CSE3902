using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Generates list of list of rectangles for objects that move uniformly on a grid
    /// </summary>
    public class GridGenerator
    {

        private static GridGenerator instance = new GridGenerator();

        private List<List<Rectangle>> playAreaGrid;
        public List<List<Rectangle>> OutsideArea { get; private set; }
       
        private Point tileSize;

        private Point wallOffset;
        public Point Offset { get => wallOffset; }

        public int offsetYBottom;


        private const float OFFSET_MULT_X = 1f / 8f;
        private const float OFFSET_MULT_Y = 9f / 48f;
        private const float OFFSET_MULT_Y_BOTTOM = 17f / 120f;

        public static GridGenerator Instance
        {
            get
            {
                return instance;
            }
        }


        private GridGenerator()
        {
            playAreaGrid = new List<List<Rectangle>>();
            tileSize = new Point();
        }

        public Point GetTileSize()
        {
            return tileSize;
        }

        public List<List<Rectangle>> GetGrid(int tileColumns, int tileRows)
        {
            List<List<Rectangle>> gridTiles = new List<List<Rectangle>>();



            float screenWidth = Camera.Instance.playArea.Width;
            float screenHeight = Camera.Instance.playArea.Height;

            //pixel values measured in paint
            

            int offsetX = (int) (screenWidth * OFFSET_MULT_X);
            int offsetY = (int)(screenHeight * OFFSET_MULT_Y);
            offsetYBottom = (int)(screenHeight * OFFSET_MULT_Y_BOTTOM);
            wallOffset = new Point(offsetX, offsetY);

            int playAreaWidth = (int)screenWidth - offsetX * 2;
            int playAreaHeight = (int)screenHeight - offsetY - offsetYBottom;


            int tileWidth = playAreaWidth / tileColumns;
            int tileHeight = playAreaHeight / tileRows;
            Point tileSize = new Point(tileWidth, tileHeight);
            
            this.tileSize = tileSize;

            

            for (int i = 0; i < tileRows; i++)
            {
                gridTiles.Add(new List<Rectangle>());

                for (int j = 0; j < tileColumns; j++)
                {
                   Point position = new Point(j * tileWidth, i * tileHeight) + wallOffset - Camera.Instance.Location.ToPoint();

                    gridTiles[i].Add(new Rectangle(position, tileSize));

                }
            }

            playAreaGrid = gridTiles;
            FormOutsideArea();

     
            return gridTiles;
        }


        public void FormOutsideArea()
        {
            Point startPoint = playAreaGrid[0][0].Location - new Point(tileSize.X * 2, tileSize.Y * 2);
            OutsideArea = new List<List<Rectangle>>();

            int width = tileSize.X;
            int height = tileSize.Y;
            int numRows = 11, numCol = 16;

            for (int i = 0; i < numRows; i++)
            {
                OutsideArea.Add(new List<Rectangle>());

                for (int j = 0; j < numCol; j++)
                {
                    Point position = new Point(j * width, i * height) + startPoint;

                    OutsideArea[i].Add(new Rectangle(position, tileSize));

                }
            }
        }

        public List<List<Rectangle>> GetGrid()
        {
            return playAreaGrid;
        }

        public int GetGridWidth()
        {
            return playAreaGrid[0].Count * tileSize.X;
        }

        public int GetGridHeight()
        {
            return playAreaGrid.Count * tileSize.Y;
        }

        /// <summary>
        /// Snaps input location to rectangle on grid. Returns null grid not yet formed.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Rectangle GetGridLocation(Vector2 location)
        {
            

            Point position = new Point((int)location.X, (int)location.Y);
            
            Rectangle foundLocation = new Rectangle(position, tileSize);


            for (int i = 0; i < playAreaGrid.Count; i++)
            {
                

                for (int j = 0; j < playAreaGrid[0].Count; j++)
                {


                    if (playAreaGrid[i][j].Contains(foundLocation.X, foundLocation.Y))
                    {
                        foundLocation = playAreaGrid[i][j];
                        
                        
                    }

                }
            }


            return foundLocation;
        }                               


        public Vector2 GetLocation(int row, int col)
        {
            if (row > playAreaGrid.Count-1 || col > playAreaGrid[0].Count-1) return Vector2.Zero;
            return playAreaGrid[row][col].Location.ToVector2();
        }

        public Vector2 GetOutsideLocation(int row, int col)
        {
            return OutsideArea[row][col].Location.ToVector2();
        }

        public List<Rectangle> GetStraightPath(Rectangle start, Rectangle end)
        {
            int camOffsetX = (int)Camera.Instance.Location.X;
            int camOffsetY = (int)Camera.Instance.Location.Y;

            int startX = start.X - wallOffset.X + camOffsetX;
            int startY = start.Y - wallOffset.Y + camOffsetY;
            int endX = end.X - wallOffset.X + camOffsetX;
            int endY = end.Y - wallOffset.Y + camOffsetY;

            bool vertical = startX == endX;
            

            List<Rectangle> path = new List<Rectangle>();

            

            if (vertical)
            {
                int increment = startY < endY ? 1 : -1;
                int col = startX / tileSize.X;
                int startRow = startY / tileSize.Y;
                int endRow = endY / tileSize.Y;

                

                for (int k = startRow; k != endRow + increment; k += increment)
                {
                    path.Add(playAreaGrid[k][col]);

                    
                }
            }
            else
            {
                int increment = startX < endX ? 1 : -1;
                int startCol = startX / tileSize.X;
                int row = startY / tileSize.Y;
                int endCol = endX / tileSize.X;

                

                for (int k = startCol; k != endCol + increment; k += increment)
                {
                    path.Add(playAreaGrid[row][k]);
                    
                }
            }
           
            return path;

        }

        public Rectangle PathCollider(List<Rectangle> rects)
        {
            Rectangle rect = new Rectangle();
            if(rects.Count > 0) rect = rects[0];
            for(int i = 1; i < rects.Count; i++)
            {
                rect = Rectangle.Union(rect, rects[i]);
            }
            return rect;
        }

        public int GetColumn(int xPosition)
        {
            return (xPosition - wallOffset.X + (int)Camera.Instance.Location.X) / tileSize.X;
        }

        public int GetRow(int yPosition)
        {
            return (yPosition - wallOffset.Y + (int)Camera.Instance.Location.Y) / tileSize.Y;
        }
    }
}
