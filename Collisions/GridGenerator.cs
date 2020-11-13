using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Text;

namespace Sprint4
{
    /// <summary>
    /// Generates list of list of rectangles for objects that move uniformly on a grid
    /// </summary>
    public class GridGenerator
    {

        private static GridGenerator instance = new GridGenerator();

        private List<List<Rectangle>> savedGrid;
        private Point tileSize;

        private Point wallOffset;
        public Point Offset { get => wallOffset; }

        private Point playArea;
        
        public static GridGenerator Instance
        {
            get
            {
                return instance;
            }
        }


        private GridGenerator()
        {
            savedGrid = new List<List<Rectangle>>();
            tileSize = new Point();
        }

        public Point GetTileSize()
        {
            return tileSize;
        }

        public List<List<Rectangle>> GetGrid(Game game, int tileColumns, int tileRows)
        {
            List<List<Rectangle>> gridTiles = new List<List<Rectangle>>();


            int screenWidth = game.Window.ClientBounds.Width;
            int screenHeight = game.Window.ClientBounds.Height;

            //pixel values measured in paint
            wallOffset = new Point(100, 90); 
            int playAreaWidth = 600;
            int playAreaHeight = 322;
            
            int tileWidth = playAreaWidth / tileColumns;
            int tileHeight = playAreaHeight / tileRows;
            Point tileSize = new Point(tileWidth, tileHeight);
            
            this.tileSize = tileSize;

            

            for (int i = 0; i < tileRows; i++)
            {
                gridTiles.Add(new List<Rectangle>());

                for (int j = 0; j < tileColumns; j++)
                {
                   Point position = new Point(j * tileWidth, i * tileHeight) + wallOffset ;

                    gridTiles[i].Add(new Rectangle(position, tileSize));

                }
            }

            savedGrid = gridTiles;



            return gridTiles;
        }



        public List<Rectangle> CollisionGrid(Game game, int rows, int col)
        {
            List<Rectangle> grid = new List<Rectangle>();

            int cellSizeX = game.Window.ClientBounds.Width / col;
            int cellSizeY = game.Window.ClientBounds.Height / rows;

            Point cellSize = new Point(cellSizeX, cellSizeY);

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    grid.Add(new Rectangle(new Point(j * cellSizeX, i * cellSizeY), cellSize));
                }
            }


            return grid;
        }

        public List<List<Rectangle>> GetGrid()
        {
            return savedGrid;
        }

        public int GetGridWidth()
        {
            return savedGrid[0].Count * tileSize.X;
        }

        public int GetGridHeight()
        {
            return savedGrid.Count * tileSize.Y;
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


            for (int i = 0; i < savedGrid.Count; i++)
            {
                

                for (int j = 0; j < savedGrid[0].Count; j++)
                {


                    if (savedGrid[i][j].Contains(foundLocation.X, foundLocation.Y))
                    {
                        foundLocation = savedGrid[i][j];
                        
                        
                    }

                }
            }


            return foundLocation;
        }                               


        public Vector2 GetLocation(int row, int col)
        {
           
            return savedGrid[row][col].Location.ToVector2();
        }

        public List<Rectangle> GetStraightPath(Rectangle start, Rectangle end)
        {

            int startX = start.X - wallOffset.X;
            int startY = start.Y - wallOffset.Y;
            int endX = end.X - wallOffset.X;
            int endY = end.Y - wallOffset.Y;
            
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
                    path.Add(savedGrid[k][col]);

                    
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
                    path.Add(savedGrid[row][k]);
                    
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
            return (xPosition - wallOffset.X) / tileSize.X;
        }

        public int GetRow(int yPosition)
        {
            return (yPosition - wallOffset.Y) / tileSize.Y;
        }
    }
}
