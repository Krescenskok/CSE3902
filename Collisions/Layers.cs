﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public static class Layers
    {
        public static Layer Enemy { get => new EnemyLayer(); }
        public static Layer EnemyProjectile { get => new EnemyProjectileLayer(); }
        public static Layer Player { get => new PlayerLayer(); }
        public static Layer PlayerWeapon {  get => new PlayerWeaponLayer(); }
        public static Layer Sword { get => new SwordLayer(); }
        public static Layer Wall { get => new ObstacleLayer(); }
        public static Layer Block { get => new ObstacleLayer(); }
        public static Layer Item { get => new ItemLayer(); }
        public static Layer Projectile { get => new ItemLayer(); }
        public static Layer Default { get => new DefaultLayer(); }
        public static Layer NPC { get => new NPC_Layer(); }
        public static Layer Trigger { get => new TriggerLayer(); }
        public static Layer Door { get => new ObstacleLayer(); }

        public static Layer Shield { get => new ShieldLayer(); }
    }
}
