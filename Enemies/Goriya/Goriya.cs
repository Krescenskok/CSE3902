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

        private XElement saveData;

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

        public EnemyCollider Collider()
        {
            return collider;
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
            collider = new EnemyCollider(gorSprite.GetRectangle(), state, HPAmount.HalfHeart, "Goriya");
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this,location);
            saveData.SetElementValue("Alive", "false");
        }

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location, 0, Color.White);
            
            if (boomy != null) boomy.Draw(spriteBatch);
        }

        public void Update()
        {
            state.Update();
            collider.Update(this);
            if (boomy != null) boomy.Update();
        }

        public EnemyCollider GetCollider()
        {
            return collider;
        }
    }
}
