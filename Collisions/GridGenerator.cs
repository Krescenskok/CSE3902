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

        private Point topLeftOffset;

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

            int roomSpriteGridOffsetY = 34;
            int roomSpriteGridOffsetX = 34;
            int roomSpriteGridWidth = 190;
            int roomSpriteGridHeight = 110;
            int roomSpriteWidth = 256;
            int roomSpriteHeight = 176;


            topLeftOffset = new Point(roomSpriteGridOffsetX * screenWidth / roomSpriteGridWidth, roomSpriteGridOffsetY * screenHeight / roomSpriteGridHeight);


            int playAreaWidth = roomSpriteGridWidth * screenWidth / roomSpriteWidth;
            int playAreaHeight = roomSpriteGridHeight * screenHeight / roomSpriteHeight;
            playArea = new Point(playAreaWidth, playAreaHeight);


            int tileWidth = playAreaWidth / tileColumns;
            int tileHeight = playAreaHeight / tileRows;
            Point tileSize = new Point(tileWidth, tileHeight);
            this.tileSize = tileSize;

            

            for (int i = 0; i < tileRows; i++)
            {
                gridTiles.Add(new List<Rectangle>());

                for (int j = 0; j < tileColumns; j++)
                {
                   Point position = new Point(j * tileWidth, i * tileHeight) + topLeftOffset;

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
            Vector2 result = new Vector2();

            result.X = col * tileSize.X;
            result.Y = row * tileSize.Y;

            result = Vector2.Add(result, topLeftOffset.ToVector2());

            return result;
        }

        public List<Rectangle> GetStraightPath(Rectangle start, Rectangle end)
        {

            int startX = start.X - topLeftOffset.X;
            int startY = start.Y - topLeftOffset.Y;
            int endX = end.X - topLeftOffset.X;
            int endY = end.Y = topLeftOffset.Y;
            
            bool vertical = startX == endX;
            

            List<Rectangle> path = new List<Rectangle>();

            

            if (vertical)
            {
                int increment = startY < endY ? 1 : -1;
                int col = startX / tileSize.X;
                int startRow = startY / tileSize.Y;
                int endRow = endY / tileSize.Y;
                
                for(int k = startRow; k != endRow + increment; k += increment)
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

        public int GetColumn(int xPosition)
        {
            return (xPosition - topLeftOffset.X) / tileSize.X;
        }

        public int GetRow(int yPosition)
        {
            return (yPosition - topLeftOffset.Y) / tileSize.Y;
        }
    }
}
