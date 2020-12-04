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
        private Vector2 location;

        private Vector2 offset;

        private float currentHP;
        private float maxHP;

        private const float SCREEN_TO_BAR_RATIO = 4f / 5f;
        private const float BAR_WIDTH_RATIO = 1f / 10f;

        Camera cam = Camera.Instance;
        GridGenerator grid = GridGenerator.Instance;
        public BossHealthBar(IEnemy enemy, Game game, int maxHP)
        {
            this.enemy = enemy;
            this.maxHP = maxHP;
            currentHP = maxHP;

            int width = grid.GetTileSize().X * 12;
            int height = grid.GetTileSize().Y;

            

            sprite = new EnemyHPSprite(game,width,height);
            HPBarDrawer.AddBar(this);
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
