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
        private const int THIRD = 3;
        private const string FULL = "Full";
        private const string HALF = "Half";
        private const string EMPTY = "Empty";
        private const string TOP = "Top";
        private const string BOTTOM = "Bottom";

        private HUDStorage storage = HUDStorage.Instance;
        private static readonly HeartManagement instance = new HeartManagement();
        public static HeartManagement Instance { get => instance; }
        public HeartManagement() { }

        public void InitializeHearts()
        {
            int i;
            for (i = 0; i < storage.MaxHearts; i++)
            {
                storage.DrawnHearts.Add(CreateHeart(FULL, i, TOP));
                storage.DrawnHeartsBottom.Add(CreateHeart(FULL, i, BOTTOM));
            }
        }

        public IItems CreateHeart(string type, int position, string place)
        {
            Vector2 location;
            if (place is TOP) location = new Vector2(storage.FirstHeartLoc.X + position * HEART_GAP, storage.FirstHeartLoc.Y); 
                else location = new Vector2(storage.FirstBottomHeartLoc.X + position * HEART_GAP, storage.FirstBottomHeartLoc.Y);

            if (type is FULL) return new FullHeart(ItemsFactory.Instance.CreateFullHeartSprite(), location);
                else if (type is HALF) return new HalfHeart(ItemsFactory.Instance.CreateHalfHeartSprite(), location);
                else return new EmptyHeart(ItemsFactory.Instance.CreateEmptyHeartSprite(), location);
        }

        public void IncreaseMaxHeartNumber()
        {
            if (storage.MaxHearts < DifficultyMultiplier.Instance.DetermineLinkMaxHP())
            {
                storage.MaxHearts++;
            }
        }

        public void ChangeDifficulty(int hp, int max)
        {
            //sets new max and prev to current so itll calculate normally
            storage.MaxHearts = max / 20;
            storage.PreviousHealth = 1;
        }

        public void UpdateHearts(LinkPlayer link)
        {
            if (storage.PreviousHealth < 0)
            {
                storage.PreviousHealth = (int)link.Health;
            }

            if (storage.PreviousHealth == (int)link.Health)
            {
                return;
            }

            int fullCount = 0, emptyCount = 0, halfCount = 0;
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
            foreach (IItems item in storage.DrawnHearts)
            {
                item.Expire();
            }
            storage.DrawnHearts.Clear();
            storage.DrawnHeartsBottom.Clear();

            int i;
            for (i = 0; i < storage.MaxHearts; i++)
            {
                if (fullCount > 0)
                {
                    storage.DrawnHearts.Add(CreateHeart(FULL, i, TOP));
                    storage.DrawnHeartsBottom.Add(CreateHeart(FULL, i, BOTTOM));
                    fullCount--;
                }
                else if (halfCount > 0)
                {
                    storage.DrawnHearts.Add(CreateHeart(HALF, i, TOP));
                    storage.DrawnHeartsBottom.Add(CreateHeart(HALF, i, BOTTOM));
                    halfCount--;
                }
                else
                {
                    storage.DrawnHearts.Add(CreateHeart(EMPTY, i, TOP));
                    storage.DrawnHeartsBottom.Add(CreateHeart(EMPTY, i, BOTTOM));
                    emptyCount--;
                }
            }
        }

        public void Reset()
        {
            storage.MaxHearts = THIRD;
            storage.DrawnHearts.Clear();
            storage.DrawnHeartsBottom.Clear();
            InitializeHearts();
        }


    }
}
