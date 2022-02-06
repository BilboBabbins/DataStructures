
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WeightedDirectGraphs;

namespace GraphVisualiz
{
    public class Game1 : Game
    {
        
        //make a tile class that inherits the sprite class
        //have tile have extra values yk, like graph values yk


        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Graph<point> gridGraph;
        const int graphSize = 20;
        const int screenSize = 1700;
        const int sqSize = screenSize / graphSize;
        Texture2D pixel;
        ButtonState prev = ButtonState.Released;
        bool setEnd = false;
        bool setStart = false;
        Tile[,] graph = new Tile[graphSize, graphSize];


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            graphics.PreferredBackBufferWidth = screenSize;
            graphics.PreferredBackBufferHeight = screenSize;
            int posY = 0;
            int posX = 0;
            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
                    graph[a, b] = new Tile(pixel, new Vector2(posX, posY), Color.White, sqSize);
                    posX += sqSize;
                }
                posY += sqSize;
                posX = 0;
            }
            graph[0, 0].startTile = true;
            graph[0, 0].Tint = Color.Green;
            graph[graphSize - 1, graphSize - 1].endTile = true;
            graph[graphSize - 1, graphSize - 1].Tint = Color.Red;



            gridGraph = new Graph<point>();
            WeightedDirectGraphs.Program.Generate(gridGraph ,graphSize, graphSize);
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
           
            MouseState ms = Mouse.GetState();
         
          

            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
          
                    graph[a, b].Pressed(ms);
                    if (setEnd == true && graph[a, b].startTile == false && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].endTile = true;
                        graph[a, b].Tint = Color.Red;
                        graph[a, b].wall = false;
                        setEnd = false;
                    }
                    else if (setStart == true && graph[a, b].endTile == false && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].startTile = true;
                        graph[a, b].Tint = Color.Green;
                        graph[a, b].wall = false;
                        setStart = false;
                    }
                    else if (graph[a, b].startTile == false && graph[a, b].endTile == false
                        && graph[a, b].wall == false && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].setWall();
                    }
                    else if (graph[a, b].startTile == false && graph[a, b].endTile == false
                         && graph[a, b].wall == true && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].removeWall();
                    }
                    else if (graph[a, b].startTile == true && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        setStart = true;
                        graph[a, b].Tint = Color.White;
                        graph[a, b].startTile = false;
                    }
                    else if (graph[a, b].endTile == true && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        setEnd = true;
                        graph[a, b].Tint = Color.White;
                        graph[a, b].endTile = false;
                    }
              
                }
            }
            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
                    if (graph[a, b].wall == true)
                    {
                        gridGraph(a, b) = null;
                    }
                    if (graph[a, b].endTile == true)
                    {

                    }
                    if (graph[a, b].startTile == true)
                    {

                    }
                }
            }
            prev = ms.LeftButton;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            /*for (int a = 0; a < screenSize; a += sqSize)
            {
                //horiziontal
                spriteBatch.Draw(pixel, new Rectangle( 0, a, screenSize, lineSize), Color.Black);
            }
            for (int b = 0; b < screenSize; b += sqSize)
            {
                //vertical
                spriteBatch.Draw(pixel, new Rectangle(b, 0, lineSize, screenSize), Color.Black);
            }*/

            //make grid out of squares 
            // Test.drawSquare(spriteBatch, 2, 200, 0, 0);

        
            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
                    graph[a, b].drawSquare(spriteBatch, 3);
                }
               
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
