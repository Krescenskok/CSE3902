using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Sprint3.Blocks;

namespace Sprint3.Blocks
{
    public enum Block
    {
        BirdLeft, BirdRight, BlackTile, BlueTile, Bricks, Column, DarkerBlueTile, Slats, SpottedTile, Stairs
    };
    public class BlocksStateMachine
    {
        private ISprite blockSprite;
       
        private Block currentBlock;

        private IDictionary<Block, IBlockState> blockToState = new Dictionary<Block, IBlockState>();
        private IBlockState currentState;
        private LinkedList<Block> blockList;
        private LinkedListNode<Block> current;


        public BlocksStateMachine()
        {
            blockSprite = SpriteFactory.Instance.CreateBlocksSprite();
            blockToState.Add(Block.BirdLeft, new BirdLeft(blockSprite));
            blockToState.Add(Block.BirdRight, new BirdRight(blockSprite));
            blockToState.Add(Block.BlackTile, new BlackTile(blockSprite));
            blockToState.Add(Block.BlueTile, new BlueTile(blockSprite));
            blockToState.Add(Block.Bricks, new Bricks(blockSprite));
            blockToState.Add(Block.Column, new Column(blockSprite));
            blockToState.Add(Block.DarkerBlueTile, new DarkerBlueTile(blockSprite));
            blockToState.Add(Block.Slats, new Slats(blockSprite));
            blockToState.Add(Block.SpottedTile, new SpottedTile(blockSprite));
            blockToState.Add(Block.Stairs, new Stairs(blockSprite));


            var blockArray = Enum.GetValues(typeof(Block)).Cast<Block>().ToArray();

            blockList = new LinkedList<Block>(blockArray);

            current = blockList.First;
            
    

            currentState = blockToState[current.Value];
        }


        public void Update()
        {

            currentState = blockToState[current.Value];
            currentState.Update();
        }
        
        public void ChangeBlock(bool goingForward)
        {
            if (goingForward)
            {
                if (current.Next != null)
                {
                    current = current.Next;
                }
                else
                {
                    current = blockList.First;
                }
            }

            else
            {
                if (current.Previous != null)
                {
                    current = current.Previous;
                }
                else
                {
                    current = blockList.Last;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            currentState.Draw(spriteBatch, currentState.GetLocation());
        }
        public void Reset()
        {
            current = blockList.First;
            currentState = blockToState[current.Value];
            currentState.Update();
        }

    }
}
