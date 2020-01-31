using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AABBCollisionDetection
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //  This is the bounding box that we'll move around
        private BoundingBox _boundingBox;

        //  This is the bounding box that will be stationary in
        //  the center of the screen
        private BoundingBox _otherBoundingBox;

        //  The width of our screen
        private int _screenWidth = 1280;

        //  The height of our screen
        private int _screenHeight = 720;

        //  This is a 1x1 texture that we'll use as the 
        //  texture when we render our bounding boxes
        private Texture2D _pixel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //  Create the first bounding box at the right edge of the screen. By multiplying
            //  the position by 0.5f, we put it at center height
            _boundingBox = new BoundingBox(new Vector2(0, _screenHeight) * 0.5f, 50, 50);

            //  Create the second boundinb box at the center of the screen.
            _otherBoundingBox = new BoundingBox(new Vector2(_screenWidth, _screenHeight) * 0.5f, 50, 50);


            //  The following is just to actually set the screen width and height
            graphics.PreferredBackBufferWidth = _screenWidth;
            graphics.PreferredBackBufferHeight = _screenHeight;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //  Create a new 1x1 Texture2D
            _pixel = new Texture2D(GraphicsDevice, 1, 1);

            //  Set the color data for the Texture2D as White
            _pixel.SetData<Color>(new Color[] { Color.White });

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //  Get the delta time between current and previous update frame
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //  The speed rate at which we'll move _boundingBox
            float speed = 200.0f;

            //  Get the state of the keyboard
            var keyboardState = Keyboard.GetState();

            //  Check for input on the arrow keys and move the _boundingBox accordingly
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _boundingBox.Position -= Vector2.UnitY * speed * deltaTime;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _boundingBox.Position += Vector2.UnitY * speed * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _boundingBox.Position -= Vector2.UnitX * speed * deltaTime;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                _boundingBox.Position += Vector2.UnitX * speed * deltaTime;
            }

            //  This is to prevent the _boundingBox from leaving the edges of the screen
            if (_boundingBox.Left <= 0) { _boundingBox.Left = 0; }
            else if (_boundingBox.Right >= _screenWidth) { _boundingBox.Right = _screenWidth; }

            if (_boundingBox.Top <= 0) { _boundingBox.Top = 0; }
            if (_boundingBox.Bottom >= _screenHeight) { _boundingBox.Bottom = _screenHeight; }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //  Changed this to Black.
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            //  Check if there is a collision. If so, render the BoundingBoxes as red.
            //  Otherwise render them as green
            if (_boundingBox.CollisionCheck(_otherBoundingBox))
            {
                spriteBatch.Draw(_pixel, _boundingBox.Bounds, Color.Red);
                spriteBatch.Draw(_pixel, _otherBoundingBox.Bounds, Color.Red);
            }
            else
            {
                spriteBatch.Draw(_pixel, _boundingBox.Bounds, Color.Green);
                spriteBatch.Draw(_pixel, _otherBoundingBox.Bounds, Color.Green);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
