using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;

namespace Sprint3
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
            innerCollider = new EnemyCollider(gSprite.GetRectangle(), state, HPAmount.HalfHeart, "gel");
            
            outsideCollider = new GelBlockCollider(gSprite.GetRectangle2(), (GelMoveState)state);
            
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
            innerCollider.Update(this);

            if(gSprite != null)outsideCollider.Update(gSprite.OuterColliderLocation(location));
           
        }

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
        }

        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
            CollisionHandler.Instance.RemoveCollider(outsideCollider);
            saveData.SetElementValue("Alive", "false");
        }

        public void Draw(SpriteBatch batch)
        {
           
            sprite.Draw(batch, location, 0, Color.White);
            
        }

        

        public EnemyCollider GetCollider()
        {
            return innerCollider;

        }
    }
}
