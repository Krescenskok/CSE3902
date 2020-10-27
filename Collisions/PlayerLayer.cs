﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class PlayerLayer : Layer
    {
        public bool CollidesWith(ICollider other)
        {
            return other.layer is EnemyLayer
                || other.layer is ItemLayer
                || other.layer is ObstacleLayer
                || other.layer is DefaultLayer;

        }
    }
}
