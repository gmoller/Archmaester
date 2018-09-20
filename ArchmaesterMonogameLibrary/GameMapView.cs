using System;
using GameLogic;
using GeneralUtilities;
using Interfaces;
using Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primitives2D;
using System.Collections.Generic;
using Input;

namespace ArchmaesterMonogameLibrary
{
    public class GameMapView
    {
        public const int ColumnCellsInView = 32;
        public const int RowsCellsInView = 18;

        private readonly GameWorld _gameWorld;
        private readonly Rectangle _drawingArea;
        private readonly Camera _camera;

        private readonly ITexture2D[] _terrainTextures;
        private readonly Overlay _overlay;
        private readonly Hud _hud;

        public int ViewWidth => _drawingArea.Width;
        public int ViewHeight => _drawingArea.Height;
        public Vector2 ViewCenter => new Vector2(_drawingArea.Width / 2.0f + CellWidth / 2.0f, _drawingArea.Height / 2.0f + CellHeight / 2.0f);
        public int ColumnCellsInWorld => _gameWorld.NumberOfColumns * CellWidth;
        public int RowCellsInWorld => _gameWorld.NumberOfRows * CellHeight;
        public int CellWidth => _drawingArea.Width / ColumnCellsInView;
        public int CellHeight => _drawingArea.Height / RowsCellsInView;

        public GameMapView(GameWorld gameWorld, Rectangle drawingArea, Hud hud)
        {
            _gameWorld = gameWorld;
            _drawingArea = drawingArea;

            _camera = new Camera(drawingArea, new Rectangle(0, 0, ColumnCellsInWorld, RowCellsInWorld), CellWidth, CellHeight);

            _terrainTextures = new ITexture2D[3];
            _terrainTextures[0] = AssetsRepository.Instance.GetTexture("Basic Terrain1");
            _terrainTextures[1] = AssetsRepository.Instance.GetTexture("Basic Terrain2");
            _terrainTextures[2] = AssetsRepository.Instance.GetTexture("Grass_Ocean_Alpha");

            _overlay = new Overlay();
            _hud = hud;
        }

        public void Update(InputState input, GameTime gameTime)
        {
            if (!_hud.MouseOver(input))
            {
                if (input.IsLeftMouseButtonDown())
                {
                    // determine where mouse pointer is in relation to the center
                    Vector2 direction = new Vector2(input.CurrentMouseState.Position.X - ViewCenter.X, input.CurrentMouseState.Position.Y - ViewCenter.Y);
                    MoveMapOneTile(direction);
                }

                if (input.IsRightMouseButtonPressed())
                {
                    CenterOnViewPosition(input.CurrentMouseState.Position);
                }
            }
        }

        private CompassDirection2 GetDirection(Vector2 v)
        {
            double angle = Math.Atan2(v.X, v.Y);
            int octant = (int)Math.Round(8 * angle / (2 * Math.PI) + 8) % 8;

            CompassDirection2 dir = (CompassDirection2)octant;

            return dir;
        }

        private void MoveMapOneTile(Vector2 direction)
        {
            CompassDirection2 dir = GetDirection(direction);

            switch (dir)
            {
                case CompassDirection2.North:
                    CenterOnViewPosition(new Point((int)ViewCenter.X, (int)ViewCenter.Y - CellHeight));
                    break;
                case CompassDirection2.NorthEast:
                    CenterOnViewPosition(new Point((int)ViewCenter.X + CellWidth, (int)ViewCenter.Y - CellHeight));
                    break;
                case CompassDirection2.East:
                    CenterOnViewPosition(new Point((int)ViewCenter.X + CellWidth, (int)ViewCenter.Y));
                    break;
                case CompassDirection2.SouthEast:
                    CenterOnViewPosition(new Point((int)ViewCenter.X + CellWidth, (int)ViewCenter.Y + CellHeight));
                    break;
                case CompassDirection2.South:
                    CenterOnViewPosition(new Point((int)ViewCenter.X, (int)ViewCenter.Y + CellHeight));
                    break;
                case CompassDirection2.SouthWest:
                    CenterOnViewPosition(new Point((int)ViewCenter.X - CellWidth, (int)ViewCenter.Y + CellHeight));
                    break;
                case CompassDirection2.West:
                    CenterOnViewPosition(new Point((int)ViewCenter.X - CellWidth, (int)ViewCenter.Y));
                    break;
                case CompassDirection2.NorthWest:
                    CenterOnViewPosition(new Point((int)ViewCenter.X - CellWidth, (int)ViewCenter.Y - CellHeight));
                    break;
            }
        }

        private void CenterOnViewPosition(Point viewPosition)
        {
            Point viewCell = GetViewCell(viewPosition);
            Point worldCell = GetWorldCell(viewCell);

            CenterOnWorldCell(worldCell);
        }

