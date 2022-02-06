using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphVisualiz
{
    
    class Tile : Sprite
    {
        public bool startTile;
        public bool endTile;
        public bool wall;
        public bool clicked;
        public int SqSize;
        public Rectangle hitbox;
        public Tile(Texture2D texture, Vector2 pos, Color color, int sqSize, Rectangle? source = null, float rotation = 0) 
            : base(texture, pos, color, source, rotation)
        {
            SqSize = sqSize;
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, sqSize, sqSize);
            startTile = false;
            endTile = false;
            wall = false;
        }

        public void drawSquare(SpriteBatch spriteBatch, int lineSize)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, SqSize, SqSize), Tint);
            //top line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, SqSize, lineSize), Color.Black);
            //right line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, lineSize, SqSize), Color.Black);
            //bottom line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y + SqSize, SqSize, lineSize), Color.Black);
            //left line
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X + SqSize, (int)Pos.Y, lineSize, SqSize), Color.Black);

        }
        public void Pressed(MouseState ms)
        {
            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (hitbox.Contains(ms.Position))
                {
                    clicked = true;
                }
                else
                {
                    clicked = false;
                }
            }
            else
            {
                clicked = false;
            }
        }
        public void setWall()
        {
            Tint = Color.Gray;
            wall = true;
        }
        public void removeWall()
        {
            Tint = Color.White;
            wall = false;
        }

        public void setStart(MouseState ms, ButtonState prev)
        {
            if (ms.LeftButton == ButtonState.Pressed)
            {
                startTile = false;
                Tint = Color.White;
            }
            if(ms.LeftButton == ButtonState.Pressed && prev == ButtonState.Released)
            {


            }
        }
    }
}
