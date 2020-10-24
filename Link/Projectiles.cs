using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Items;

namespace Sprint3.Link
{
    public class ProjectilesCommand: ICommand
    {
        private double lastTime;
        LinkPlayer link;
        Vector2 itemLocation;
        Heart silverSword;

        private static ProjectilesCommand instance = new ProjectilesCommand();

        public static ProjectilesCommand Instance
        {
            get
            {
                return instance;
            }
        }

        public LinkPlayer Link { get => link; set => link = value; }

        public ProjectilesCommand()
        {
           
        }

        public void ArrowBow()
        {

        }

        public void SwordBeam()
        {

            System.Diagnostics.Debug.WriteLine("Created beam");
            itemLocation = link.currentLocation;

            silverSword = new Heart(ItemsFactory.Instance.CreateHeartSprite(), itemLocation);

       
        }

        public void DoInit(Game game)
        {
        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            System.Diagnostics.Debug.WriteLine("beam");
            if (silverSword!=null)
            {
                System.Diagnostics.Debug.WriteLine("drawing beam");
                silverSword.Draw(spriteBatch);
            }

        }


        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                if(silverSword!=null)
                {
                    itemLocation.X += 10;
                    silverSword.Location = itemLocation;
                    lastTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
               
            }
        }
    }
}
