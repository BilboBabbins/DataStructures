using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using UnionFindLibrary;

namespace MazeVisualizer
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D pixel;
        const int graphSize = 30;
        const int totalGridSize = 600;
        const int sqSize = totalGridSize / graphSize;
        const int extraScreen = 0;
        Tile[,] grid;
        Random rand = new Random();
        Stack<DrawingInfo> info = new Stack<DrawingInfo>();

        //set up grid thing with line squares 
        QuickUnion<(int y, int x)> MazeUnion;

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
            (int y, int x)[] UnionValues = new (int y, int x)[graphSize * graphSize];

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

            int unions = 0;
            while (MazeUnion.UniqueSets() != 1)
            {

                (int y, int x) item1 = GraphGeneration.GeneratePos(graphSize, graphSize, rand);
                (int y, int x) item2 = GraphGeneration.AddDirection(item1, GraphGeneration.GenerateDirection(rand));


                while (item2.x > graphSize - 1 || item2.y > graphSize - 1 || item2.x < 0 || item2.y < 0)
                {
                    item2 = GraphGeneration.AddDirection(item1, GraphGeneration.GenerateDirection(rand));
                }

                if (MazeUnion.Union(item1, item2))
                {
                    unions++;
                    //top
                    if (item1.y + 1 == item2.y)
                    {
                        info.Push(new DrawingInfo(item1, item2, Directions.Bottom));
                    }
                    //bottom
                    if (item1.y - 1 == item2.y)
                    {
                        info.Push(new DrawingInfo(item1, item2, Directions.Top));
                    }
                    //right
                    if (item1.x + 1 == item2.x)
                    {
                        info.Push(new DrawingInfo(item1, item2, Directions.Right));
                    }
                    //left
                    if (item1.x - 1 == item2.x)
                    {
                        info.Push(new DrawingInfo(item1, item2, Directions.Left));
                    }
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
            watch.Start();
        }

        Stopwatch watch = new Stopwatch();

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            for (int i = 0; i < 3 && info.Count > 0; i++)
            {
                var pop = info.Pop();
                ProcessData(pop);

            }

            base.Update(gameTime);
        }

        void ProcessData(DrawingInfo update)
        {
            if (update.Direction == Directions.Bottom)
            {
                grid[update.Cell.y, update.Cell.x].bottomWall = false;
                grid[update.Neighbor.y, update.Neighbor.x].topWall = false;
            }
            if (update.Direction == Directions.Top)
            {
                grid[update.Cell.y, update.Cell.x].topWall = false;
                grid[update.Neighbor.y, update.Neighbor.x].bottomWall = false;
            }
            if (update.Direction == Directions.Right)
            {
                grid[update.Cell.y, update.Cell.x].rightWall = false;
                grid[update.Neighbor.y, update.Neighbor.x].leftWall = false;
            }
            if (update.Direction == Directions.Left)
            {
                grid[update.Cell.y, update.Cell.x].leftWall = false;
                grid[update.Neighbor.y, update.Neighbor.x].rightWall = false;
            }
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
