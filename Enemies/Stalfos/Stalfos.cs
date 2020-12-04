using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using Sprint5.DifficultyHandling;

namespace Sprint5
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
        private EnemySprite sprite;
        private StalfosWalkingSprite stalfosSprite;
        private EnemyCollider collider;

        public int HP { get; private set; } = HPAmount.EnemyLevel2;

        private Point spriteSize;
        private Rectangle rect;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }

        public EnemyHealthBar HP_BAR { get; private set; }
        private const float barSize = 1.5f;

        private Game game;

        public Stalfos(Game game, Vector2 location, XElement xml)
        {
            
            this.location = location;
            this.game = game;

            state = new EnemySpawnState(this, game);
            saveInfo = xml;
            HP = DifficultyMultiplier.Instance.DetermineEnemyHP(HP);

            
        }

        public void Spawn()
        {
            state = new StalfosWalkingState(this, location);
            stalfosSprite = (StalfosWalkingSprite)sprite;

            spriteSize = stalfosSprite.GetRectangle().Size;
            rect = new Rectangle(this.location.ToPoint(), spriteSize);
            rect = HitboxAdjuster.Instance.AdjustHitbox(rect, .5f);
            collider = new EnemyCollider(rect, this, HPAmount.HalfHeart, "Stalfos");

            HP_BAR = new EnemyHealthBar(this, rect,game, HP, barSize);
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
            saveInfo.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(collider.Center);
        }


        
      
        public void TakeDamage(Direction dir, int amount)
        {
            HP -= amount;

            if (HP <= HPAmount.Zero) { Die(); 
            }
            else 
            {
                bool stunned = (state as StalfosWalkingState).permaStun;
                state = new StalfosDamagedState(dir, this, location,stunned);
            }
            
        }

        public void ObstacleCollision(Collision col)
        {
            state.MoveAwayFromCollision(col);
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

        public void SetSprite(ISprite sprite)
        {

            this.sprite = (EnemySprite)sprite;

        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }



        public void Stun()
        {
            state.Stun(false);
        }
    }
}
