using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class LinkItems
    {
        private SpriteBatch SpriteBatch;
        private ItemsStateMachine StateMachine;
        private ItemsStateMachine PrevState;


        
        public LinkItems(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
            StateMachine = new ItemsStateMachine();
            PrevState = StateMachine;
        }


        public void Draw()
        {
            
            StateMachine.Draw(SpriteBatch);
        }

        public void ChangeState(bool goingForward)
        {
            StateMachine.ChangeItem(goingForward);
        }


        public void Update()
        {
            StateMachine.Update();
        }

        public void Reset()
        {
            StateMachine.Reset();
        }
        /*public void Map()
        {

        }

        public void Key()
        {

        }

        public void FancyKey()
        {

        }

        public void HeartContainer()
        {

        }

        public void TriforcePiece()
        {

        }

        public void WoodenBoomerang()
        {

        }

        public void Bow()
        {

        }

        public void FullHeart()
        {

        }

        public void HalfHeart()
        {

        }

        public void EmptyHeart()
        {

        }

        public void Rupee()
        {

        }

        public void Arrow()
        {

        }

        public void Bomb()
        {

        }

        public void Fairy()
        {

        }

        public void Clock()
        {

        }

        public void Meat()
        {

        }

        public void MagicBook()
        {

        }

        public void Recorder()
        {

        }*/
    }
}
