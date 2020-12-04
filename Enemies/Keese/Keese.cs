using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using Sprint5.DifficultyHandling;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class Keese : IEnemy
    {
        private XElement saveData;
        public IEnemyState state;
        private EnemySprite sprite;

        private EnemyCollider collider;

        public int HP { get; private set; } = HPAmount.EnemyLevel1;
        private const float barSize = 1.5f;


        private Point spriteSize;
        private Rectangle rect;

        public Vector2 Location { get; set; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }

        

        public Keese(Game game, Vector2 location, XElement xml)
        {
            Location = location;
            state = new EnemySpawnState(this, game);

            collider = new EnemyCollider();
            HP = DifficultyMultiplier.Instance.DetermineEnemyHP(HP);

            saveData = xml;
        }

        public void Spawn()
        {
            
            state = new KeeseMoveState(this, Location);
            KeeseMoveSprite kSprite = (KeeseMoveSprite)sprite;
            spriteSize = kSprite.GetRectangle().Size;
            rect = new Rectangle(Location.ToPoint(), spriteSize);
            rect = HitboxAdjuster.Instance.AdjustHitbox(rect, .6f);

            collider = new EnemyCollider(HitboxAdjuster.Instance.AdjustHitbox(rect, .5f), this, HPAmount.HalfHeart, "Keese");

            HPBarDrawer.AddBar(new EnemyHealthBar(this, rect, barSize));
        }
        public void SetSprite(ISprite sprite)
        {
            this.sprite = (EnemySprite)sprite;
        }

        public void UpdateLocation(Vector2 location)
        {
            Location = location;
        }
     

        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, Location);
            saveData.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(collider.Center);
        }

    

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Location, 0, Color.White);
        }

        public void Update()
        {
            
            state.Update();
            sprite.Update();
            
        }

  

        public void TakeDamage(Direction dir, int amount)
        {
            HP = Math.Max(HP - amount, HPAmount.Zero);
            if (HP <= HPAmount.Zero) Die();
        }

        public void ObstacleCollision(Collision collision)
        {
            state.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            Die();
        }
    }
}
