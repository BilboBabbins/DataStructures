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
    //WHEN U COME BAKC FROM KOREA DO THIS 
    //when click one of the heuristics buttons 
    // highlight the path taken, add delay so it loooks cool
    //also fix the hesuristics buttons colors bc it doesn't match the other button colors


    
    
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D pixel;
        Texture2D pixelForPF;
        int graphSize = 50;
        const int totalGridSize = 800;
        int sqSize;
        const int extraScreen = 300;
        Tile[,] grid;
        Random rand = new Random();
        Stack<DrawingInfo> info = new Stack<DrawingInfo>();
        bool startMaze = false;
        bool startPathfinding = false;
        //Graph<(int, int)> gridGraph;
        pathTiles startTile;
        pathTiles endTile;
        pathTiles[,] graphForPath;
        LinkedList<Vertex<(int, int)>> path = new LinkedList<Vertex<(int,int)>>();

        SpriteFont font;
        Button start;
        Texture2D buttonTexture;
        Texture2D arrowTexture;
        Texture2D downArrowTexture;
        Texture2D redDotTexture;
        Texture2D greenDotTexture;
        ButtonState prev = ButtonState.Released;

        Button redDot;
        Button greenDot;
     
        Button mazeSizeText;
        Button downArrow;
        Button upArrow;
        Button startPathButton;

        Button manhattanButton;
        Button diagonalButton;
        Button euclideanButton;
        Button loadPath;

        bool manhattan;
        bool diagonal;
        bool euclidean;
        
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

            sqSize = totalGridSize / graphSize;
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            graphics.PreferredBackBufferWidth = totalGridSize + extraScreen;
            graphics.PreferredBackBufferHeight = totalGridSize;
            graphics.ApplyChanges();

            InitGrid();




            //Generate random x, y location, generate random direction x 
            //Select either (x-1,y), (x+1,y),(x,y-1), (x, y+ 1)

            //now make sure that pos is a valid index, if it's not then generate new direction  

            base.Initialize();
        }
        void InitGrid()
        {
            grid = new Tile[graphSize, graphSize];
            int posY = 0;
            int posX = 0;


            int count = 0;
            for (int x = 0; x < graphSize; x++)
            {
                for (int y = 0; y < graphSize; y++)
                {
                    grid[y, x] = new Tile(pixel, new Vector2(posY, posX), Color.Transparent, sqSize, new Vector2(1, 1));
                    //UnionValues[count] = (y, x);
                    count++;
                    posX += sqSize;
                }
                posY += sqSize;
                posX = 0;
            }
        }

        void InitGraphForPath()
        {
            graphForPath = new pathTiles[graphSize, graphSize];
            int posX = 0;
            int posY = 0;
            for (int x = 0; x < graphSize; x++)
            {
                for (int y = 0; y < graphSize; y++)
                {
                    graphForPath[x, y] = new pathTiles(new Vector2(posX, posY), sqSize);
                    posY += sqSize;
                }
                posX += sqSize;
                posY = 0;
            }
            redDot.Pos = graphForPath[graphSize - 1, graphSize - 1].Pos;
            greenDot.Pos = graphForPath[0, 0].Pos;
            graphForPath[0, 0].startTile = true;
            graphForPath[graphSize - 1, graphSize - 1].endTile = true;
            endTile = graphForPath[graphSize - 1, graphSize - 1];
            startTile = graphForPath[0, 0];

            redDot.Scale = new Vector2((float)sqSize / redDotTexture.Width, (float)sqSize / redDotTexture.Width);
            greenDot.Scale = new Vector2((float)sqSize / greenDotTexture.Width, (float)sqSize / greenDotTexture.Width);
        }
        Graph<Vertex<(int, int)>> copyMaze(Tile[,] grid)
        {
            Graph<(int, int)> mazeGraph = new Graph<(int, int)>();
            Vertex<(int, int)>[,] points = new Vertex<(int, int)>[graphSize, graphSize];
            //init copy of maze
            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
                    points[a, b] = new Vertex<(int, int)>((a, b));
                    mazeGraph.AddVertex(points[a, b]);
                }
            }

            for (int y = 0; y < graphSize; y++)
            {
                for (int x = 0; x < graphSize; x++)
                {
                    if (x + 1 < graphSize && !grid[y, x].rightWall)
                    {
                        mazeGraph.AddEdge(points[y, x], points[y, x + 1], 1); //Right edge
                    }
                    if (x - 1 >= 0 && !grid[y, x].leftWall)
                    {
                        mazeGraph.AddEdge(points[y, x], points[y, x - 1], 1); //Left edge
                    }
                    if (y + 1 < graphSize && !grid[y,x].bottomWall)
                    {
                        mazeGraph.AddEdge(points[y, x], points[y + 1, x], 1); //Down edge
                    }
                    if (y - 1 >= 0 && !grid[y,x].topWall)
                    {
                        mazeGraph.AddEdge(points[y, x], points[y - 1, x], 1); //Up edge
                    }
                }
            }

            return mazeGraph;
        }
        public static double Manhattan(Vertex<(int,int)> node, Vertex<(int, int)> goal)
        {
            double dx = Math.Abs(node.Value.Item1 - goal.Value.Item1);
            double dy = Math.Abs(node.Value.Item2 - goal.Value.Item2); ;
            double dis = (dx + dy);
            return dis;
        }
        public static double Diagonal(Vertex<(int, int)> node, Vertex<(int, int)> goal)
        {

            double dx = Math.Abs(node.Value.Item1 - goal.Value.Item1);
            double dy = Math.Abs(node.Value.Item2 - goal.Value.Item2);
            double dis = (dx + dy) + (Math.Sqrt(2) - 2 * 1) * Math.Min(dx, dy);
            return dis;
        }

        public static double Euclidean(Vertex<(int, int)> node, Vertex<(int, int)> goal)
        {

            double dx = Math.Abs(node.Value.Item1 - goal.Value.Item1);
            double dy = Math.Abs(node.Value.Item2 - goal.Value.Item2);
            double dis = Math.Sqrt(dx * dx + dy * dy);
            return dis;
        }
        void mazeGeneration()
        {
            (int y, int x)[] UnionValues = new (int y, int x)[graphSize * graphSize];
            InitGrid();
            info.Clear();

            //Loop through the rows and cols and set up the UnionValues array
            int count = 0;
            for (int y = 0; y < graphSize; y++)
            {
                for (int x = 0; x < graphSize; x++)
                {
                    UnionValues[count] = (y, x);
                    count++;
                }

            }

            MazeUnion = new QuickUnion<(int y, int x)>(UnionValues);

            int unions = 0;
            while (unions != graphSize * graphSize - 1)
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

            ;
        }


        //Generate random 2d position

        //Generate random direction function

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Font");
            buttonTexture = Content.Load<Texture2D>("Box");
            arrowTexture = Content.Load<Texture2D>("arrow");
            downArrowTexture = Content.Load<Texture2D>("downArrow");
            redDotTexture = Content.Load<Texture2D>("reddot");
            greenDotTexture = Content.Load<Texture2D>("greendot");
            
            startPathButton = new Button(buttonTexture, new Vector2(totalGridSize + 23, 650), Color.White, font, "Path Start", new Vector2(0.27f, 0.27f), Color.Black);
            start = new Button(buttonTexture, new Vector2(totalGridSize + 23, 525), Color.White, font, "Maze Start", new Vector2(0.27f, 0.27f), Color.Black);
            upArrow = new Button(arrowTexture, new Vector2(totalGridSize + 110, 50), Color.White, font, "", new Vector2(0.34f, 0.34f), Color.Transparent);
            downArrow = new Button(downArrowTexture, new Vector2(totalGridSize + 110, 325), Color.White, font, "", new Vector2(0.34f, 0.34f), Color.Transparent);
            watch.Start();
            mazeSizeText = new Button(buttonTexture, new Vector2(totalGridSize + 23, 200), Color.White, font, $"{graphSize}", new Vector2(0.27f, 0.27f), Color.Black);

            redDot = new Button(redDotTexture, new Vector2(0,0), Color.White, font, "", new Vector2((float) sqSize/redDotTexture.Width 
                ,(float) sqSize/redDotTexture.Width ), Color.White);
            greenDot = new Button(greenDotTexture, new Vector2(0,0), Color.White, font, "", new Vector2((float)sqSize/greenDotTexture.Width 
                , (float)sqSize/greenDotTexture.Width  ), Color.White);


            manhattanButton = new Button(buttonTexture, new Vector2(totalGridSize + 15, 100), Color.White, font, "Manhattan", new Vector2(0.27f, 0.27f), Color.Black);
            diagonalButton = new Button(buttonTexture, new Vector2(totalGridSize + 15, 250), Color.White, font, "Diagonal", new Vector2(0.27f, 0.27f), Color.Black);
            euclideanButton = new Button(buttonTexture, new Vector2(totalGridSize + 15, 400), Color.White, font, "Euclidean", new Vector2(0.27f, 0.27f), Color.Black);
            loadPath = new Button(buttonTexture, new Vector2(totalGridSize + 15, 650), Color.White, font, "Load path", new Vector2(0.27f, 0.27f), Color.Black);

            upArrow.Tint = Color.FromNonPremultiplied(upArrow.Tint.R, upArrow.Tint.G, upArrow.Tint.B, 175);
            downArrow.Tint = Color.FromNonPremultiplied(downArrow.Tint.R, downArrow.Tint.G, downArrow.Tint.B, 175);
            startPathButton.Tint = Color.FromNonPremultiplied(start.Tint.R, start.Tint.G, start.Tint.B, 175);
            start.Tint = Color.FromNonPremultiplied(start.Tint.R, start.Tint.G, start.Tint.B, 175);
            mazeSizeText.Tint = Color.FromNonPremultiplied(mazeSizeText.Tint.R, mazeSizeText.Tint.G, mazeSizeText.Tint.B, 175);

             
        }

        Stopwatch watch = new Stopwatch();

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (startMaze)
            {
                for (int i = 0; i < (int)Math.Ceiling(0.2 * graphSize) && info.Count > 0; i++)
                {
                    var pop = info.Pop();
                    ProcessData(pop);
                }
                if (info.Count == 0)
                {
                    startMaze = false;


                    upArrow.Tint = Color.FromNonPremultiplied(upArrow.Tint.R, upArrow.Tint.G, upArrow.Tint.B, 175);

                    downArrow.Tint = Color.FromNonPremultiplied(downArrow.Tint.R, downArrow.Tint.G, downArrow.Tint.B, 175);
                }
            }

            MouseState ms = Mouse.GetState();
            if (startPathButton.IsClicked(ms))
            {
                grid[0, 0].Tint = Color.Green;
                startPathfinding = true;
                copyMaze(grid);
                InitGraphForPath();

                //add a start and end points
                //then when one of the buttons are clicked start


            }
            if (start.IsClicked(ms))
            {
                startMaze = true;
                mazeGeneration();
                
                upArrow.Tint = Color.FromNonPremultiplied(upArrow.Tint.R, upArrow.Tint.G, upArrow.Tint.B, 65);

                downArrow.Tint = Color.FromNonPremultiplied(downArrow.Tint.R, downArrow.Tint.G, downArrow.Tint.B, 65);
            }
            if (upArrow.IsClicked(ms))
            {
                if (!startMaze)
                {
                    if (graphSize < 100)
                    {
                        graphSize += 1;
                    }
                    mazeSizeText.Text = $"{graphSize}";
                    sqSize = totalGridSize / graphSize;
                    InitGrid();
                }
            }
            if (downArrow.IsClicked(ms))
            {
                if (!startMaze)
                {
                    if (graphSize > 1)
                    {
                        graphSize -= 1;
                    }
                    mazeSizeText.Text = $"{graphSize}";
                    sqSize = totalGridSize / graphSize;
                    InitGrid();
                }


            }

            if (startPathfinding == true)
            {
               
                for (int a = 0; a < graphSize; a++)
                {
                    for (int b = 0; b < graphSize; b++)
                    { 
                       if(graphForPath[a, b].LeftPressed(ms))
                       {
                            //set prev starttile false
                            startTile.startTile = false;
                            startTile = graphForPath[a, b];
                            graphForPath[a, b].startTile = true;
                            greenDot.Pos = graphForPath[a, b].Pos;
                       }

                       else if (graphForPath[a, b].RightPressed(ms))
                       {
                            //set prev endtile false
                            endTile.endTile = false;
                            endTile = graphForPath[a, b];
                            graphForPath[a, b].endTile = true;
                            redDot.Pos = graphForPath[a, b].Pos;
                       }

                    }
                }
                //get the start and end points
                //generate path
                //visualize path
                if (manhattanButton.IsClicked(ms))
                {
                    manhattan = true;
                    euclidean = false;
                    diagonal = false;
                }
                if (diagonalButton.IsClicked(ms))
                {
                    manhattan = false;
                    euclidean = false;
                    diagonal = true;
                }
                if (euclideanButton.IsClicked(ms))
                {
                    manhattan = false;
                    euclidean = true;
                    diagonal = false;
                }

                if (loadPath.IsClicked(ms))
                {
                    Graph<Vertex<(int, int)>> graph = copyMaze(grid);
                    Vertex<(int, int)> start = new Vertex<(int, int)>(((int)startTile.Pos.X, (int)startTile.Pos.Y));
                    Vertex<(int, int)> end = new Vertex<(int, int)>(((int)endTile.Pos.X, (int)endTile.Pos.Y));
                    if (manhattan)
                    {
                        path = graph.AStarPF(graph.Search(start));
                    }
                    else if (diagonal)
                    {
                        path = graph.AStarPF();
                    }
                    else if (euclidean)
                    {
                        path = graph.AStarPF();
                    }
                }
            
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
            if (startPathfinding == false)
            {
                start.Draw(spriteBatch);
                upArrow.Draw(spriteBatch);
                downArrow.Draw(spriteBatch);
                mazeSizeText.Draw(spriteBatch);
                startPathButton.Draw(spriteBatch);
            }
            else
            {
                redDot.Draw(spriteBatch);
                greenDot.Draw(spriteBatch);
                manhattanButton.Draw(spriteBatch);
                euclideanButton.Draw(spriteBatch);
                diagonalButton.Draw(spriteBatch);
                loadPath.Draw(spriteBatch);
            }
         
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
