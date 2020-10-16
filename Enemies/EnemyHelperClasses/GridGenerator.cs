using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Generates list of list of rectangles for objects that move uniformly on a grid
    /// </summary>
    public class GridGenerator
    {

        private static GridGenerator instance = new GridGenerator();

        private List<List<Rectangle>> savedGrid;
        private Point tileSize;
        
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

            int screenWidth = game.GraphicsDevice.Viewport.Width;
            int screenHeight = game.GraphicsDevice.Viewport.Height;

            int tileWidth = screenWidth / tileColumns;
            int tileHeight = screenHeight / tileRows;
            Point tileSize = new Point(tileWidth, tileHeight);
            this.tileSize = tileSize;

            for (int i = 0; i < tileRows; i++)
            {
                gridTiles.Add(new List<Rectangle>());

                for (int j = 0; j < tileColumns; j++)
                {
                   Point position = new Point(j * tileWidth, i * tileHeight);

                    gridTiles[i].Add(new Rectangle(position, tileSize));

                }
            }

            savedGrid = gridTiles;



            return gridTiles;
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
    }
}
