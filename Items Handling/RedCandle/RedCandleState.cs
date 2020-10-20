using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class RedCandleState : IItemsState
    {
        private RedCandle candle;
        private Vector2 position;

        public RedCandleState(RedCandle candle, Vector2 initPos)
        {
            this.candle = candle;
            this.position = initPos;
        }

        public void Update()
        {

        }

        public void Expire()
        {

        }

        public void Collected()
        {

        }
    }
}
