using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class BladeTrap : IEnemy
    {

        private Vector2 location;
        public Vector2 Location { get => location; }

        
        private Game game;
        
        public IEnemyState state;
        public IEnemyState State { get => state; }
        private ISprite sprite;
        private BladeTrapSprite bladeSprite;

        private EnemyCollider collider;
        private TrapCollider playerFinderOne;
        private TrapCollider playerFinderTwo;

        private string direction1, direction2;

        public BladeTrap(Game game, Vector2 location, string dir1, string dir2)
        {
            direction1 = dir1;
            direction2 = dir2;

            this.game = game;
            state = new EnemySpawnState(this,game);

            this.location = location;

            collider = new EnemyCollider();
            playerFinderOne = new TrapCollider();
            playerFinderTwo = new TrapCollider();
        }

        public void Spawn()
        {
            state = new BladeTrapRestState(location,this);
            BladeTrapRestState initialState = (BladeTrapRestState)state;
            bladeSprite = (BladeTrapSprite)sprite;
            Rectangle objectSize = bladeSprite.GetRectangle();
            objectSize.Location = location.ToPoint();

            collider = new EnemyCollider(objectSize, state, 5, "trap");


            int horizontalAttackRange = GridGenerator.Instance.GetGridWidth();
            int verticalAttackRange = GridGenerator.Instance.GetGridHeight();

            int attackRange1, attackRange2;
            

            if (direction1.Equals("right") || direction1.Equals("left")) attackRange1 = horizontalAttackRange;
            else attackRange1 = verticalAttackRange;

            if (direction2.Equals("right") || direction2.Equals("left")) attackRange2 = horizontalAttackRange;
            else attackRange2 = verticalAttackRange;

            playerFinderOne = new TrapCollider(objectSize, initialState, direction1, attackRange1);
            playerFinderTwo = new TrapCollider(objectSize, initialState, direction2, attackRange2);
        }
        

        public void Update()
        {
            state.Update();
            collider.Update(this);
            playerFinderOne.Update(this);
            playerFinderTwo.Update(this);
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }


        public EnemyCollider GetCollider()
        {
            return collider;
        }

       
    }
}
