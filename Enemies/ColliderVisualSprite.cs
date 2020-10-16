using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final.Enemies
{
    public class ColliderVisualSprite
    {
        Texture2D texture;
        public ColliderVisualSprite(Game game, Vector2 size)
        {
            texture = new Texture2D(game.GraphicsDevice, (int)size.X, (int)size.Y);

            Color[] data = new Color[texture.Width*texture.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.GreenYellow;
            texture.SetData(data);
        }

        public void Draw(SpriteBatch batch, Vector2 location)
        {
            batch.Draw(texture, location, Color.Green);
        }
    }
}
