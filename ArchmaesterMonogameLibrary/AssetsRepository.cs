using System;
using System.Collections.Generic;
using System.IO;
using BitmapFonts;
using Microsoft.Xna.Framework.Content;
using Textures;

namespace ArchmaesterMonogameLibrary
{
    public sealed class AssetsRepository
    {
        private static readonly Lazy<AssetsRepository> Lazy = new Lazy<AssetsRepository>(() => new AssetsRepository());

        private readonly Dictionary<string, IFont> _fonts;
        private readonly Dictionary<string, ITexture2D> _textures;

        public static AssetsRepository Instance => Lazy.Value;

        private AssetsRepository()
        {
            _fonts = new Dictionary<string, IFont>();
            _textures = new Dictionary<string, ITexture2D>();
        }

        public void AddFont(string name, IFont font)
        {
            _fonts.Add(name, font);
        }

        public void AddTexture(string name, ITexture2D texture)
        {
            _textures.Add(name, texture);
        }

        public void AddTextures(string path, ContentManager content)
        {
            string[] files = Directory.GetFiles(Path.Combine("Content", path), "*.xnb");

            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                _textures.Add(fileName, new Texture2DWrapper(Path.Combine(path, fileName), content));
            }
        }

        public IFont GetFont(string fontName)
        {
            return _fonts[fontName];
        }

        public ITexture2D GetTexture(string textureName)
        {
            ITexture2D tex;
            try
            {
                tex = _textures[textureName];
            }
            catch (Exception ex)
            {
                throw new Exception($"Texture [{textureName}] not found in dictionary.", ex);
            }

            return tex;
        }
    }
}