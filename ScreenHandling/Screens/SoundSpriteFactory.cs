using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class SoundSpriteFactory
    {

        private static readonly SoundSpriteFactory instance = new SoundSpriteFactory();
        public static SoundSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }
        private SoundSpriteFactory()
        {

        }

        public void PopulateSoundLists(SoundScreen screen)
        {
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.ZeldaRemix));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.ZeldaOriginal));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.Tron));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.ImOnaBoat));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.iCarly));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.HoldingOut));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.Fireflies));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.Doom));
            screen.Songs.Add(new MenuOption(StateId.Sound, ScreenName.Allstar));
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "DungeonTheme");
            screen.SongFile.Add(ScreenName.ZeldaRemix, "ZeldaRemixTheme");
            screen.SongFile.Add(ScreenName.HoldingOut, "Shrek2Theme");
            screen.SongFile.Add(ScreenName.Fireflies, "FirefliesTheme");
            screen.SongFile.Add(ScreenName.September, "SeptemberTheme");
            screen.SongFile.Add(ScreenName.iCarly, "iCarlyTheme");
            screen.SongFile.Add(ScreenName.Tron, "TronTheme");
            screen.SongFile.Add(ScreenName.Doom, "DoomTheme");
            screen.SongFile.Add(ScreenName.Allstar, "AllStarTheme");
            screen.SongFile.Add(ScreenName.ImOnaBoat, "OnABoatTheme");
            screen.Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume0));
            screen.Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume20));
            screen.Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume40));
            screen.Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume60));
            screen.Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume80));
            screen.Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume100));

        }
    }
}
