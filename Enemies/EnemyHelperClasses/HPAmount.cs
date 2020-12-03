using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sprint5
{
    public class HPAmount
    {
        public static HPAmount Instance { get; } = new HPAmount();

        public const int Full_Heart = 20, ThreeQuarterHeart = 15, HalfHeart = 10, QuarterHeart = 5;

        //EnemyLevel# where # refers to number of hits needed to kill the enemy
        public const int EnemyLevel1 = 5, EnemyLevel2 = 10, EnemyLevel3 = 15, EnemyLevel4 = 20;
        public const int OneHit = 5, TwoHits = 10, ThreeHits = 15, FourHits = 20;
        public const int Zero = 0;
      
    }
}
