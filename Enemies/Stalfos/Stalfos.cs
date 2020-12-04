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

        private XElement saveInfo;
        public IEnemyState state;

        private EnemySprite sprite;
        private StalfosWalkingSprite stalfosSprite;
        private EnemyCollider collider;

        public int HP { get; private set; } = HPAmount.EnemyLevel2;
        private const float barSize = 1.5f;

        private Point spriteSize;
        private Rectangle rect;

        public Vector2 Location { get; set; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }


        public Stalfos(Game game, Vector2 location, XElement xml)
        {
            Location = location;

            state = new EnemySpawnState(this, game);
            saveInfo = xml;
            HP = DifficultyMultiplier.Instance.DetermineEnemyHP(HP);
        }

        public void Spawn()
        {
            state = new StalfosWalkingState(this, Location);
            stalfosSprite = (StalfosWalkingSprite)sprite;

            spriteSize = stalfosSprite.GetRectangle().Size;
            rect = new Rectangle(Location.ToPoint(), spriteSize);
            rect = HitboxAdjuster.Instance.AdjustHitbox(rect, .5f);
            collider = new EnemyCollider(rect, this, HPAmount.HalfHeart, "Stalfos");

            HPBarDrawer.AddBar(new EnemyHealthBar(this, rect, barSize));
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, Location);
            saveInfo.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(collider.Center);
        }

        public void TakeDamage(Direction dir, int amount)
        {
            if(state is StalfosWalkingState)
            {

                HP = Math.Max(HP - amount, 0);

                if (HP == HPAmount.Zero)
                {
                    Die();
                }
                else
                {
                    bool stunned = (state as StalfosWalkingState).permaStun;
                    state = new StalfosDamagedState(dir, this, Location, stunned);
                }

            }

        }

        public void ObstacleCollision(Collision col)
        {
            state.MoveAwayFromCollision(col);
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            sprite.Draw(spriteBatch, Location, 0, Color.White);

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

        public void Stun()
        {
            state.Stun(false);
        }
    }
}
