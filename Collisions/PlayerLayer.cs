﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class PlayerLayer : Layer
    {
        public bool AttachedToPlayer => true;
        public bool CollidesWith(ICollider other)
        {
            return other.layer is EnemyLayer
                || other.layer is ItemLayer
                || other.layer is ObstacleLayer
                || other.layer is NPC_Layer
                || other.layer is DefaultLayer;

        }
    }
}
