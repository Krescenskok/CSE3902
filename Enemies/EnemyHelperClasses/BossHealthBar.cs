using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class BossHealthBar : HPBar
    {
        private IEnemy enemy;
        private EnemyHPSprite sprite;


        private float currentHP;
        private float maxHP;

        Camera cam = Camera.Instance;
        GridGenerator grid = GridGenerator.Instance;

        public BossHealthBar(IEnemy enemy)
        {
            this.enemy = enemy;
            this.maxHP = enemy.HP;
            currentHP = maxHP;

            int width = grid.GetTileSize().X * 12;
            int height = grid.GetTileSize().Y;

            sprite = new EnemyHPSprite(width,height);
        }



        public void Update()
        {
 
            currentHP = enemy.HP;
            float fill = currentHP / maxHP;
            sprite.Update(fill);

            if (fill == 0) HPBarDrawer.Remove(this);
        }

        public void Draw(SpriteBatch batch)
        {
            Vector2 loc = new Vector2(grid.GetTileSize().X * 2, grid.GetTileSize().Y * 0.5f);
            sprite.Draw(batch, loc - cam.Location, 0, Color.White);
        }
    }
}
