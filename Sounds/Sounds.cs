using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        private Game game;

        public void LoadSounds(Game game)
        {
            this.game = game;
            sounds = SoundContent.LoadContent(game.Content, "Sounds");

            

            lowHealth = Get("LowHealth");
            lowHealth.IsLooped = true;
        }

        public void Mute()
        {
            foreach(KeyValuePair<string,SoundEffectInstance> pair in sounds)
            {
                pair.Value.Volume = 0;
            }
        }

        public void UnMute()
        {
            foreach (KeyValuePair<string, SoundEffectInstance> pair in sounds)
            {
                pair.Value.Volume = 1;
            }
        }


        private SoundEffectInstance Get(string name)
        {
            if (sounds.ContainsKey(name)) return sounds[name];
            return null;
        }

     

        #region //sound effects

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
