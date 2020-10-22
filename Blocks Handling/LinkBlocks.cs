using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final.Blocks;

namespace Sprint2Final.Blocks
{
    public class LinkBlocks
    {
        private SpriteBatch SpriteBatch;
        private BlocksStateMachine StateMachine;
        private BlocksStateMachine PrevState;
        private bool GoingForward;


        
        public LinkBlocks(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
            StateMachine = new BlocksStateMachine();
            PrevState = StateMachine;
        }


        public void Draw()
        {
            
            StateMachine.Draw(SpriteBatch);
        }

        public void ChangeState(bool goingForward)
        {
            StateMachine.ChangeBlock(goingForward);
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
