using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
{
    /// <summary>
    /// <author>JT Thrash</author>
    /// Class for enemy named Stalfos (the skeleton). Must be initialized in LoadContent method in main Game1 class.
    /// </summary>
    public class Stalfos : IEnemy
    {

        XElement saveInfo;
        public IEnemyState state;

        private Vector2 location;
        private ISprite sprite;
        private StalfosWalkingSprite stalfosSprite;
        private EnemyCollider collider;

        private int HP = HPAmount.EnemyLevel2;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }

        public Stalfos(Game game, Vector2 location, XElement xml)
        {
            
            this.location = location;
            state = new EnemySpawnState(this, game);
            collider = new EnemyCollider();

            saveInfo = xml;
        }

        public void Spawn()
        {
            state = new StalfosWalkingState(this, location);
            stalfosSprite = (StalfosWalkingSprite)sprite;
            collider = new EnemyCollider(stalfosSprite.GetRectangle(), state, HPAmount.HalfHeart, "Stalfos");
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
            saveInfo.SetElementValue("Alive", "false");
        }


        
        /// <returns>true when stalfos HP is > 0</returns>
        public bool SubtractHP(int amount)
        {
            HP -= amount;

            
            if (HP <= HPAmount.Zero) { Die(); }

            return HP > HPAmount.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            sprite.Draw(spriteBatch, location, 0, Color.White);
        }

        public void Update()
        {
            state.Update();
            collider.Update(this);
        }

        public void SetSprite(ISprite sprite)
        {

            this.sprite = sprite;

        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public EnemyCollider GetCollider()
        {
            return collider;
        }
    }
}
