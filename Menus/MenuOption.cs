using Sprint5.ScreenHandling.ScreenSprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Menus
{
    public class MenuOption
    {
        public StateId Id { get; set; }

        public ScreenName name { get; set; }


        public MenuOption(StateId id, ScreenName name)
        {
            //need to make command for stateswap if id if the menu item is to change menu
            //make command for doing something if its not a swap menu
            //Need a command list that makes commands based on what its name is
            this.Id = id;
            this.Name = name;

        }
    }
}
