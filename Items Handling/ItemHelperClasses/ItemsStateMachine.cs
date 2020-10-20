using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sprint2
{
   /* public enum Item
    {
        Compass, Rupee, Boomerang, Map, Letter, Key, FancyKey, HeartContainer, TriforcePiece, MagicalBoomerang, Heart, HalfHeart, EmptyHeart,
        MagicBook, Clock, Fairy, RedRing, BlueRing, RedPotion, BluePotion, WoodenSword, SilverSword, Wand, Raft, Arrow, SilverArrow, RedCandle,
        BlueCandle, Bomb, FancySword, Shield, Meat, Bow, Bracelet, Recorder
    };*/
    public enum Item
    {
        Bomb, Arrow, SilverArrow, HalfHeart, Rupee, Boomerang, TriforcePiece, MagicalBoomerang, Heart, HeartContainer, EmptyHeart,
        Fairy
    };
    public class ItemsStateMachine
    {
        private IDictionary<Item, IItems> itemToState = new Dictionary<Item, IItems>();
        private IItems item;
        private LinkedList<Item> itemList;
        private LinkedListNode<Item> current;
        private Vector2 location = new Vector2(100, 50);

        public ItemsStateMachine()
        {
            itemToState.Add(Item.Compass, new Compass(ItemsFactory.Instance.CreateCompassSprite(), location));
            itemToState.Add(Item.Rupee, new Rupee(ItemsFactory.Instance.CreateRupeeSprite(), location));
            itemToState.Add(Item.Boomerang, new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), location, "Down"));
            itemToState.Add(Item.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), location));
            itemToState.Add(Item.Clock, new Clock(ItemsFactory.Instance.CreateClockSprite(), location));
            itemToState.Add(Item.EmptyHeart, new EmptyHeart(ItemsFactory.Instance.CreateEmptyHeartSprite(), location));
            itemToState.Add(Item.Fairy, new Fairy(ItemsFactory.Instance.CreateFairySprite(), location));
            itemToState.Add(Item.FancyKey, new FancyKey(ItemsFactory.Instance.CreateFancyKeySprite(), location));
            itemToState.Add(Item.HalfHeart, new HalfHeart(ItemsFactory.Instance.CreateHalfHeartSprite(), location));
            itemToState.Add(Item.Heart, new Heart(ItemsFactory.Instance.CreateHeartSprite(), location));
            itemToState.Add(Item.HeartContainer, new HeartContainer(ItemsFactory.Instance.CreateHeartContainerSprite(), location));
            itemToState.Add(Item.Key, new Key(ItemsFactory.Instance.CreateKeySprite(), location));
            itemToState.Add(Item.MagicalBoomerang, new MagicalBoomerang(ItemsFactory.Instance.CreateMagicalBoomerangSprite(), location, "Down"));
            itemToState.Add(Item.Map, new Map(ItemsFactory.Instance.CreateMapSprite(), location));
            itemToState.Add(Item.TriforcePiece, new TriforcePiece(ItemsFactory.Instance.CreateTriforcePieceSprite(), location));
            itemToState.Add(Item.Arrow, new Arrow(ItemsFactory.Instance.CreateArrowSprite(), location, "Down"));
            itemToState.Add(Item.BlueCandle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), location));
            itemToState.Add(Item.BluePotion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), location)); ;
            itemToState.Add(Item.BlueRing, new BlueRing(ItemsFactory.Instance.CreateBlueRingSprite(), location));
            itemToState.Add(Item.Bomb, new  Bomb(ItemsFactory.Instance.CreateBombSprite(), location));
            itemToState.Add(Item.Bracelet, new Bracelet(ItemsFactory.Instance.CreateBraceletSprite(), location));
            itemToState.Add(Item.FancySword, new FancySword(ItemsFactory.Instance.CreateFancySwordSprite(), location));
            itemToState.Add(Item.Letter, new Letter(ItemsFactory.Instance.CreateLetterSprite(), location));
            itemToState.Add(Item.MagicBook, new MagicBook(ItemsFactory.Instance.CreateMagicBookoSprite(), location));
            itemToState.Add(Item.Meat, new Meat(ItemsFactory.Instance.CreateMeatSprite(), location));
            itemToState.Add(Item.Raft, new Raft(ItemsFactory.Instance.CreateRaftSprite(), location));
            itemToState.Add(Item.Recorder, new Recorder(ItemsFactory.Instance.CreateRecorderSprite(), location));
            itemToState.Add(Item.RedCandle, new RedCandle(ItemsFactory.Instance.CreateRedCandleSprite(), location));
            itemToState.Add(Item.RedPotion, new RedPotion(ItemsFactory.Instance.CreateRedPotionSprite(), location));
            itemToState.Add(Item.RedRing, new RedRing(ItemsFactory.Instance.CreateRedRingSprite(), location));
            itemToState.Add(Item.Shield, new Shield(ItemsFactory.Instance.CreateShieldSprite(), location));
            itemToState.Add(Item.SilverArrow, new SilverArrow(ItemsFactory.Instance.CreateSilverArrowSprite(), location, "Down"));
            itemToState.Add(Item.SilverSword, new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), location));
            itemToState.Add(Item.Wand, new Wand(ItemsFactory.Instance.CreateWandSprite(), location));
            itemToState.Add(Item.WoodenSword, new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), location));

            var itemArray = Enum.GetValues(typeof(Item)).Cast<Item>().ToArray();

            itemList = new LinkedList<Item>(itemArray);

            current = itemList.First;
            item = itemToState[current.Value];
        }


        public void Update()
        {
            item = itemToState[current.Value];
            item.Update();
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
            item.Draw(spriteBatch);
        }
        public void Reset()
        {
            current = itemList.First;
            item = itemToState[current.Value];
            
        }

    }
}
