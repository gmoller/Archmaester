using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Archmaester.Fonts
{
    public class BmFont
    {
        //private string _fontFilePath;
        //private FontFile _fontFile;
        //private Texture2D _fontTexture;
        private readonly FontRenderer _fontRenderer;

        public BmFont(string fontTexture, string png, ContentManager c)
        {
            string fontFilePath = Path.Combine(c.RootDirectory, fontTexture);
            FontFile fontFile = FontLoader.Load(fontFilePath);
            Texture2D fontTexture2D = c.Load<Texture2D>(png);
            _fontRenderer = new FontRenderer(fontFile, fontTexture2D);
        }

        public void Draw(string message, Vector2 pos, Color color, float scale, SpriteBatch spriteBatch)
        {
            _fontRenderer.DrawText(spriteBatch, (int)pos.X, (int)pos.Y, message, color, scale);
        }
    }
}