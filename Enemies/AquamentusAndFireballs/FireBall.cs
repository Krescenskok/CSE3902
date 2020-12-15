using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Enemies.EnemyHelperClasses;
using Sprint5.EnemyAndNPC.AquamentusAndFireballs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class FireBall : IEnemy
    {
        private ISprite sprite;
        private FireBallSprite fireBallSprite;
        private FireBallState fireBallState;
        private Vector2 fireBallPos;
        private LinkPlayer link;
        private FireballCollider fireBallCollider;

        public Vector2 Location { get => fireBallPos; }

        public IEnemyState State { get => fireBallState; }

        public List<ICollider> Colliders { get => new List<ICollider> { fireBallCollider }; }
        public int HP { get; private set; }

        public FireBall(Aquamentus aquamentus, Vector2 initialPos, Vector2 targetPos, int attackStrength, LinkPlayer link)
        {
            fireBallState = new FireBallState(this,aquamentus, initialPos, targetPos);
            sprite = EnemySpriteFactory.Instance.CreateFireBall();
            fireBallSprite = (FireBallSprite)sprite;
            this.link = link;
            fireBallCollider = new FireballCollider(fireBallSprite.GetRectangle(initialPos),this,attackStrength, "fireball", this.link);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, fireBallPos, 0, Color.White);
        }

        public void ChangePos(Vector2 newPos)
        {
            fireBallPos = newPos;
        }


        public void SetSprite(EnemySprite sprite)
        {
            //wont use
        }

        public void Spawn()
        {
            //wont use
        }

        public void TakeDamage()
        {
            //FireBall wont TakeDamage
        }

        public void Update()
        {
            fireBallState.Update();
           
        }


        public void ObstacleCollision(Collision collision)
        {
            State.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            //not affected
        }

        public void TakeDamage(Direction dir, int amount)
        {
            //do nothing
        }
    }
}
