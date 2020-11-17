using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Monster that throws boomerangs</para>
    /// </summary>
    public class Goriya : IEnemy
    {

        private Vector2 location;
       

        public IEnemyState state;
        private ISprite sprite;
        private GoriyaWalkSprite gorSprite;
        

        private GoriyaBoomerang boomy;

        private Game game;


        private EnemyCollider collider;

        private int HP = HPAmount.EnemyLevel3;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }

        private XElement saveData;

        private Point spriteSize;
        private Rectangle rect;

        public void SetSprite(ISprite sprite)
        {

            this.sprite = sprite;

        }

        public void SetBoomerang(GoriyaBoomerang boomerang)
        {
            boomy = boomerang;
            
        }

        public GoriyaBoomerang GetBoomerang()
        {
            return boomy;
        }

      

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
            
        }


        public Goriya(Game game, Vector2 location, XElement xml)
        {
            this.game = game;
            this.location = location;
            state = new EnemySpawnState(this,game);
            
            collider = new EnemyCollider();
            saveData = xml;
        }
        public void Spawn()
        {
            state = new GoriyaMoveState(this, location);
            gorSprite = (GoriyaWalkSprite)sprite;

            spriteSize = gorSprite.GetRectangle().Size;
            rect = new Rectangle(location.ToPoint(), spriteSize);

            collider = new EnemyCollider(HitboxAdjuster.Instance.AdjustHitbox(rect, .6f), this, HPAmount.HalfHeart, "Goriya");

        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this,location);
            saveData.SetElementValue("Alive", "false");

            RoomItems.Instance.DropRandom(location);
            if (RoomEnemies.Instance.allDead) RoomItems.Instance.DropItem("Boomerang", Location);
        }

    

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location, 0, Color.White);
            
            if (boomy != null) boomy.Draw(spriteBatch);
        }

        public void Update()
        {
            state.Update();
            
            if (boomy != null) boomy.Update();
        }

        public EnemyCollider GetCollider()
        {
            return collider;
        }

        public void TakeDamage(Direction dir, int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
            else state = new GoriyaDamagedState(dir, this, location, 2);
        }

        public void ObstacleCollision(Collision collision)
        {
            state.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            state.Stun();
        }

        public void Attack()
        {
            state.Attack();
        }
    }
}
