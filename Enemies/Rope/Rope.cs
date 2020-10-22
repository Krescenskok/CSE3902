using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final;
using Sprint2Final.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Sprint2Final
{
    public class Rope : IEnemy
    {

        private XElement saveInfo;

        private Game game;

        private ISprite sprite;
        private RopeMoveSprite rp;
        private IEnemyState state;
        private Vector2 location;
       
        private EnemyCollider collider;

        private const int attack = 5;
        private int HP = 10;

        public Vector2 Location => throw new NotImplementedException();

        public IEnemyState State => throw new NotImplementedException();

        public Rope(Game game, Vector2 location, XElement xml)
        {
            this.location = location;
            this.game = game;
            state = new EnemySpawnState(this,game);

            collider = new EnemyCollider();

            saveInfo = xml;

            
        }

        public void Spawn()
        {
            state = new RopeMoveState(this, location, game);
            rp = (RopeMoveSprite)sprite;
            collider = new EnemyCollider(rp.GetRectangle(), state, attack);
           
        }

        public void Update()
        {
            state.Update();
            collider.Update(this);

           
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
            
        }

        public void Draw(SpriteBatch batch)
        {

            sprite.Draw(batch, location, 0, Color.White);

            
        }

        public void SubtractHP(int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();

        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this,location);
            
            saveInfo.SetElementValue("Alive", "false");
            
        }

        public EnemyCollider GetCollider()
        {
            return collider;
        }
    }
}
