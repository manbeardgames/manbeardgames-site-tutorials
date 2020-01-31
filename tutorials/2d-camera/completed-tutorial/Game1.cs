using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CameraTutorial
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //  Our camera
        Camera2D _camera;

        //  The position of our player
        Vector2 _playerPosition = new Vector2(0, 100);

        //  The texture used to render our player
        Texture2D _playerTexture;

        //  This is a rectangle that defines our game world at 1500 wide and 1000 tall
        Rectangle _gameWorld = new Rectangle(0, 0, 1500, 1000);


        //  This is just a 1x1 pixel that we use to draw our rectangle
        Texture2D _pixelTexture;

        //  Font we use to display text on the screen
        SpriteFont _font;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            //  Make sure mouse is visible
            IsMouseVisible = true;

            //  Set our initial widht and height at 1280 x 720
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            //  Allow user to resize window if they'd like to
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _camera = new Camera2D(GraphicsDevice.Viewport);
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //  Load the texture of our player
            _playerTexture = Content.Load<Texture2D>("player");

            //  Create our 1x1 pixel to draw our rectangle
            _pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            _pixelTexture.SetData<Color>(new Color[] { Color.White });

            //  Load our font
            _font = Content.Load<SpriteFont>("font");
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

            //  Calculate delta time
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //  Check for player movement;
            CheckForPlayerMovement(deltaTime);

            //  Check for camera movement;
            CheckForCameraMovement(deltaTime);

            base.Update(gameTime);
        }

        /// <summary>
        ///     Checks keyboard input to determine if player should move
        /// </summary>
        /// <param name="deltaTime"></param>
        private void CheckForPlayerMovement(float deltaTime)
        {
            //  Player rate of speed
            float playerSpeed = 200.0f;

            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _playerPosition -= Vector2.UnitY * playerSpeed * deltaTime;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _playerPosition += Vector2.UnitY * playerSpeed * deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _playerPosition -= Vector2.UnitX * playerSpeed * deltaTime;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _playerPosition += Vector2.UnitX * playerSpeed * deltaTime;
            }


            //  Ensure the player can't move beyond the game world
            if (_playerPosition.X <= _gameWorld.X) { _playerPosition.X = _gameWorld.X; }
            if (_playerPosition.X >= _gameWorld.Width - _playerTexture.Width) { _playerPosition.X = _gameWorld.Width - _playerTexture.Width; }

            if (_playerPosition.Y <= _gameWorld.Y) { _playerPosition.Y = _gameWorld.Y; }
            if (_playerPosition.Y >= _gameWorld.Height - _playerTexture.Height) { _playerPosition.Y = _gameWorld.Height - _playerTexture.Height; }
        }


        /// <summary>
        ///     Checks keyboard input to determine if camera should move
        /// </summary>
        /// <param name="deltaTime"></param>
        private void CheckForCameraMovement(float deltaTime)
        {
            //  Player rate of speed
            float cameraSpeed = 200.0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _camera.Position -= Vector2.UnitY * cameraSpeed * deltaTime;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _camera.Position += Vector2.UnitY * cameraSpeed * deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _camera.Position -= Vector2.UnitX * cameraSpeed * deltaTime;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _camera.Position += Vector2.UnitX * cameraSpeed * deltaTime;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //  Pass the camera transform matrix to the spritebatch. Doing this will make anything
            //  drawn after .Begin take into account the camera's translation
            spriteBatch.Begin(transformMatrix: _camera.TransformationMatrix);

            //  Draw a rectangle that sybolizes the bounds of our game world
            spriteBatch.Draw(_pixelTexture, _gameWorld, new Color(8, 30, 60));
            //  Draw the player
            spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);

            spriteBatch.End();


            //  If we do not pass the transformation matrix, like below, then 
            //  what is rendered to the screen is not affected by the camera. This is useful
            //  for things like text that you want to display statically in the same position
            spriteBatch.Begin();

            //  Display the players position
            spriteBatch.DrawString(_font, $"Player Position (top-left): {_playerPosition.ToPoint()}", Vector2.Zero, Color.White);
            //  Display the camera position
            spriteBatch.DrawString(_font, $"Camera Position (top-left): {_camera.Position.ToPoint()}", new Vector2(0, 20), Color.White);
            //  Display the mouse position in screen space
            spriteBatch.DrawString(_font, $"Mouse Position (Screen Space): {Mouse.GetState().Position}", new Vector2(0, 40), Color.White);
            //  Display the mouse position in world space
            spriteBatch.DrawString(_font, $"Mouse Position (World Space): {_camera.ScreenToWorld(Mouse.GetState().Position.ToVector2())}", new Vector2(0, 60), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
