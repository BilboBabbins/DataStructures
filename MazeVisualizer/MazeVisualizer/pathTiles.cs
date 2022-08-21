using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MazeVisualizer
{
    class pathTiles
    {



        public bool wall;
        public bool clicked;
        public bool visited;
        public int SqSize;
        public Rectangle hitbox;
        public bool path = false;
        public Vector2 Pos;
        public (int, int) graphPos;
        public pathTiles(Vector2 pos, int sqSize)

        {
            Pos = pos;
            SqSize = sqSize;
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, sqSize, sqSize);

            wall = false;
            visited = false;
        }


        public bool RightPressed(MouseState ms)
        {
            if (ms.RightButton == ButtonState.Pressed)
            {
                if (hitbox.Contains(ms.Position))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool LeftPressed(MouseState ms)
        {
            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (hitbox.Contains(ms.Position))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



          }
    }

