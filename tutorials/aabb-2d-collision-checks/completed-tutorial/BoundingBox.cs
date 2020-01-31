using Microsoft.Xna.Framework;

namespace AABBCollisionDetection
{
    public class BoundingBox
    {
        /// <summary>
        ///     Gets or Sets the xy-coordinate top-left position
        ///     of the bounding box
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;

        /// <summary>
        ///     The width of the bounding box
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        ///     The height of the bounding box
        /// </summary>
        public float Height { get; set; }


        /// <summary>
        ///     Creates a new bounding box instnace
        /// </summary>
        /// <param name="position">The position of the bounding box</param>
        /// <param name="width">The width of the bounding box</param>
        /// <param name="height">The height of the bounding box</param>
        public BoundingBox(Vector2 position, float width, float height)
        {
            //  Set the properties
            _position = position;
            Width = width;
            Height = height;
        }


        /// <summary>
        ///     Gets or Sets the y-coordinate position of the top
        ///     edge of the bounding box
        /// </summary>
        public float Top
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        /// <summary>
        ///     Gets or Sets the y-coordinate position of the bottom
        ///     edge of the bounding box
        /// </summary>
        public float Bottom
        {
            get { return _position.Y + Height; }
            set { _position.Y = value - Height; }
        }

        /// <summary>
        ///     Gets or Sets the x-coordinate position of the left
        ///     edge of the bounding box
        /// </summary>
        public float Left
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        /// <summary>
        ///     Gets or Sets the x-coordinate position of the right
        ///     edge of the bounding box
        /// </summary>
        public float Right
        {
            get { return _position.X + Width; }
            set { _position.X = value - Width; }
        }

        /// <summary>
        ///     Performs Axis-Aligned Bounding Box collision check against another
        ///     BoundingBox
        /// </summary>
        /// <param name="other">The other BoundingBox to check if this and that one is colliding</param>
        /// <returns>
        ///     True if they are colliding; otherwise false.
        /// </returns>
        public bool CollisionCheck(BoundingBox other)
        {
            // 1. Is the left edge of this BoundingBox less than the right edge of the other BoundingBox
            // 2. Is the right edge of this BoundingBox greater than the left edge of the other BoundingBox
            // 3. Is the top edge of this BoundingBox less than the bottom edge of the other BoundingBox
            // 4. Is the bottom edge of this BoundingBox greater than the top edge of the other BoundingBox
            if (this.Left < other.Right && this.Right > other.Left &&
                this.Top < other.Bottom && this.Bottom > other.Top)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     Gets a Rectangle representation of this BoundingBox
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Left, (int)Top, (int)Width, (int)Height);
            }
        }


    }
}
