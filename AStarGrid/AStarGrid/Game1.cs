using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AStarGrid
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        const int sqSize = 20;
        const int graphWidth = 10;
        const int graphHeight = 20
        Texture2D pixel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pixel = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            //800,480
            for (int a = 0; a < width; a += sqSize)
            {
                //horiziontal
                _spriteBatch.Draw(pixel, new Rectangle(50, 50, 1, 200), Color.Green);
            }
            for (int b = 0; b < ; b++)
            {
                //vertical
                _spriteBatch.Draw(pixel, new Rectangle(50, 50, 1, 200), Color.Green);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
