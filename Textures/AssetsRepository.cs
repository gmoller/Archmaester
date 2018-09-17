using System;
using System.Collections.Generic;
using Interfaces;

namespace Textures
{
    public sealed class AssetsRepository
    {
        private static readonly Lazy<AssetsRepository> Lazy = new Lazy<AssetsRepository>(() => new AssetsRepository());

        private readonly Dictionary<string, IFont> _fonts;
        private readonly Dictionary<string, ITexture2D> _textures;
        private readonly Dictionary<string, string> _sounds;

        public static AssetsRepository Instance => Lazy.Value;

        private AssetsRepository()
        {
            _fonts = new Dictionary<string, IFont>();
            _textures = new Dictionary<string, ITexture2D>();
            _sounds = new Dictionary<string, string>();
        }

        public void AddFont(string name, IFont font)
        {
            _fonts.Add(name, font);
        }

        public void AddTexture(string name, ITexture2D texture)
        {
            _textures.Add(name, texture);
        }

        public void AddSound(string name, string sound)
        {
            _sounds.Add(name, sound);
        }

        public IFont GetFont(string fontName)
        {
            IFont font;
            try
            {
                font = _fonts[fontName];
            }
            catch (Exception ex)
            {
                throw new Exception($"Texture [{fontName}] not found in dictionary.", ex);
            }

            return font;
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

        public string GetSound(string soundName)
        {
            string sound;
            try
            {
                sound = _sounds[soundName];
            }
            catch (Exception ex)
            {
                throw new Exception($"Sound [{soundName}] not found in dictionary.", ex);
            }

            return sound;
        }
    }
}