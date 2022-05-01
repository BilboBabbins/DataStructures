using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

using UnionFindLibrary;

namespace MazeVisualizer
{
    public class Game1 : Game
    {
        UnionFindLibrary.QuickUnion<(int y, int x)> MazeUnion;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D pixel;
        const int graphSize = 10;
        const int totalGridSize = 1000;
        const int sqSize = totalGridSize / graphSize;
        const int extraScreen = 200;
        Tile[,] grid;
        Random rand = new Random(213213);
       

        //set up grid thing with line squares 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            graphics.PreferredBackBufferWidth = totalGridSize + extraScreen;
            graphics.PreferredBackBufferHeight = totalGridSize;
            graphics.ApplyChanges();

            grid = new Tile[graphSize, graphSize];
         

            int posY = 0;
            int posX = 0;
            (int y, int x)[] UnionValues = new (int y, int x)[graphSize*graphSize];

            int count = 0;
            for (int y = 0; y < graphSize; y++)
            {
                for (int x = 0; x < graphSize; x++)
                {
                    grid[y, x] = new Tile(pixel, new Vector2(posX, posY), Color.White, sqSize);
                    UnionValues[count] = (y, x);
                    count++;
                    posX += sqSize;
                }
                posY += sqSize;
                posX = 0;
            }

            //Generate random x, y location, generate random direction x 
            //Select either (x-1,y), (x+1,y),(x,y-1), (x, y+ 1)
          
            //now make sure that pos is a valid index, if it's not then generate new direction 

            MazeUnion = new QuickUnion<(int y, int x)>(UnionValues);

            (int y, int x) item1 = GraphGeneration.GeneratePos(graphSize, graphSize, rand);
            int unions = 0;
            while (MazeUnion.UniqueSets() != 1)
            {
               
            (int y, int x) item2 = GraphGeneration.AddDirection(item1, GraphGeneration.GenerateDirection(rand));

                if (MazeUnion.Union(item1, item2))
                {
                    unions++;
                    item1 = item2;
                }
            }

            
            base.Initialize();
        }

        //Generate random 2d position

        //Generate random direction function

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Pink);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
                    grid[a, b].drawSquare(spriteBatch, 1);
                }
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
