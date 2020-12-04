using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class EnemyHealthBar : HPBar
    {
        private IEnemy enemy;
        private EnemyHPSprite sprite;
        private Vector2 location;

        private Vector2 offset;

        private float currentHP;
        private float maxHP;
        public EnemyHealthBar(IEnemy enemy, Rectangle enemyRect,float size)
        {
            this.enemy = enemy;
            maxHP = enemy.HP;
            currentHP = maxHP;
           
            int width = (int)(enemyRect.Width * size);
            int height = enemyRect.Height / 5;

            Rectangle initial = new Rectangle(enemy.Location.ToPoint(), new Point(width, height));

            offset.X = enemyRect.Center.X - initial.Center.X;
            offset.Y = height * -1;

            sprite = new EnemyHPSprite(width,height);

        }

        public void Update()
        {
            location = enemy.Location + offset;

            currentHP = enemy.HP;
            float fill = currentHP / maxHP;
            sprite.Update(fill);

            if (fill == 0) HPBarDrawer.Remove(this);
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }
    }
}
