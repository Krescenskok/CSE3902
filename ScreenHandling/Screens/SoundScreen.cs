using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;
using Sprint5.ScreenHandling.ScreenSprites;
using Microsoft.Xna.Framework.Content;

namespace Sprint5.ScreenHandling
{
    public class SoundScreen : IScreen
    {
        private int drawBounds = 0;
        public MenuOption Background { get; set; }

        public List<MenuOption> Options { get; set; }
        public List<MenuOption> DrawList { get; set; }


        public List<MenuOption> Volume { get; set; }

        public List<MenuOption> Sprites { get; set; }

        private int SelectedIndex = 0;
        private int SongIndex = 0;
        private int VolumeIndex = 0;
        private String SongOrVolume = "";

        private Game1 Game;
        public List<MenuOption> Songs { get; set; }
        public IDictionary<ScreenName, String> SongFile { get; set; }

        public SoundScreen()
        {
            Options = new List<MenuOption>();
            DrawList = new List<MenuOption>();
            Background = new MenuOption(StateId.Sound, ScreenName.SoundBG);

            Options.Add(new MenuOption(StateId.Options, ScreenName.BackSelect));

            Options.Add(new MenuOption(StateId.Sound, ScreenName.SoundTrackSelect));

            Options.Add(new MenuOption(StateId.Sound, ScreenName.VolumeSelect));
            Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume0));
            Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume20));
            Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume40));
            Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume60));
            Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume80));
            Volume.Add(new MenuOption(StateId.Sound, ScreenName.Volume100));

            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.SelectLeft));
            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.SelectRight));
            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.SoundSelectNone));

            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.BackEsc));
            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.BackB));

            DrawList.Add(Background);
            ToggleOption(Options[0]);
        }

        public void Draw(Game1 game, GameTime gameTime)
        {

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

            game.Spritebatch.Begin();

            game.GraphicsDevice.Viewport = game.Camera.EntireView;

            foreach (MenuOption option in DrawList)
            {
                ScreenSprite currentSprite = ScreenSpriteMap.Instance.GetSprite(option.Name);

                game.Spritebatch.Draw(currentSprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.EntireArea.Width, game.Camera.EntireArea.Height), Color.White);
            }


            game.Spritebatch.End();
        }

        public void ToggleOption(MenuOption option)
        {
            if (DrawList.Contains(option))
            {
                DrawList.Remove(option);
            }
            else
            {
                DrawList.Add(option);
            }
        }
        public void ClearDrawList()
        {
            DrawList.Clear();
            DrawList.Add(Background);
        }

        public void Navigate(string action)
        {
            if (action == "Up")
            {
                if (SelectedIndex != 0)
                {
                    ToggleOption(Options[SelectedIndex]);
                    SelectedIndex--;
                    ToggleOption(Options[SelectedIndex]);
                }
            }
            else if (action == "Down")
            {
                if (SelectedIndex != (Options.Count - 1))
                {
                    ToggleOption(Options[SelectedIndex]);
                    SelectedIndex++;
                    ToggleOption(Options[SelectedIndex]);
                }
            }
            else if (action == "Left")
            {
                if (SelectedIndex != (Options.Count - 1))
                {
                    if (SongOrVolume == "Volume")
                    {
                        if (SongIndex > 0)
                        {
                            ToggleOption(Volume[VolumeIndex]);
                            VolumeIndex--;
                            ToggleOption(Volume[VolumeIndex]);
                            Sounds.Instance.VolumeDown();
                        }
                    } else if (SongOrVolume == "Song")
                    {
                        ToggleOption(Songs[SongIndex]);
                        SongIndex--;
                        if (SongIndex < 0)
                        {
                            SongIndex = Songs.Count - 1;
                        }
                        ToggleOption(Sprites[0]);
                        ToggleOption(Songs[SongIndex]);
                        Sounds.Instance.ChangeBGM(SongFile[Songs[SongIndex].Name]);
                    }
                }
            }
            else if (action == "Right")
            {
                if (SelectedIndex != (Options.Count - 1))
                {
                    if (SongOrVolume == "Volume")
                    {
                        if (SongIndex <= (Songs.Count - 1))
                        {
                            ToggleOption(Volume[VolumeIndex]);
                            VolumeIndex++;
                            ToggleOption(Volume[VolumeIndex]);
                            Sounds.Instance.VolumeUp();
                        }
                    }
                    else if (SongOrVolume == "Song")
                    {
                        ToggleOption(Songs[SongIndex]);
                        SongIndex++;
                        if (SongIndex > (Songs.Count - 1))
                        {
                            SongIndex = 0;
                        }
                        ToggleOption(Sprites[1]);
                        ToggleOption(Songs[SongIndex]);
                        Sounds.Instance.ChangeBGM(SongFile[Songs[SongIndex].Name]);
                    }
                }
            }
        }

        public void Back()
        {
            Game.State.Swap(Game.State.Previous.Id);
        }

        public void Select()
        {
            ScreenName currentName = Options[SelectedIndex].Name;
            StateId currentId = Options[SelectedIndex].Id;
            if (Game.State.Current.Id != currentId)
            {
                Game.State.Swap(currentId);
            }
            else if (currentName == ScreenName.VolumeSelect)
            {
                ClearDrawList();
                SongOrVolume = "Volume";
                ToggleOption(Volume[5]);
            }
            else if (currentName == ScreenName.SoundTrackSelect)
            {
                ClearDrawList();
                SongOrVolume = "Song";
                ToggleOption(Songs[0]);
                ToggleOption(Sprites[2]);
            }

        }

    }
}