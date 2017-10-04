using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EvolutionConquest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Framework variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputState _inputState;
        private Player _player;
        private SpriteFont _diagFont;
        //Game variables
        private Texture2D _whitePixel;
        private int _frames;
        private double _elapsedSeconds;
        private int _fps;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _inputState = new InputState();
            _player = new Player();
            _frames = 0;
            _elapsedSeconds = 0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Global.Camera.ViewportWidth = _graphics.GraphicsDevice.Viewport.Width;
            Global.Camera.ViewportHeight = _graphics.GraphicsDevice.Viewport.Height;
            Global.Camera.CenterOn(new Vector2(Global.Camera.ViewportWidth / 2, Global.Camera.ViewportHeight / 2));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the Font from the Content object. Use the Content Pipeline under the  "Content" folder to add assets to game
            _diagFont = Content.Load<SpriteFont>("DiagnosticsFont");

            //Load in a simple white pixel
            _whitePixel = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            Color[] color = new Color[1];
            color[0] = Color.White;
            _whitePixel.SetData(color);
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
            if (_inputState.IsExitGame(PlayerIndex.One))
            {
                Exit();
            }
            else
            {
                _inputState.Update();
                _player.HandleInput(_inputState);
                Global.Camera.HandleInput(_inputState, PlayerIndex.One);
            }

            //Update Logic goes here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //FPS Logic
            if (_elapsedSeconds >= 1)
            {
                _fps = _frames;
                _frames = 0;
                _elapsedSeconds = 0;
            }
            _frames++;
            _elapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //DRAW IN THE WORLD
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.Camera.TranslationMatrix);
            //Sample Draws to test Panning/Zooming. Notice that choosing a color on the Draw Results in the pixel color changing. This works because putting a tint onto white results in that color 
            _spriteBatch.Draw(_whitePixel, new Rectangle(Global.Camera.ViewportWidth / 2, Global.Camera.ViewportHeight / 2, 10, 10), Color.Black);
            _spriteBatch.Draw(_whitePixel, new Rectangle(-100, 100, 10, 10), Color.Red);
            _spriteBatch.End();

            //DRAW HUD INFOMATION
            _spriteBatch.Begin();
            //FPS Counter in top left corner
            _spriteBatch.DrawString(_diagFont, "FPS: " + _fps, new Vector2(10, 10), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}