﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3;
using Sprint3.Enemies;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Sprint3
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
        private RopePlayerFinderCollider finderCollider;
        private string currentDirection = "right";

        private int HP = HPAmount.EnemyLevel1;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }

        public Rope(Game game, Vector2 location, XElement xml)
        {
            this.location = location;
            this.game = game;
            state = new EnemySpawnState(this,game);

            collider = new EnemyCollider();
            finderCollider = new RopePlayerFinderCollider(this);

            saveInfo = xml;

            
        }

        public void Spawn()
        {
            state = new RopeMoveState(this, location, game);
            rp = (RopeMoveSprite)sprite;
            collider = new EnemyCollider(rp.GetRectangle(), state, HPAmount.HalfHeart);
            finderCollider = new RopePlayerFinderCollider(rp.GetRectangle(), this, game);
        }

        public void Update()
        {
            state.Update();
            collider.Update(this);
            finderCollider.Update(currentDirection);
           
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

        public void UpdateDirection(string dir)
        {
            currentDirection = dir;
        }
    }
}
