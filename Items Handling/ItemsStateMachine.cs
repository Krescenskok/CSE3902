using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Items.States;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sprint2.Items
{
    public enum Item
    {
        Compass, Rupee, Boomerang, Map, Letter, Key, FancyKey, HeartContainer, TriforcePiece, MagicalBoomerang, Heart, HalfHeart, EmptyHeart,
        MagicBook, Clock, Fairy, RedRing, BlueRing, RedPotion, BluePotion, WoodenSword, SilverSword, Wand, Raft, Arrow, SilverArrow, RedCandle,
        BlueCandle, Bomb, FancySword, Shield, Meat, Bow, Bracelet, Recorder
    };
    public class ItemsStateMachine
    {
        private ISprite itemSprite;

        private IDictionary<Item, IItemsState> itemToState = new Dictionary<Item, IItemsState>();
        private IItemsState currentState;
        private LinkedList<Item> itemList;
        private LinkedListNode<Item> current;


        public ItemsStateMachine()
        {
            itemSprite = SpriteFactory.Instance.CreateItemsSprite();
            itemToState.Add(Item.Compass, new Compass(itemSprite));
            itemToState.Add(Item.Rupee, new Rupee(itemSprite));
            itemToState.Add(Item.Boomerang, new Boomerang(itemSprite));
            itemToState.Add(Item.Bow, new Bow(itemSprite));
            itemToState.Add(Item.Clock, new Clock(itemSprite));
            itemToState.Add(Item.EmptyHeart, new EmptyHeart(itemSprite));
            itemToState.Add(Item.Fairy, new Fairy(itemSprite));
            itemToState.Add(Item.FancyKey, new FancyKey(itemSprite));
            itemToState.Add(Item.HalfHeart, new HalfHeart(itemSprite));
            itemToState.Add(Item.Heart, new Heart(itemSprite));
            itemToState.Add(Item.HeartContainer, new HeartContainer(itemSprite));
            itemToState.Add(Item.Key, new Key(itemSprite));
            itemToState.Add(Item.MagicalBoomerang, new MagicalBoomerang(itemSprite));
            itemToState.Add(Item.Map, new Map(itemSprite));
            itemToState.Add(Item.TriforcePiece, new TriforcePiece(itemSprite));
            itemToState.Add(Item.Arrow, new Arrow(itemSprite));
            itemToState.Add(Item.BlueCandle, new BlueCandle(itemSprite));
            itemToState.Add(Item.BluePotion, new BluePotion(itemSprite)); ;
            itemToState.Add(Item.BlueRing, new BlueRing(itemSprite));
            itemToState.Add(Item.Bomb, new  Bomb(itemSprite));
            itemToState.Add(Item.Bracelet, new Bracelet(itemSprite));
            itemToState.Add(Item.FancySword, new FancySword(itemSprite));
            itemToState.Add(Item.Letter, new Letter(itemSprite));
            itemToState.Add(Item.MagicBook, new MagicBook(itemSprite));
            itemToState.Add(Item.Meat, new Meat(itemSprite));
            itemToState.Add(Item.Raft, new Raft(itemSprite));
            itemToState.Add(Item.Recorder, new Recorder(itemSprite));
            itemToState.Add(Item.RedCandle, new RedCandle(itemSprite));
            itemToState.Add(Item.RedPotion, new RedPotion(itemSprite));
            itemToState.Add(Item.RedRing, new RedRing(itemSprite));
            itemToState.Add(Item.Shield, new Shield(itemSprite));
            itemToState.Add(Item.SilverArrow, new SilverArrow(itemSprite));
            itemToState.Add(Item.SilverSword, new SilverSword(itemSprite));
            itemToState.Add(Item.Wand, new Wand(itemSprite));
            itemToState.Add(Item.WoodenSword, new WoodenSword(itemSprite));

            var itemArray = Enum.GetValues(typeof(Item)).Cast<Item>().ToArray();

            itemList = new LinkedList<Item>(itemArray);

            current = itemList.First;
            
     

            currentState = itemToState[current.Value];
        }


        public void Update()
        {

            currentState = itemToState[current.Value];
            currentState.Update();
        }
        
        public void ChangeItem(bool goingForward)
        {
            if (goingForward)
            {
                if (current.Next != null)
                {
                    current = current.Next;
                }
                else
                {
                    current = itemList.First;
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
                    current = itemList.Last;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            currentState.Draw(spriteBatch, currentState.GetLocation());
        }
        public void Reset()
        {
            current = itemList.First;
            currentState = itemToState[current.Value];
            currentState.Update();
        }

    }
}
