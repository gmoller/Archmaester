using System;
using Common;
using GameLogic;
using GameMap;
using GeneralUtilities;
using Interfaces;
using Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primitives2D;

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

        public int ViewWidth => _drawingArea.Width;
        public int ViewHeight => _drawingArea.Height;
        public Vector2 ViewCenter => new Vector2(_drawingArea.Width / 2.0f + CellWidth / 2.0f, _drawingArea.Height / 2.0f + CellHeight / 2.0f);
        public int ColumnCellsInWorld => _gameWorld.NumberOfColumns * CellWidth;
        public int RowCellsInWorld => _gameWorld.NumberOfRows * CellHeight;
        public int CellWidth => _drawingArea.Width / ColumnCellsInView; // 100
        public int CellHeight => _drawingArea.Height / RowsCellsInView; // 100

        public GameMapView(GameWorld gameWorld, Rectangle drawingArea)
        {
            _gameWorld = gameWorld;
            _drawingArea = drawingArea;

            _camera = new Camera(drawingArea, new Rectangle(0, 0, ColumnCellsInWorld, RowCellsInWorld), CellWidth, CellHeight);

            _terrainTextures = new ITexture2D[2];
            _terrainTextures[0] = AssetsRepository.Instance.GetTexture("Basic Terrain1");
            _terrainTextures[1] = AssetsRepository.Instance.GetTexture("Basic Terrain2");
        }

        public void MoveMap(Vector2 direction)
        {
            _camera.MoveCamera(direction);
        }

        public void CenterOnViewPosition(Point viewPosition)
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

            for (int rowIndex = 0; rowIndex < _gameWorld.GameBoard.NumberOfRows; ++rowIndex)
            {
                DrawRow(rowIndex, spriteBatch);
            }

            DrawMarkers(spriteBatch);

            spriteBatch.End();
        }

        private void DrawRow(int rowIndex, SpriteBatch spriteBatch)
        {
            for (int colIndex = 0; colIndex < _gameWorld.GameBoard.NumberOfColumns; ++colIndex)
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

                // which cell are we drawing
                Cell cell = _gameWorld.GetCell(cellLocation);

                // draw cell
                if (_gameWorld.IsCellVisible(cellLocation))
                {
                    Point tile = GetTile(cell.TerrainTypeId);
                    Rectangle sourceRectangle = _terrainTextures[tile.X].Frames[tile.Y].Rectangle;

                    spriteBatch.Draw(_terrainTextures[tile.X], rectangle, sourceRectangle, Color.White);
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

        private void DrawMarkers(SpriteBatch spriteBatch)
        {
            float radius = CellWidth / 2.0f;
            spriteBatch.DrawCircle(0 * CellWidth + radius, 0 * CellHeight + radius, radius, 50, Color.DeepPink);
            spriteBatch.DrawCircle((ColumnCellsInView - 1) * CellWidth + radius, 0 * CellHeight + radius, radius, 50, Color.DeepPink);
            spriteBatch.DrawCircle((ColumnCellsInView / 2) * CellWidth + radius, (RowsCellsInView / 2) * CellHeight + radius, radius, 50, Color.DeepPink); // Center
            spriteBatch.DrawCircle(0 * CellWidth + radius, (RowsCellsInView - 1) * CellHeight + radius, radius, 50, Color.DeepPink);
            spriteBatch.DrawCircle((ColumnCellsInView - 1) * CellWidth + radius, (RowsCellsInView - 1) * CellHeight + radius, radius, 50, Color.DeepPink);
        }
    }
}