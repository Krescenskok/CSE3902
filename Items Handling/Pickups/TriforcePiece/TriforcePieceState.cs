using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class TriforcePieceState : IItemsState
    {
        private TriforcePiece item;
        private Vector2 position;
        private int runTime;
        private int frame;

        public TriforcePieceState(TriforcePiece item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
            runTime = 0;
            frame = 0;
        }

        public void Update()
        {
            if (runTime % 5 == 0)
            {
                frame++;
            }
            item.UpdateFrame(frame % 2);
            runTime++;
        }

        public void Expire()
        {
            RoomItems.Instance.Destroy(item);

            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());

            CollisionHandler.Instance.RemoveCollider(item.Collider);

            item.Expire();
        }

        public void Collected()
        {
        }
    }
}
