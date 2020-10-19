using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class Dodongo : IEnemy
    {
        IEnemyState dodongoState;
        ISprite dodongoSprite;
        private int dodongoHP;
        private Vector2 dodongoPos;

        public Dodongo(Vector2 initialPos)
        {
            dodongoPos = initialPos;
            dodongoHP = 3;
            dodongoState = new DodongoMovingState(this, initialPos);

            SetSprite("Right");
        }

        public void SetSprite(string direction)
        {
            dodongoSprite = EnemySpriteFactory.Instance.CreateDodongoSprite(direction);
        }

        public void Update()
        {
            dodongoState.Update();
        }

        public void UpdatePos(Vector2 newPos)
        {
            dodongoPos = newPos;
        }

        public void TakeDamage(int amount)
        {
            dodongoState.TakeDamage(amount);
        }

        public void LostHp()
        {
            dodongoHP--;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            dodongoSprite.Draw(spriteBatch, dodongoPos, 0, Color.White);
        }

        public void SetSprite(ISprite sprite)
        {
            throw new NotImplementedException();
        }

        public void Spawn()
        {
            throw new NotImplementedException();
        }
    }
}
