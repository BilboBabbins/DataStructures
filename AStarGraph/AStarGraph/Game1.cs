
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using WeightedDirectGraphs;

namespace GraphVisualiz
{
    public class Game1 : Game
    {
        
        //make side screen with different algorithm buttons
        //after pressing it, displays the pathfinding 


        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Graph<point> gridGraph;
        LinkedList<Vertex<point>> path = new LinkedList<Vertex<point>>();
        LinkedList<Vertex<point>> visited = new LinkedList<Vertex<point>>();
        LinkedList<Vertex<point>> queued = new LinkedList<Vertex<point>>();
        const int graphSize = 30;
        const int screenSize = 1000;
        const int sqSize = screenSize / graphSize;
        Texture2D pixel;
        Texture2D blueButton;
        ButtonState prev = ButtonState.Released;
        bool setEnd = false;
        bool setStart = false;
        Tile[,] graph = new Tile[graphSize, graphSize];
        Button Manhattan;
        Button Diagonal;
        Button Euclidean;
        Button Start;
        int type = 0;
        point startPos;
        point endPos;
        //manhattan 1, diagonal 2, eulciicdan 3
        SpriteFont font;

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
            graphics.PreferredBackBufferWidth = screenSize + 400;
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
            endPos = new point(graphSize - 1, graphSize - 1);


            gridGraph = new Graph<point>();
            //initalizing grid graph
            //for (int a = 0; a < graphSize; a++)
            //{
            //    for (int b = 0; b < graphSize; b++)
            //    {
            //        gridGraph.AddVertex(new Vertex<point>(new point(a, b)));
            //    }
            //}

            //adding edges
            /*Vertex<point>[,] points = new Vertex<point>[graphSize, graphSize];
            for (int a = 0; a < graphSize; a++)
            {
                int prevY = 0;
                for (int b = 0; b < graphSize - 1; b++)
                {
                    gridGraph.AddEdge(points[a, prevY], points[a, b + 1]);
                    prevY = b + 1;
                }
            }
            for (int a = 0; a < graphSize; a++)
            {
                int prevX = 0;
                for (int b = 0; b < graphSize - 1; b++)
                {
                    gridGraph.AddEdge(points[prevX, a], points[b + 1, a]);
                    prevX = b + 1;
                }
            }

            for (int a = graphSize; a > 0; a--)
            {
                int prevY = graphSize - 1;
                for (int b = graphSize; b > 0; b--)
                {

                    gridGraph.AddEdge(points[a - 1, prevY], points[a - 1, b - 1]);
                    prevY = b - 1;
                }
            }
            for (int a = graphSize; a > 0; a--)
            {
                int prevX = graphSize - 1;
                for (int b = graphSize; b > 0; b--)
                {

                    gridGraph.AddEdge(points[prevX, a - 1], points[b - 1, a - 1]);
                    prevX = b - 1;
                }
            }*/
            WeightedDirectGraphs.Program.Generate(gridGraph ,graphSize, graphSize);
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");
            blueButton = Content.Load<Texture2D>("blueButton");
            Manhattan = new Button(blueButton, new Vector2(screenSize + 65, 10), Color.White, font, "Manhattan", Color.White);
            Diagonal = new Button(blueButton, new Vector2(screenSize + 65, 210), Color.White, font, "Diagonal", Color.White);
            Euclidean = new Button(blueButton, new Vector2(screenSize + 65, 410), Color.White, font, "Euclidean", Color.White);
            Start = new Button(blueButton, new Vector2(screenSize + 65, 610), Color.Green, font, "Start", Color.GreenYellow);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
           
            MouseState ms = Mouse.GetState();
            Manhattan.Pressed(ms);
            Diagonal.Pressed(ms);
            Euclidean.Pressed(ms);
            Start.Pressed(ms);

            if (Manhattan.clicked == true)
            {
                type = 1;
            }
            if (Diagonal.clicked == true)
            {
                type = 2;
            }
            if (Euclidean.clicked == true)
            {
                type = 3;
            }


            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
          
