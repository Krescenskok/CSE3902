using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sprint5
{
    public static class SoundContent
    {
        public static Dictionary<string, SoundEffectInstance> LoadContent(ContentManager contentManager, string contentFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "/" + contentFolder);
            if (!dir.Exists) throw new DirectoryNotFoundException();


            Dictionary<string, SoundEffectInstance> result = new Dictionary<string, SoundEffectInstance>();

            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                SoundEffect sound = contentManager.Load<SoundEffect>(contentFolder + "/" + key);
                result[key] = sound.CreateInstance();
            }
            return result;
        }
    }
}
