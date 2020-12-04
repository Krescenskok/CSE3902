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
        private Vector2 location;

        public IEnemyState state;
        private EnemySprite sprite;

        private Game game;
        

        private EnemyCollider collider;

        public int HP { get; private set; } = HPAmount.EnemyLevel1;
        public EnemyHealthBar HP_BAR { get; private set; }
        private const float barSize = 1.5f;


        private Point spriteSize;
        private Rectangle rect;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }

        private XElement saveData;

        public Keese(Game game, Vector2 location, XElement xml)
        {
            this.game = game;
            this.location = location;
            state = new EnemySpawnState(this, game);

            collider = new EnemyCollider();
            HP = DifficultyMultiplier.Instance.DetermineEnemyHP(HP);


            saveData = xml;
        }

        public void Spawn()
        {
            
            state = new KeeseMoveState(this, location);
            KeeseMoveSprite kSprite = (KeeseMoveSprite)sprite;
            spriteSize = kSprite.GetRectangle().Size;
            rect = new Rectangle(location.ToPoint(), spriteSize);
            rect = HitboxAdjuster.Instance.AdjustHitbox(rect, .6f);

            collider = new EnemyCollider(HitboxAdjuster.Instance.AdjustHitbox(rect, .5f), this, HPAmount.HalfHeart, "Keese");

            HP_BAR = new EnemyHealthBar(this, rect, game, HP, barSize);
        }
        public void SetSprite(ISprite sprite)
        {
            this.sprite = (EnemySprite)sprite;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }
     

        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
            saveData.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(collider.Center);
        }

    

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location, 0, Color.White);
        }

        public void Update()
        {
            
            state.Update();
            sprite.Update();
            
        }

  

        public void TakeDamage(Direction dir, int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
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
