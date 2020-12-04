using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class ArrowImpactState : IItemsState
    {
        private Arrow arrow;
        private int runTime;
        private const int maxTime = 75;
        private const int adjust = 20;

        public ArrowImpactState(Arrow arrow, string direction)
        {
            this.arrow = arrow;
            AdjustLocation(direction);
            runTime = 0;
            arrow.UpdateSprite(ItemsFactory.Instance.CreateProjectileImpactSprite());
        }

        private void AdjustLocation(string direction)
        {
            Vector2 loc = arrow.Location;
            if (direction.Equals("Right"))
            {
                loc.X += adjust;
            }
            else if (direction.Equals("Left"))
            {
                loc.X -= adjust;
            }
            else if (direction.Equals("Up"))
            {
                loc.Y -= adjust;
            }
            else if (direction.Equals("Down"))
            {
                loc.Y += adjust;
            }
            arrow.UpdateLocation(loc);
        }

        public void Update()
        {
            runTime++;
            if (runTime >= maxTime)
            {
                Expire();
            }
        }

        public void Expire()
        {
            arrow.IsExpired = true;

            arrow.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
           //can't collect
        }
    }
}
