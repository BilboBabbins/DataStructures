using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace MazeVisualizer
{
    class Tile : Sprite
    {

        public int SqSize;

        public bool rightWall;
        public bool leftWall;
        public bool topWall;
        public bool bottomWall;

        public Tile(Texture2D texture, Vector2 pos, Color color, int sqSize) 
            : base(texture, pos, color)
        {
            SqSize = sqSize;
        }
        public void drawSquare(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, SqSize, SqSize), Tint);
            //top line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, SqSize, SqSize), Color.Black);
            //right line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, SqSize, SqSize), Color.Black);
            //bottom line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y + SqSize, SqSize, SqSize), Color.Black);
            //left line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X + SqSize, (int)Pos.Y, SqSize, SqSize), Color.Black);

        }
    }
}
