using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.EnemyAndNPC.AquamentusAndFireballs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
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
        private EnemyCollider fireBallCollider;

        public Vector2 Location { get => fireBallPos; }

        public IEnemyState State { get => fireBallState; }

        public List<ICollider> Colliders { get => new List<ICollider> { fireBallCollider }; }

        public FireBall(Aquamentus aquamentus, Vector2 initialPos, Vector2 targetPos, int attackStrength)
        {
            fireBallState = new FireBallState(this,aquamentus, initialPos, targetPos);
            sprite = EnemySpriteFactory.Instance.CreateFireBall();
            fireBallSprite = (FireBallSprite)sprite;
            fireBallCollider = new EnemyCollider(fireBallSprite.GetRectangle(initialPos),this,attackStrength, "fireball");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, fireBallPos, 0, Color.White);
        }

        public void ChangePos(Vector2 newPos)
        {
            fireBallPos = newPos;
        }



        public void SetSprite(ISprite sprite)
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

        public void TakeDamage(Direction dir, int amount)
        {
            State.TakeDamage(amount);
        }

        public void ObstacleCollision(Collision collision)
        {
            State.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            State.Stun();
        }
    }
}