                    graph[a, b].Pressed(ms);
                    //setting end
                    if (setEnd == true && graph[a, b].startTile == false && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].endTile = true;
                        graph[a, b].Tint = Color.Red;
                        graph[a, b].wall = false;
                        endPos = new point(a, b);
                        setEnd = false;
                    }
                    //setting start
                    else if (setStart == true && graph[a, b].endTile == false && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].startTile = true;
                        graph[a, b].Tint = Color.Green;
                        graph[a, b].wall = false;
                        startPos = new point(a, b);
                        setStart = false;
                    }
                    //setting wall
                    else if (graph[a, b].startTile == false && graph[a, b].endTile == false
                        && graph[a, b].wall == false && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].setWall();
                        Vertex<point> blocked = gridGraph.Search(new point(a, b));
                        blocked.blocked = true;
                    }
                    //removing wall
                    else if (graph[a, b].startTile == false && graph[a, b].endTile == false
                         && graph[a, b].wall == true && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        graph[a, b].removeWall();
                        Vertex<point> blocked = gridGraph.Search(new point(a, b));
                        blocked.blocked = false;
                       
                    }
                    //removing start
                    else if (graph[a, b].startTile == true && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        setStart = true;
                        graph[a, b].Tint = Color.White;
                        graph[a, b].startTile = false;
                    }
                    //removing end
                    else if (graph[a, b].endTile == true && graph[a, b].clicked == true && prev == ButtonState.Released)
                    {
                        setEnd = true;
                        graph[a, b].Tint = Color.White;
                        graph[a, b].endTile = false;
                    }
              
                }
            }
            /*for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
                    if (graph[a, b].wall == true)
                    {
                        //gridGraph(a, b) = null;
                    }
                    if (graph[a, b].endTile == true)
                    {

                    }
                    if (graph[a, b].startTile == true)
                    {

                    }
                }
            }*/

            //buttons updates
            if (Start.clicked == true)
            {

               

                for (int a = 0; a < graphSize; a++)
                {
                    for (int b = 0; b < graphSize; b++)
                    {
                        if (graph[a, b].startTile != true && graph[a, b].endTile != true && graph[a, b].wall != true)
                        {
                            graph[a, b].Tint = Color.White;
                        }

                    }

                }
                //display visited and q in alg

                 
                //run astar and display
                if (type == 1)
                {
                   
                    var result = gridGraph.AStarPFVisualizer(gridGraph.Search(startPos), gridGraph.Search(endPos), WeightedDirectGraphs.Program.Manhattan);
                    path = result.path;
                    queued = result.queued;
                    visited = result.visited;
                   
                    var headQue = queued.First;
                    var headVisit = visited.First;

                    while (headQue != null && headQue.Value != gridGraph.Search(endPos))
                    {
                        if (headQue.Value != gridGraph.Search(startPos))
                        {
                            graph[(int)headQue.Value.Value.x, (int)headQue.Value.Value.y].Tint = Color.LightGreen;
                        }
                        headQue = headQue.Next;
                    }
                    while (headVisit != null && headVisit.Value != gridGraph.Search(endPos))
                    {
                        if (headVisit.Value != gridGraph.Search(startPos))
                        {
                            graph[(int)headVisit.Value.Value.x, (int)headVisit.Value.Value.y].Tint = Color.CornflowerBlue;
                        }
                         
                        headVisit = headVisit.Next;
                    }
                    if (path == null)
                    {
                        headVisit = visited.First;
                        while (headVisit != null && headVisit.Value != gridGraph.Search(endPos))
                        {
                            if (headVisit.Value != gridGraph.Search(startPos))
                            {
                                graph[(int)headVisit.Value.Value.x, (int)headVisit.Value.Value.y].Tint = Color.Purple;
                            }

                            headVisit = headVisit.Next;
                        }

                    }
                    else
                    {
                        var headPath = path.First.Next;
                        while (headPath.Value != gridGraph.Search(endPos))
                        {
                            //set graph path to a color
                            graph[(int)headPath.Value.Value.x, (int)headPath.Value.Value.y].Tint = Color.Coral;
                            headPath = headPath.Next;

                        }

                    }

                }
                else if (type == 2)
                {
                    var result = gridGraph.AStarPFVisualizer(gridGraph.Search(startPos), gridGraph.Search(endPos), WeightedDirectGraphs.Program.Diagonal);
                    path = result.path;
                    queued = result.queued;
                    visited = result.visited;

                    var headQue = queued.First;
                    var headVisit = visited.First;

                    while (headQue != null && headQue.Value != gridGraph.Search(endPos))
                    {
                        if (headQue.Value != gridGraph.Search(startPos))
                        {
                            graph[(int)headQue.Value.Value.x, (int)headQue.Value.Value.y].Tint = Color.LightGreen;
                        }
                        headQue = headQue.Next;
                    }
                    while (headVisit != null && headVisit.Value != gridGraph.Search(endPos))
                    {
                        if (headVisit.Value != gridGraph.Search(startPos))
                        {
                            graph[(int)headVisit.Value.Value.x, (int)headVisit.Value.Value.y].Tint = Color.CornflowerBlue;
                        }

                        headVisit = headVisit.Next;
                    }
                    if (path == null)
                    {
                        headVisit = visited.First;
                        while (headVisit != null && headVisit.Value != gridGraph.Search(endPos))
                        {
                            if (headVisit.Value != gridGraph.Search(startPos))
                            {
                                graph[(int)headVisit.Value.Value.x, (int)headVisit.Value.Value.y].Tint = Color.Purple;
                            }

                            headVisit = headVisit.Next;
                        }

                    }
                    else
                    {
                        var headPath = path.First.Next;
                        while (headPath.Value != gridGraph.Search(endPos))
                        {
                            //set graph path to a color
                            graph[(int)headPath.Value.Value.x, (int)headPath.Value.Value.y].Tint = Color.Coral;
                            headPath = headPath.Next;

                        }

                    }
                }
                else if (type == 3)
                {
                    var result = gridGraph.AStarPFVisualizer(gridGraph.Search(startPos), gridGraph.Search(endPos), WeightedDirectGraphs.Program.Euclidean);
                    path = result.path;
                    queued = result.queued;
                    visited = result.visited;
                 
                    var headQue = queued.First;
                    var headVisit = visited.First;

                    while (headQue != null && headQue.Value != gridGraph.Search(endPos))
                    {
                        if (headQue.Value != gridGraph.Search(startPos))
                        {
                            graph[(int)headQue.Value.Value.x, (int)headQue.Value.Value.y].Tint = Color.LightGreen;
                        }
                        headQue = headQue.Next;
                    }
                    while (headVisit != null && headVisit.Value != gridGraph.Search(endPos))
                    {
                        if (headVisit.Value != gridGraph.Search(startPos))
                        {
                            graph[(int)headVisit.Value.Value.x, (int)headVisit.Value.Value.y].Tint = Color.CornflowerBlue;
                        }

                        headVisit = headVisit.Next;
                    }
                    if (path == null)
                    {
                        headVisit = visited.First;
                        while (headVisit != null && headVisit.Value != gridGraph.Search(endPos))
                        {
                            if (headVisit.Value != gridGraph.Search(startPos))
                            {
                                graph[(int)headVisit.Value.Value.x, (int)headVisit.Value.Value.y].Tint = Color.Purple;
                            }

                            headVisit = headVisit.Next;
                        }

                    }
                    else
                    {
                        var headPath = path.First.Next;
                        while (headPath.Value != gridGraph.Search(endPos))
                        {
                            //set graph path to a color
                            graph[(int)headPath.Value.Value.x, (int)headPath.Value.Value.y].Tint = Color.Coral;
                            headPath = headPath.Next;

                        }

                    }
                }

            }
            


            Window.Title = $"{ms.Position}";
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


            //Texture2D temp = new Texture2D(GraphicsDevice, 1, 1);
            //temp.SetData(new Color[] { Color.White });
            for (int a = 0; a < graphSize; a++)
            {
                for (int b = 0; b < graphSize; b++)
                {
                    graph[a, b].drawSquare(spriteBatch, 3);
                    
                }
               
            }
            Manhattan.Draw(spriteBatch);
            Diagonal.Draw(spriteBatch);
            Euclidean.Draw(spriteBatch);
            Start.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "This is a sentence.", new Vector2(100, 100), Color.Red);

            //spriteBatch.Draw(temp, graph[5, 2].hitbox, Color.Red);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
