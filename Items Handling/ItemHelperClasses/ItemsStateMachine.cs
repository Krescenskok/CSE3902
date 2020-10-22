using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Items;
using Sprint2.Link;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sprint2
{
    public enum Item
    {
        SwordBeam, Arrow, BluePotion, SilverSword, WandBeam, Boomerang,BlueCandle, BlueRing, Bomb, Bow, Clock, Compass, EmptyHeart, Fairy, HalfHeart, 
        Heart, HeartContainer, Key, Map, Rupee, TriforcePiece, Wand, WoodenSword
    };
    public class ItemsStateMachine
    {
        private IDictionary<Item, IItems> itemToState = new Dictionary<Item, IItems>();
        private IItems item;
        private LinkedList<Item> itemList;
        private LinkedListNode<Item> current;
        private Vector2 location = new Vector2(100, 50);
        private LinkPlayer link;

        public ItemsStateMachine(LinkPlayer link)
        {
            this.link = link;
            itemToState.Add(Item.Compass, new Compass(ItemsFactory.Instance.CreateCompassSprite(), location));
            itemToState.Add(Item.Rupee, new Rupee(ItemsFactory.Instance.CreateRupeeSprite(), location));
            itemToState.Add(Item.Boomerang, new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), location, "Down", link));
            itemToState.Add(Item.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), location));
            itemToState.Add(Item.Clock, new Clock(ItemsFactory.Instance.CreateClockSprite(), location));
            itemToState.Add(Item.EmptyHeart, new EmptyHeart(ItemsFactory.Instance.CreateEmptyHeartSprite(), location));
            itemToState.Add(Item.Fairy, new Fairy(ItemsFactory.Instance.CreateFairySprite(), location));
            itemToState.Add(Item.HalfHeart, new HalfHeart(ItemsFactory.Instance.CreateHalfHeartSprite(), location));
            itemToState.Add(Item.Heart, new Heart(ItemsFactory.Instance.CreateHeartSprite(), location));
            itemToState.Add(Item.HeartContainer, new HeartContainer(ItemsFactory.Instance.CreateHeartContainerSprite(), location));
            itemToState.Add(Item.Key, new Key(ItemsFactory.Instance.CreateKeySprite(), location));
            itemToState.Add(Item.Map, new Map(ItemsFactory.Instance.CreateMapSprite(), location));
            itemToState.Add(Item.TriforcePiece, new TriforcePiece(ItemsFactory.Instance.CreateTriforcePieceSprite(), location));
            itemToState.Add(Item.Arrow, new Arrow(ItemsFactory.Instance.CreateArrowSprite("Down"), location, "Down"));
            itemToState.Add(Item.BlueCandle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), location));
            itemToState.Add(Item.BluePotion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), location)); ;
            itemToState.Add(Item.BlueRing, new BlueRing(ItemsFactory.Instance.CreateBlueRingSprite(), location));
            itemToState.Add(Item.Bomb, new  Bomb(ItemsFactory.Instance.CreateBombSprite(), location));
            itemToState.Add(Item.SilverSword, new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), location));
            itemToState.Add(Item.Wand, new Wand(ItemsFactory.Instance.CreateWandSprite(), location));
            itemToState.Add(Item.WoodenSword, new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), location));
            itemToState.Add(Item.SwordBeam, new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), location, "Right"));
            itemToState.Add(Item.WandBeam, new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite("Right"), location, "Right"));

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
            ResetAllItems();
            current = itemList.First;
            item = itemToState[current.Value];
        }

        public void ResetAllItems()
        {
            itemToState = new Dictionary<Item, IItems>();
            itemToState.Add(Item.Compass, new Compass(ItemsFactory.Instance.CreateCompassSprite(), location));
            itemToState.Add(Item.Rupee, new Rupee(ItemsFactory.Instance.CreateRupeeSprite(), location));
            itemToState.Add(Item.Boomerang, new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), location, "Down", link));
            itemToState.Add(Item.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), location));
            itemToState.Add(Item.Clock, new Clock(ItemsFactory.Instance.CreateClockSprite(), location));
            itemToState.Add(Item.EmptyHeart, new EmptyHeart(ItemsFactory.Instance.CreateEmptyHeartSprite(), location));
            itemToState.Add(Item.Fairy, new Fairy(ItemsFactory.Instance.CreateFairySprite(), location));
            itemToState.Add(Item.HalfHeart, new HalfHeart(ItemsFactory.Instance.CreateHalfHeartSprite(), location));
            itemToState.Add(Item.Heart, new Heart(ItemsFactory.Instance.CreateHeartSprite(), location));
            itemToState.Add(Item.HeartContainer, new HeartContainer(ItemsFactory.Instance.CreateHeartContainerSprite(), location));
            itemToState.Add(Item.Key, new Key(ItemsFactory.Instance.CreateKeySprite(), location));
            itemToState.Add(Item.Map, new Map(ItemsFactory.Instance.CreateMapSprite(), location));
            itemToState.Add(Item.TriforcePiece, new TriforcePiece(ItemsFactory.Instance.CreateTriforcePieceSprite(), location));
            itemToState.Add(Item.Arrow, new Arrow(ItemsFactory.Instance.CreateArrowSprite("Down"), location, "Down"));
            itemToState.Add(Item.BlueCandle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), location));
            itemToState.Add(Item.BluePotion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), location)); ;
            itemToState.Add(Item.BlueRing, new BlueRing(ItemsFactory.Instance.CreateBlueRingSprite(), location));
            itemToState.Add(Item.Bomb, new Bomb(ItemsFactory.Instance.CreateBombSprite(), location));
            itemToState.Add(Item.SilverSword, new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), location));
            itemToState.Add(Item.Wand, new Wand(ItemsFactory.Instance.CreateWandSprite(), location));
            itemToState.Add(Item.WoodenSword, new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), location));
            itemToState.Add(Item.SwordBeam, new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), location, "Right"));
            itemToState.Add(Item.WandBeam, new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite("Right"), location, "Right"));
        }

    }
}
