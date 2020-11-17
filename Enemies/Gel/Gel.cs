using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Small gelatinous creature that moves randomly between square spaces on the map.</para>
    /// </summary>
    public class Gel : IEnemy
    {
        private Game game;
        private Vector2 location;
        public IEnemyState state;
        private ISprite sprite;
        private GelMoveSprite gSprite;

        private EnemyCollider innerCollider;
        private GelBlockCollider outsideCollider;
        private int HP = HPAmount.EnemyLevel1;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }

        public List<ICollider> Colliders { get => new List<ICollider> { innerCollider,outsideCollider }; }

        private XElement saveData;

        public Gel(Game game, Vector2 location, XElement xlm)
        {
            this.location = location;
            this.game = game;
            state = new EnemySpawnState(this, game);
            innerCollider = new EnemyCollider();
            outsideCollider = new GelBlockCollider();

            saveData = xlm;
        }

        public void Spawn()
        {
            state = new GelMoveState(this, location, game);
            gSprite = (GelMoveSprite)sprite;
            innerCollider = new EnemyCollider(gSprite.GetRectangle(), this, HPAmount.HalfHeart, "gel");
            
            outsideCollider = new GelBlockCollider(gSprite.GetRectangle2(), (GelMoveState)state, this);
            
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }


        public void Update()
        {
            state.Update();
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
            saveData.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(location);
        }

        public void Draw(SpriteBatch batch)
        {
           
            sprite.Draw(batch, location, 0, Color.White);
            
        }

        public void ObstacleCollision(Collision col)
        {
            state.MoveAwayFromCollision(col);
        }

        public EnemyCollider GetCollider()
        {
            return innerCollider;

        }

        public void TakeDamage(Direction dir, int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
        }

        public void Stun()
        {
            state.Stun();
        }

        public void Attack()
        {
            //does nothing
        }
    }
}
