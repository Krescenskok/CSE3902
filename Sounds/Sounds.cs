using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Sprint4
{
    public class Sounds
    {
        public static Sounds Instance { get; } = new Sounds();
        private Sounds()
        {

        }

        private Dictionary<string, SoundEffectInstance> sounds;

        private SoundEffectInstance lowHealth;
        private SoundEffectInstance BGM;

        private Game game;
        public bool Muted { get; private set; }


        private KeyboardState state;
        private KeyboardState prevState;

        public void LoadSounds(Game game)
        {
            this.game = game;
            sounds = SoundContent.LoadContent(game.Content, "Sounds");

            

            lowHealth = Get("LowHealth");
            lowHealth.IsLooped = true;

            BGM = Get("DungeonTheme");
            BGM.IsLooped = true;
            BGM.Play();
        }

        public void Update()
        {
            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.M) && !prevState.IsKeyDown(Keys.M)) ToggleMute();

            prevState = state;
        }

        private void ToggleMute()
        {
            if (Muted) UnMute();
            else Mute();
        }

        private void Mute()
        {
            
            
            foreach(KeyValuePair<string,SoundEffectInstance> pair in sounds)
            {
                pair.Value.Volume = 0;
            }
            Muted = true;
            
            
        }

        private void UnMute()
        {
            foreach (KeyValuePair<string, SoundEffectInstance> pair in sounds)
            {
                pair.Value.Volume = 1;
            }
            Muted = false;
        }


        private SoundEffectInstance Get(string name)
        {
            if (sounds.ContainsKey(name)) return sounds[name];
            return null;
        }

     

        #region //sound effects

        public void PlaySoundEffect(string name)
        {
            SoundEffectInstance sound = Get(name);

            if (sound != null) sound.Play();
        }
        public void PlayEnemyHit()
        {
            SoundEffectInstance sound = Get("EnemyHit");
            
            if (sound != null) sound.Play();
        }

        public void PlayEnemyDie()
        {
            SoundEffectInstance sound = Get("EnemyDie");
            if (sound != null) sound.Play();
        }

        public void PlayLinkHurt()
        {
            SoundEffectInstance sound = Get("LinkHurt");
            if (sound != null) sound.Play();
        }

        public void PlayDodongoRoar()
        {
            SoundEffectInstance sound = Get("DodongoRoar");
            if (sound != null) sound.Play();
        }

        public void PlayAquamentusRoar()
        {
            SoundEffectInstance sound = Get("AquamentusRoar");
            if (sound != null) sound.Play();
        }

        public void PlayBossScream()
        {
            SoundEffectInstance sound = Get("BossScream3");
            if (sound != null) sound.Play();
        }
        #endregion


        #region // looped sounds
        public void StartLowHealthLoop()
        {
            lowHealth.Play();
        }

        public void StopLowHealthLoop()
        {
            lowHealth.Stop();
        }
        #endregion


    }
}
