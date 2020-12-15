using Sprint5.ScreenHandling.ScreenSprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class MenuOption
    {
        public StateId Id;

        public ScreenName Name { get; }


        public MenuOption(StateId id, ScreenName name)
        {
            this.Id = id;
            this.Name = name;

        }
    }
}
