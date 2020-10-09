using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Blocks;

namespace Sprint2.Blocks
{
    public interface IBlockState
    {
        void Update();
        void Draw(SpriteBatch spriteBatch, Vector2 location);

        Vector2 GetLocation();

       // Vector2 Compass();


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
