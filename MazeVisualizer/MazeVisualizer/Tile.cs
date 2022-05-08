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
            rightWall = true;
            leftWall = true;
            topWall = true;
            bottomWall = true;
        }
        public void drawSquare(SpriteBatch spriteBatch, int lineSize)
        {
            if (topWall)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, SqSize, lineSize), Color.Black);
                //top line
            }
            if (rightWall)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Pos.X + SqSize, (int)Pos.Y, lineSize, SqSize), Color.Black);
                //right line
            }
            if (bottomWall)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y + SqSize, SqSize, lineSize), Color.Black);
                //bottom line
            }
            if (leftWall)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, lineSize, SqSize), Color.Black);
                //left line
            }
           

        }
    }
}
