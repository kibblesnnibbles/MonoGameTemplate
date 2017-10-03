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
        private Texture2D _blackPixel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _inputState = new InputState();
            _player = new Player();
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

            _diagFont = Content.Load<SpriteFont>("DiagnosticsFont");
            _blackPixel = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            Color[] color = new Color[1];
            color[0] = Color.Black;
            _blackPixel.SetData(color);
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
                _player.HandleInput(_inputState);
            }

            _inputState.Update();
            Global.Camera.HandleInput(_inputState, PlayerIndex.One);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.Camera.TranslationMatrix);
            _spriteBatch.Draw(_blackPixel, new Rectangle(Global.Camera.ViewportWidth / 2, Global.Camera.ViewportHeight / 2, 10, 10), Color.Black);
            _spriteBatch.Draw(_blackPixel, new Rectangle(-100, 100, 10, 10), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
