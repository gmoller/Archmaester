using System;
using GeneralUtilities;
using Microsoft.Xna.Framework;

namespace GuiControls
{
    public abstract class Control
    {
        protected readonly VerticalAlignment VerticalAlignment;
        protected readonly HorizontalAlignment HorizontalAlignment;
        protected readonly Vector2 Position;

        protected Size Size;
        protected int ScaledWidth => (int)(Size.Width * Scale);
        protected int ScaledHeight => (int)(Size.Height * Scale);

        public Rectangle Area => DetermineArea(VerticalAlignment, HorizontalAlignment, Position, ScaledWidth, ScaledHeight);
        public float Scale { get; set; }
        public float Alpha { get; set; }

        protected Control(VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position)
        {
            VerticalAlignment = verticalAlignment;
            HorizontalAlignment = horizontalAlignment;
            Position = position;
        }

        private Rectangle DetermineArea(VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, int scaledWidth, int scaledHeight)
        {
            Vector2 topLeftPosition = DetermineTopLeftPosition(verticalAlignment, horizontalAlignment, position, scaledWidth, scaledHeight);

            Rectangle area = new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, scaledWidth, scaledHeight);

            return area;
        }

        protected Vector2 DetermineTopLeftPosition(VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, int scaledWidth, int scaledHeight)
        {
            Vector2 offset;

            if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Left)
            {
                offset = new Vector2(0.0f, 0.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Center)
            {
                offset = new Vector2(scaledWidth / 2.0f, 0.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Right)
            {
                offset = new Vector2(scaledWidth, 0.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Middle && horizontalAlignment == HorizontalAlignment.Left)
            {
                offset = new Vector2(0.0f, scaledHeight / 2.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Middle && horizontalAlignment == HorizontalAlignment.Center)
            {
                offset = new Vector2(scaledWidth / 2.0f, scaledHeight / 2.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Middle && horizontalAlignment == HorizontalAlignment.Right)
            {
                offset = new Vector2(scaledWidth, scaledHeight / 2.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Left)
            {
                offset = new Vector2(0.0f, scaledHeight);
            }
            else if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Center)
            {
                offset = new Vector2(scaledWidth / 2.0f, scaledHeight);
            }
            else if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Right)
            {
                offset = new Vector2(scaledWidth, scaledHeight);
            }
            else
            {
                throw new NotImplementedException($"{verticalAlignment}{horizontalAlignment} alignment has not been implemented yet.");
            }

            Vector2 topLeftPosition = position - offset;

            return topLeftPosition;
        }
    }
}