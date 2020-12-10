using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    public class RoomWalls
    {

        private static readonly RoomWalls instance = new RoomWalls();

        private List<WallCollider> walls;
        public List<ICollider> Walls => walls.ConvertAll(x => x as ICollider);

        private Point sideWallSize;
        private Point middleWallSize;
        private Point sideWallHalfSize;
        private Point middleWallHalf;

        private List<Point> locations;
        private GridGenerator generator;
        

        private Game game;
        private Camera cam = Camera.Instance;


        public static RoomWalls Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomWalls()
        {
            walls = new List<WallCollider>();

            locations = new List<Point>();
        }


        public void LoadRoom(Game game, XElement room)
        {
            walls = new List<WallCollider>();
      
            this.game = game;
            generator = GridGenerator.Instance;

            if (locations.Count == 0) CalculateWallDrawLocations();

            List<XElement> items = room.Elements("Wall").ToList();
            foreach (XElement item in items)
            {
                XAttribute typeTag = item.Attribute("Type");

                string objName = typeTag.Value;
  
                Point camOffset = cam.Location.ToPoint();

                if (objName.Equals("Left"))
                {
                    walls.Add(new WallCollider(locations[0] - camOffset, sideWallSize));
                }
                else if (objName.Equals("Right"))
                {
                    walls.Add(new WallCollider(locations[2] - camOffset, sideWallSize));
                }
                else if (objName.Equals("Top"))
                {
                    walls.Add(new WallCollider(locations[0] - camOffset, middleWallSize));
                }
                else if (objName.Equals("Bottom"))
                {
                    walls.Add(new WallCollider(locations[5] - camOffset, middleWallSize));
                }
                else if (objName.Equals("LeftTop"))
                {
                    walls.Add(new WallCollider(locations[0] - camOffset, sideWallHalfSize));
                }
                else if (objName.Equals("LeftBottom"))
                {
                    walls.Add(new WallCollider(locations[6] - camOffset, sideWallHalfSize));
                }
                else if (objName.Equals("TopLeft"))
                {
                    walls.Add(new WallCollider(locations[0] - camOffset, middleWallHalf));
                }
                else if (objName.Equals("TopRight"))
                {
                    walls.Add(new WallCollider(locations[1] - camOffset, middleWallHalf));
                }
                else if (objName.Equals("RightTop"))
                {
                    walls.Add(new WallCollider(locations[2] - camOffset, sideWallHalfSize));
                }
                else if (objName.Equals("RightBottom"))
                {
                    walls.Add(new WallCollider(locations[3] - camOffset, sideWallHalfSize));

                }else if(objName.Equals("BottomLeft"))
                {
                    walls.Add(new WallCollider(locations[5] - camOffset, middleWallHalf));
                }
                else if (objName.Equals("BottomRight"))
                {
                    walls.Add(new WallCollider(locations[4] - camOffset, middleWallHalf));
                }




                
            }

        }






        private void CalculateWallDrawLocations()
        {
            int tileWidth = generator.GetTileSize().X;
            int tileHeight = generator.GetTileSize().Y;
            locations.Add(new Point(0, 0));
            locations.Add(new Point(tileWidth * 9, 0));
            locations.Add(new Point(tileWidth * 14, 0));
            locations.Add(new Point(tileWidth * 14, tileHeight * 6));
            locations.Add(new Point(tileWidth * 9, tileHeight * 9));
            locations.Add(new Point(0, tileHeight * 9));
            locations.Add(new Point(0, tileHeight * 6));


            sideWallSize = new Point(generator.Offset.X, game.Window.ClientBounds.Height);
            middleWallSize = new Point(game.Window.ClientBounds.Width, generator.Offset.Y);

            sideWallHalfSize = new Point(generator.Offset.X, tileHeight * 5);
            middleWallHalf = new Point(tileWidth * 7, generator.Offset.Y);
        }



        public void Update()
        {


            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Update();
            }
          

        }




    }
}
