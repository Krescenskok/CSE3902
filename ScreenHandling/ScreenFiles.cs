using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class ScreenFiles
    {
        public IDictionary<ScreenName, String> SpriteFileMap { get; set; }

        private static readonly ScreenFiles instance = new ScreenFiles();
        public static ScreenFiles Instance
        {
            get
            {
                return instance;
            }
        }
        private ScreenFiles()
        {
            SpriteFileMap = new Dictionary<ScreenName, String>();
            SpriteFileMap.Add(ScreenName.Quit, "winquit");
            SpriteFileMap.Add(ScreenName.QuitSelect, "winquitselect");
            SpriteFileMap.Add(ScreenName.BackEsc, "BackEsc");
            SpriteFileMap.Add(ScreenName.BackB, "BackB");
            SpriteFileMap.Add(ScreenName.Back, "back");
            SpriteFileMap.Add(ScreenName.BackSelect, "backselect");

            SpriteFileMap.Add(ScreenName.WinBG, "WinScreen");

            SpriteFileMap.Add(ScreenName.LoseBG, "GameOverScreen");
            SpriteFileMap.Add(ScreenName.WinMainMenu, "winmainmenu");
            SpriteFileMap.Add(ScreenName.WinMainMenuSelect, "winmainmenuSelect");
            SpriteFileMap.Add(ScreenName.WinStats, "winstats");
            SpriteFileMap.Add(ScreenName.WinStatsSelect, "winstatSelect");
            SpriteFileMap.Add(ScreenName.WinCredits, "wincredits");
            SpriteFileMap.Add(ScreenName.WinCreditsSelect, "wincreditsselect");

            SpriteFileMap.Add(ScreenName.MainBG, "newMenu");
            SpriteFileMap.Add(ScreenName.MainNewGame, "MainMenuNewGameSelect");
            SpriteFileMap.Add(ScreenName.MainOptionsSelect, "MainMenuOptionsSelect");
            SpriteFileMap.Add(ScreenName.MainCreditsSelect, "MainMenuCreditsSelect");
            SpriteFileMap.Add(ScreenName.MainQuitSelect, "MainMenuQuitSelect");

            SpriteFileMap.Add(ScreenName.OptionsBG, "OptionsScreen");
            SpriteFileMap.Add(ScreenName.OptionsSoundSelect, "OptionsSoundSelect");
            SpriteFileMap.Add(ScreenName.OptionsControlSelect, "OptionsControlsSelect");
            SpriteFileMap.Add(ScreenName.OptionsKeyBindingsSelect, "OptionsKeybindSelect");
            SpriteFileMap.Add(ScreenName.OptionsFullScreenSelect, "OptionsFullscreenSelect");

            SpriteFileMap.Add(ScreenName.SoundBG, "SoundScreen");
            SpriteFileMap.Add(ScreenName.Allstar, "allstar");
            SpriteFileMap.Add(ScreenName.Doom, "doom");
            SpriteFileMap.Add(ScreenName.Fireflies, "fireflies");
            SpriteFileMap.Add(ScreenName.HoldingOut, "holdingout");
            SpriteFileMap.Add(ScreenName.iCarly, "icarly");
            SpriteFileMap.Add(ScreenName.ImOnaBoat, "imonaboat");
            SpriteFileMap.Add(ScreenName.Tron, "tron");
            SpriteFileMap.Add(ScreenName.ZeldaOriginal, "zeldaoriginal");
            SpriteFileMap.Add(ScreenName.ZeldaRemix, "zeldaremix");

            SpriteFileMap.Add(ScreenName.Volume0, "volume0");
            SpriteFileMap.Add(ScreenName.Volume20, "volume20");
            SpriteFileMap.Add(ScreenName.Volume40, "volume40");
            SpriteFileMap.Add(ScreenName.Volume60, "volume60");
            SpriteFileMap.Add(ScreenName.Volume80, "volume80");
            SpriteFileMap.Add(ScreenName.Volume100, "volumeBarfull");

            SpriteFileMap.Add(ScreenName.SelectLeft, "songSelectLeftSelect");
            SpriteFileMap.Add(ScreenName.SelectRight, "songSelectRightSelect");
            SpriteFileMap.Add(ScreenName.SoundSelectNone, "SONGsELECTnotselected");

            SpriteFileMap.Add(ScreenName.SoundTrackSelect, "soundsoundtrackselect");
            SpriteFileMap.Add(ScreenName.VolumeSelect, "soundvolumeselect");

            SpriteFileMap.Add(ScreenName.StatsBG, "STATSSCREEN");

            SpriteFileMap.Add(ScreenName.ControlsBG, "ControlScreen");
            SpriteFileMap.Add(ScreenName.CreditsBG, "CreditsScreen");

            SpriteFileMap.Add(ScreenName.PauseBG, "PauseScreen");

            SpriteFileMap.Add(ScreenName.PauseResumeSelect, "PauseResumeSelect");
            SpriteFileMap.Add(ScreenName.PauseOptionsSelect, "PauseOptionsSelect");
            SpriteFileMap.Add(ScreenName.PauseMainMenuSelect, "PauseMainMenuSelect");
        }

        public String SpriteFile (ScreenName name)
        {
            return SpriteFileMap[name];
        }

    }
}
