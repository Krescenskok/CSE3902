using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Sprint5
{
    public class Sounds
    {
        public static Sounds Instance { get; } = new Sounds();
        private Sounds()
        {
            
        }

        private Dictionary<string, SoundEffect> sounds;
        private List<SoundEffectInstance> soundInstances;

        private SoundEffectInstance lowHealth;
        private SoundEffectInstance BGM;

        private Dictionary<string, SoundEffectInstance> loopedSounds;

        private Game game;
        public bool Muted { get; private set; }


        private KeyboardState state;
        private KeyboardState prevState;

        private float currentVolume = 1;

        private string selectedSong;

        public void LoadSounds(Game game)
        {

            
            this.game = game;
            sounds = SoundContent.LoadContent(game.Content, "Sounds");
            soundInstances = new List<SoundEffectInstance>();
            loopedSounds = new Dictionary<string, SoundEffectInstance>();

            if (lowHealth != null) lowHealth.Stop();
            lowHealth = AddLoop("LowHealth");


            if (selectedSong == null) selectedSong = "DungeonTheme";
            if (BGM != null) BGM.Stop();
            BGM = AddLoop(selectedSong);
            BGM.Play();

            
        }

        public void Update()
        {
            state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.M) && !prevState.IsKeyDown(Keys.M)) ToggleMute();
            prevState = state;


        }

        public void RoomChange(int num)
        {
            int i = soundInstances.Count, index = 0;
            while (i > 0)
            {
                if (NonPersistentSound(soundInstances[index])) { soundInstances[index].Stop(); soundInstances.RemoveAt(index--); }
                i--;
                index++;
            }
        
            
        }

        private bool NonPersistentSound(SoundEffectInstance sound)
        {
            return !sound.Equals(BGM) && !sound.Equals(lowHealth);
        }

        public void VolumeUp()
        {
            currentVolume = Math.Clamp(currentVolume + .2f, 0,1);
            if (!Muted) UnMute();
        }
        public void VolumeDown()
        {
            currentVolume = Math.Clamp(currentVolume - .2f, 0, 1);
        }

        private void ToggleMute()
        {
            if (Muted) UnMute();
            else Mute();
        }

        public void TogglePause()
        {
            foreach(SoundEffectInstance sound in soundInstances)
            {
                if (!sound.Equals(BGM) && sound.State is SoundState.Playing) sound.Pause();
                else if (!sound.Equals(BGM) && sound.State is SoundState.Paused) sound.Play();
            }
        }

        private void Mute()
        {


            foreach (SoundEffectInstance instance in soundInstances)
            {
                instance.Volume = 0;
            }
            Muted = true;
            
            
        }

        private void UnMute()
        {
            foreach (SoundEffectInstance instance in soundInstances)
            {
                instance.Volume = currentVolume;
            }
            Muted = false;
        }


        private SoundEffectInstance Get(string name)
        {
            SoundEffectInstance instance = null;
            if (sounds.ContainsKey(name))
            {
                instance = sounds[name].CreateInstance();
                soundInstances.Add(instance);
            } 
            return instance;
        }

        private SoundEffectInstance AddLoop(string name)
        {
            if (sounds.ContainsKey(name)) {
                SoundEffectInstance instance = sounds[name].CreateInstance();
                instance.IsLooped = true;
                soundInstances.Add(instance);
                return instance;
            }
            return null;
        }

        private void Play(SoundEffectInstance instance)
        {
            if (Muted) instance.Volume = 0;
            if(instance != null && !(instance.State is SoundState.Playing)) instance.Play();
        }

        #region //sound effects

        public void Play(string name)
        {
            SoundEffectInstance sound = Get(name);
            Play(sound);
        }

        public void LinkDeath()
        {
            SoundEffectInstance linkDie = Get("LinkDie");
            Play(linkDie);
            BGM.Stop();
            lowHealth.Stop();
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
            if(lowHealth.State != SoundState.Playing) lowHealth.Play();
        }

        public void StopLowHealthLoop()
        {
            if(lowHealth.State == SoundState.Playing) lowHealth.Stop();
        }


        public void AddLoopedSound(string name, string key)
        {
            SoundEffectInstance newLoop = AddLoop(name);

            if (newLoop != null)
            {
                loopedSounds.Add(key, newLoop);
                Play(loopedSounds[key]);
            }
           
        }

        public void StopLoopedSound(string key)
        {
            if (loopedSounds.ContainsKey(key))
            {
                SoundEffectInstance instance = loopedSounds[key];
                instance.Stop();
                soundInstances.Remove(instance);
                loopedSounds.Remove(key);
            }
        }

        public void ChangeBGM(string name)
        {
            BGM.Stop();
            soundInstances.Remove(BGM);
            BGM = AddLoop(name);
            BGM.Play();
            selectedSong = name;
        }

        #endregion

        
    }
}
