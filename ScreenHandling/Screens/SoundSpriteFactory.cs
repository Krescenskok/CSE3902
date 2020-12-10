using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.ScreenHandling.Screens
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
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "ZeldaRemixTheme");
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "Shrek2Theme");
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "Firefliestheme");
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "SeptemberTheme");
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "iCarlyTheme");
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "TronTheme");
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "DoomTheme");
            screen.SongFile.Add(ScreenName.ZeldaOriginal, "AllStarTheme");

        }
    }
}
