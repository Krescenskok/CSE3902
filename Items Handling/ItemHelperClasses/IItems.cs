﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public interface IItems
    {
        void Update();

        void Expire();

        void Draw(SpriteBatch spriteBatch);

        Vector2 Location { get; }

        IItemsState State { get; }

        ICollider Collider { get; }

        bool IsExpired { get; set; }
    }
}
