using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml.Linq;
using Sprint5.DifficultyHandling;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Monster that throws boomerangs</para>
    /// </summary>
    public class Goriya : IEnemy
    {
        private XElement saveData;
        
       

        public IEnemyState state;
        private EnemySprite sprite;
        private GoriyaWalkSprite gorSprite;
        
        public GoriyaBoomerang Boomerang { get; set; }

        public int HP { get; private set; } = HPAmount.EnemyLevel3;
        private const float barSize = 1.5f;


        public Vector2 Location { get; set; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }
        private EnemyCollider collider;

       

        private Point spriteSize;
        private Rectangle rect;

        public Goriya(Game game, Vector2 location, XElement xml)
        {
            this.Location = location;
            state = new EnemySpawnState(this,game);
            
            collider = new EnemyCollider();
            saveData = xml;
            HP = DifficultyMultiplier.Instance.DetermineEnemyHP(HP);

        }
        public void Spawn()
        {
            state = new GoriyaMoveState(this, Location);
            gorSprite = (GoriyaWalkSprite)sprite;

            spriteSize = gorSprite.GetRectangle().Size;
            rect = new Rectangle(Location.ToPoint(), spriteSize);
            rect = HitboxAdjuster.Instance.AdjustHitbox(rect, .6f);
            collider = new EnemyCollider(rect, this, HPAmount.HalfHeart, "Goriya");

            HPBarDrawer.AddBar(new EnemyHealthBar(this, rect, barSize));
        }

        public void SetSprite(EnemySprite sprite)
        {
            this.sprite = sprite;
        }


        public void Die()
        {
            if (Boomerang != null) Boomerang.Die();
            RoomEnemies.Instance.Destroy(this,Location);
            saveData.SetElementValue("Alive", "false");

            RoomItems.Instance.DropRandom(collider.Center);
            if (RoomEnemies.Instance.allDead) RoomItems.Instance.DropItem("Boomerang", collider.Center);
        }

    

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Location, 0, Color.White);
            
            if (Boomerang != null) Boomerang.Draw(spriteBatch);

        }

        public void Update()
        {
            state.Update();
            sprite.Update();
            if (Boomerang != null) Boomerang.Update();

        }


        public void TakeDamage(Direction dir, int amount)
        {
            HP = Math.Max(HP - amount, HPAmount.Zero);
            if (HP == HPAmount.Zero) Die();
            else
            {
                bool stunned = (state as GoriyaMoveState).permaStun;
                state = new GoriyaDamagedState(dir, this, Location, 2,stunned);
            }
        }

        public void ObstacleCollision(Collision collision)
        {
            state.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            state.Stun(false);
        }

    }
}
