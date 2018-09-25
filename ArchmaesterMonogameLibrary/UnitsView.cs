using BitmapFonts;
using GameLogic;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Textures;

namespace ArchmaesterMonogameLibrary
{
    public class UnitsView
    {
        private readonly GameWorld _gameWorld;
        private readonly IFont _font;

        public UnitsView(GameWorld gameWorld)
        {
            _gameWorld = gameWorld;

            //_texture = AssetsRepository.Instance.GetTexture("Basic Terrain1");
            _font = AssetsRepository.Instance.GetFont("MenuBitmapFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (Unit unit in _gameWorld.HumanPlayerUnits)
            {
                Vector2 position = new Vector2(unit.Location.X * 50 + 10, unit.Location.Y * 50 + 10);
                //_texture.Draw(position, Color.White, 1.0f, spriteBatch);
                spriteBatch.DrawString(_font, "A", position, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }

            spriteBatch.End();
        }
    }
}