        private Point GetViewCell(Point location)
        {
            int screenColumn = location.X / CellWidth;
            int screenRow = location.Y / CellHeight;
            Point viewCell = new Point(screenColumn, screenRow);

            return viewCell;
        }

        private Point GetWorldCell(Point view)
        {
            int temp1 = _camera.VisibleRectangle.X - _drawingArea.X + 0;
            int temp2 = temp1 / CellWidth;

            int temp3 = temp2 + view.X;

            int temp4 = _camera.VisibleRectangle.Y - _drawingArea.Y + 0;
            int temp5 = temp4 / CellHeight;

            int temp6 = temp5 + view.Y;

            return new Point(temp3, temp6);
        }

        private void CenterOnWorldCell(Point worldCell)
        {
            _camera.CenterOnWorldCell(worldCell);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int rowIndex = 0; rowIndex < _gameWorld.NumberOfRows; ++rowIndex)
            {
                DrawRow(rowIndex, spriteBatch);
            }

            DrawMarkers(spriteBatch);

            spriteBatch.End();
        }

        private void DrawRow(int rowIndex, SpriteBatch spriteBatch)
        {
            for (int colIndex = 0; colIndex < _gameWorld.NumberOfColumns; ++colIndex)
            {
                Point2 cellLocation = Point2.Create(colIndex, rowIndex);
                DrawColumn(cellLocation, spriteBatch);
            }
        }

        private void DrawColumn(Point2 cellLocation, SpriteBatch spriteBatch)
        {
            // x & y are the co-ordinates relative to the world
            int worldX = cellLocation.X * CellWidth;
            int worldY = cellLocation.Y * CellHeight;

            // convert these to co-ordinates relative to the view (screen)
            // so we know where to draw this tile
            int screenX = worldX - _camera.VisibleRectangle.X;
            int screenY = worldY - _camera.VisibleRectangle.Y;

            if (_drawingArea.Contains(screenX, screenY))
            {
                var rectangle = new Rectangle(screenX, screenY, CellWidth, CellHeight);

                // which terrain are we drawing?
                int terrainTypeId = _gameWorld.GetTerrainTypeIdOfCell(cellLocation);

                // draw cell
                if (_gameWorld.IsCellVisible(cellLocation))
                {
                    Point tile = GetTile(terrainTypeId);
                    Rectangle sourceRectangle = _terrainTextures[tile.X].Frames[tile.Y].Rectangle;

                    spriteBatch.Draw(_terrainTextures[tile.X], rectangle, sourceRectangle, Color.White);
                    DrawOverlays(terrainTypeId, cellLocation, rectangle, spriteBatch);
                }
                else
                {
                    spriteBatch.FillRectangle(rectangle, Color.Black);
                }

                // draw grid
                spriteBatch.DrawRectangle(rectangle, Color.Black);
            }
        }

        private Point GetTile(int terrainTypeId)
        {
            switch (terrainTypeId)
            {
                case 0: // plains
                    return new Point(1, 3);
                case 1: // forest
                    return new Point(0, 1);
                case 6: // hills
                    return new Point(1, 0);
                case 7: // mountains
                    return new Point(0, 5);
                case 11: // ocean
                    return new Point(1, 5);
                default:
                    throw new Exception($"Terrain texture [{terrainTypeId}] not found!");
            }
        }

        private void DrawOverlays(int terrainTypeId, Point2 cellLocation, Rectangle rectangle, SpriteBatch spriteBatch)
        {
            List<int> neighboringTerrainTypeIds = _gameWorld.GetNeighboringTerrainTypeIds(cellLocation);
            _overlay.DrawFrame(terrainTypeId, neighboringTerrainTypeIds, rectangle, _terrainTextures[2], spriteBatch);
        }

        private void DrawMarkers(SpriteBatch spriteBatch)
        {
            float radius = CellWidth / 2.0f;
            spriteBatch.DrawCircle(0 * CellWidth + radius, 0 * CellHeight + radius, radius, 50, Color.DeepPink);
            spriteBatch.DrawCircle((ColumnCellsInView - 1) * CellWidth + radius, 0 * CellHeight + radius, radius, 50, Color.DeepPink);
            spriteBatch.DrawCircle((ColumnCellsInView / 2) * CellWidth + radius, (RowsCellsInView / 2) * CellHeight + radius, radius, 50, Color.DeepPink); // Center
            spriteBatch.DrawCircle(0 * CellWidth + radius, (RowsCellsInView - 1) * CellHeight + radius, radius, 50, Color.DeepPink);
            spriteBatch.DrawCircle((ColumnCellsInView - 1) * CellWidth + radius, (RowsCellsInView - 1) * CellHeight + radius, radius, 50, Color.DeepPink);
        }

        private enum CompassDirection2
        {
            South,
            SouthEast,
            East,
            NorthEast,
            North,
            NorthWest,
            West,
            SouthWest
        }
    }
}