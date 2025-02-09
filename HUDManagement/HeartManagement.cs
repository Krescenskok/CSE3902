using Microsoft.Xna.Framework;
using Sprint5.Items;
using System;
using System.Collections.Generic;
using System.Text;
//using Sprint5.DifficultyHandling;

namespace Sprint5.HUDManagement
{
    public class HeartManagement
    {
        private const int HEART_GAP = 30;
        private const int FULL_HEART = 20;
        private const int HALF_HEART = 10;
        private const string FULL = "Full";
        private const string HALF = "Half";
        private const string EMPTY = "Empty";

        private HUDStorage storage = HUDStorage.Instance;
        private static readonly HeartManagement instance = new HeartManagement();
        public static HeartManagement Instance { get => instance; }
        public HeartManagement() { }

        public void InitializeHearts()
        {
            storage.DrawnHeartsBottom.Clear();
            int i;
            for (i = 0; i < storage.MaxHearts; i++)
            {
                storage.DrawnHeartsBottom.Add(CreateHeart(FULL, i));
            }
        }

        public IItems CreateHeart(string type, int position)
        {
            Vector2 location  = new Vector2(storage.FirstBottomHeartLoc.X + position * HEART_GAP, storage.FirstBottomHeartLoc.Y);

            if (type is FULL) return new FullHeart(ItemsFactory.Instance.CreateFullHeartSprite(), location);
                else if (type is HALF) return new HalfHeart(ItemsFactory.Instance.CreateHalfHeartSprite(), location);
                else return new EmptyHeart(ItemsFactory.Instance.CreateEmptyHeartSprite(), location);
        }

        public void IncreaseMaxHeartNumber()
        {
            storage.MaxHearts++;
        }

        public void UpdateHearts(LinkPlayer link)
        {
            int fullCount = 0, emptyCount = 0, halfCount = 0;
            storage.MaxHearts = (int)(link.FullHealth / FULL_HEART);
            storage.PreviousHealth = (int)link.Health;
            int healthLost = (int)(link.FullHealth - link.Health);
            if (healthLost != 0)
            {
                if (healthLost % FULL_HEART == 0)
                {
                    emptyCount = healthLost / FULL_HEART;
                }
                else if (healthLost % HALF_HEART == 0)
                {
                    halfCount = (healthLost / HALF_HEART) % 2;
                    emptyCount = (healthLost - HALF_HEART) / FULL_HEART;
                }
            }
            fullCount = storage.MaxHearts - halfCount - emptyCount;
            RedrawHearts(fullCount, halfCount, emptyCount);

        }

        private void RedrawHearts(int fullCount, int halfCount, int emptyCount)
        {
            foreach (IItems item in storage.DrawnHeartsBottom)
            {
                item.Expire();
            }
            storage.DrawnHeartsBottom.Clear();

            int i;
            for (i = 0; i < storage.MaxHearts; i++)
            {
                if (fullCount > 0)
                {
                    storage.DrawnHeartsBottom.Add(CreateHeart(FULL, i));
                    fullCount--;
                }
                else if (halfCount > 0)
                {
                    storage.DrawnHeartsBottom.Add(CreateHeart(HALF, i));
                    halfCount--;
                }
                else
                {
                    storage.DrawnHeartsBottom.Add(CreateHeart(EMPTY, i));
                    emptyCount--;
                }
            }
        }

        public void Reset(LinkPlayer link)
        {
            UpdateHearts(link);
        }


    }
}
