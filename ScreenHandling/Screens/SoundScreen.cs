using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;
using Sprint5.ScreenHandling.ScreenSprites;
using Microsoft.Xna.Framework.Content;

namespace Sprint5
{
    public class SoundScreen : IScreen
    {
        private int drawBounds = 0;
        private List<MenuOption> Options = new List<MenuOption>();
        public List<MenuOption> DrawList { get; set; } = new List<MenuOption>();
        public MenuOption Background { get; set; } = new MenuOption(StateId.Controls, ScreenName.SoundBG);
        public List<MenuOption> Sprites { get; set; } = new List<MenuOption>();
        private Game1 Game;
        private int SelectedIndex = 0;
        public List<MenuOption> Volume { get; set; } = new List<MenuOption>();
        private int SongIndex = 0;
        private int VolumeIndex = 5;
        private int timer = 10;
        private String SongOrVolume = "";
        public List<MenuOption> Songs { get; set; } = new List<MenuOption>();
        public IDictionary<ScreenName, String> SongFile { get; set; } = new Dictionary<ScreenName, String>();
        public SoundScreen()
        {

            Options.Add(new MenuOption(StateId.Sound, ScreenName.SoundTrackSelect));

            Options.Add(new MenuOption(StateId.Sound, ScreenName.VolumeSelect));
            Options.Add(new MenuOption(StateId.Options, ScreenName.BackSelect));

            SoundSpriteFactory.Instance.PopulateSoundLists(this);

            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.SelectLeft));
            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.SelectRight));
            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.SoundSelectNone));

            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.BackEsc));
            Sprites.Add(new MenuOption(StateId.Sound, ScreenName.BackB));

            DrawList.Add(Background);
            DrawList.Add(new MenuOption(StateId.Sound, ScreenName.Back));
            ToggleOption(Options[0]);
        }

        public void Draw(Game1 game, GameTime gameTime)
        {
            Game = game;


            game.Spritebatch.Begin();

            game.GraphicsDevice.Viewport = game.Camera.EntireView;

            foreach (MenuOption option in DrawList)
            {
                ScreenSprite currentSprite = ScreenSpriteMap.Instance.GetSprite(option.Name);

                game.Spritebatch.Draw(currentSprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.EntireArea.Width, game.Camera.EntireArea.Height), Color.White);
            }


            game.Spritebatch.End();
            if (DrawList.Contains(Sprites[0]) || DrawList.Contains(Sprites[1]))
            {
                timer--;
                if (timer == 0)
                {
                    DrawList.Remove(Sprites[0]);
                    DrawList.Remove(Sprites[1]);
                    timer = 10;
                }
            }

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
            DrawList.Add(new MenuOption(StateId.Sound, ScreenName.Back));
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
                
                    if (SongOrVolume == "Volume")
                    {
                        if (VolumeIndex > 0)
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
            else if (action == "Right")
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

        public void Back()
        {
            Game.State.BackwardSwap();
        }

        public void Select()
        {
            ScreenName currentName = Options[SelectedIndex].Name;
            StateId currentId = Options[SelectedIndex].Id;

            if (currentName == ScreenName.VolumeSelect && SongOrVolume != "Volume")
            {
                ClearDrawList();
                SongOrVolume = "Volume";
                ToggleOption(Options[1]);
                ToggleOption(Volume[5]);
            }
            else if (currentName == ScreenName.SoundTrackSelect && SongOrVolume != "Song")
            {
                ClearDrawList();
                SongOrVolume = "Song";
                ToggleOption(Options[0]);
                ToggleOption(Songs[0]);
                ToggleOption(Sprites[2]);
            } else
            {
                Game.State.BackwardSwap();
            }

        }

    }
}