using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    class DoorSpriteFactory
    {

        private Texture2D lockedTopDoor;
        private Texture2D lockedBottomDoor;
        private Texture2D lockedRightDoor;
        private Texture2D lockedLeftDoor;

        private Texture2D closedTopDoor;
        private Texture2D closedBottomDoor;
        private Texture2D closedRightDoor;
        private Texture2D closedLeftDoor;



        public static DoorSpriteFactory Instance { get; } = new DoorSpriteFactory();


        private DoorSpriteFactory()
        {
            
        }

        public void LoadAllTextures(Game1 game)
        {

            lockedTopDoor = game.Content.Load<Texture2D>("Procedural/LockedTop");
            lockedBottomDoor = game.Content.Load<Texture2D>("Procedural/LockedBottom");
            lockedRightDoor = game.Content.Load<Texture2D>("Procedural/LockedRight");
            lockedLeftDoor = game.Content.Load<Texture2D>("Procedural/LockedLeft");

            closedTopDoor = game.Content.Load<Texture2D>("Procedural/ClosedTop");
            closedBottomDoor = game.Content.Load<Texture2D>("Procedural/ClosedBottom");
            closedRightDoor = game.Content.Load<Texture2D>("Procedural/ClosedRight");
            closedLeftDoor = game.Content.Load<Texture2D>("Procedural/ClosedLeft");


        }

        StringComparison cmp = StringComparison.OrdinalIgnoreCase;
        public DoorSprite CreateDoor(int row, int col, string type)
        {
            
            if (type.Equals("LockedTop",cmp)) return new DoorSprite(lockedTopDoor, row, col);
            else if (type.Equals("LockedBottom", cmp)) return new DoorSprite(lockedBottomDoor, row, col);
            else if (type.Equals("LockedRight", cmp)) return new DoorSprite(lockedRightDoor, row, col);
            else if (type.Equals("LockedLeft", cmp)) return new DoorSprite(lockedLeftDoor, row, col);
            else if (type.Equals("ClosedTop", cmp) || type.Equals("OpenTop",cmp)) return new DoorSprite(closedTopDoor, row, col);
            else if (type.Equals("ClosedBottom", cmp) || type.Equals("OpenBottom", cmp)) return new DoorSprite(closedBottomDoor, row, col);
            else if (type.Equals("ClosedLeft", cmp) || type.Equals("OpenLeft", cmp)) return new DoorSprite(closedLeftDoor, row, col);
            else if (type.Equals("ClosedRight", cmp) || type.Equals("OpenRight", cmp)) return new DoorSprite(closedRightDoor, row, col);

            return null;
        }


    }
}
