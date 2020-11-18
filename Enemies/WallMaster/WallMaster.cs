using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Big hand that lurks behind walls, then comes out to grab Link.</para>
    /// </summary>
    public class WallMaster : IEnemy
    {
        private Vector2 location;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider,playerFinder }; }

        public IEnemyState state;
        private ISprite sprite;
        private WallMasterSprite masterSprite;

        private EnemyCollider collider;
        private HandPlayerFinderCollider playerFinder;

        private int HP = HPAmount.EnemyLevel2;

        private XElement saveData;

        public WallMaster(Game game, Vector2 location, XElement xml)
        {
            this.location = location;

            state = new WallMasterMoveState(location, game, this);
            masterSprite = (WallMasterSprite)sprite;
            collider = new EnemyCollider(masterSprite.GetRectangle(), this, HPAmount.HalfHeart, "WallMaster");
            WallMasterMoveState masterState = (WallMasterMoveState)state;
            playerFinder = new HandPlayerFinderCollider(masterState.TrackingArea(), this, game);

            

            saveData = xml;
        }

        public void Spawn()
        {
           //does not play spawn animation
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }
        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0 , Color.White);
        }

        internal void Attack()
        {
            state.Attack();
        }

        public void Update()
        {
            state.Update();
            
        }
     
        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, Location);
            saveData.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(location);
        }


        public void TakeDamage(Direction dir, int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
        }

        public void ObstacleCollision(Collision collision)
        {
           //does not have obstacles
        }

        public void Stun()
        {
            state.Stun();
        }
    }
}
