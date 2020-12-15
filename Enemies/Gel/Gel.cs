using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;
using Sprint5.DifficultyHandling;

namespace Sprint5
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
        public EnemySprite sprite;
        public GelMoveSprite gSprite;

        private EnemyCollider innerCollider;
        private GelBlockCollider outsideCollider;
        
        private Point spriteSize;
        private Rectangle rect;

        public Vector2 Location { get => location; }
        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { innerCollider,outsideCollider }; }

        private XElement saveData;

        public int HP { get; private set; } = HPAmount.EnemyLevel1;
        private const float barSize = 1.5f;

        public Gel(Game game, Vector2 location, XElement xlm)
        {
            this.location = location;
            this.game = game;
            state = new EnemySpawnState(this, game);
            innerCollider = new EnemyCollider();
            outsideCollider = new GelBlockCollider();

            saveData = xlm;
            HP = DifficultyMultiplier.Instance.DetermineEnemyHP(HP);

        }

        public void Spawn()
        {
            state = new GelMoveState(this, location, game);
            gSprite = (GelMoveSprite)sprite;
            
            

            spriteSize = gSprite.GetRectangle().Size;
            rect = new Rectangle(location.ToPoint(), spriteSize);
            innerCollider = new EnemyCollider(HitboxAdjuster.Instance.AdjustHitbox(rect, .3f), this, HPAmount.HalfHeart, "gel");

            outsideCollider = new GelBlockCollider(HitboxAdjuster.Instance.AdjustHitbox(rect, 1.5f), (GelMoveState)state, this);

            rect = HitboxAdjuster.Instance.AdjustHitbox(rect, .7f);
            HPBarDrawer.AddBar( new EnemyHealthBar(this, rect,barSize));
        }

        public void SetSprite(EnemySprite sprite)
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
            sprite.Update();
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
            saveData.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(innerCollider.Center);
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }

        public void ObstacleCollision(Collision col)
        {
            state.MoveAwayFromCollision(col);
        }

        public void TakeDamage(Direction dir, int amount)
        {
            HP = Math.Max(HP - amount, HPAmount.Zero);
            if (HP == HPAmount.Zero) Die();
        }

        public void Stun()
        {
            state.Stun(false);
        }

    }
}
