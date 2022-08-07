using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeVisualizer
{
    class Button : Sprite
    {
        public SpriteFont Label;
        public string Text;
        public Color TextColor;
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, (int)(Texture.Width * Scale.X), (int)(Texture.Height * Scale.Y));
            }
        }


        public Button(Texture2D texture, Vector2 pos, Color color, SpriteFont label, string text, Vector2 scale, Color textColor, Rectangle? source = null, float rotation = 0)
              : base(texture, pos, color, scale, source, rotation)
        {
            //hitbox = new Rectangle((int)pos.X, (int)pos.Y, ;
            Label = label;
            Text = text;
            TextColor = textColor;
        }

        public bool IsClicked(MouseState ms)
        {

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (HitBox.Contains(ms.Position))
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Vector2 textPos = Label.MeasureString(Text);
            spriteBatch.DrawString(Label, $"{Text}", new Vector2(Pos.X + (Texture.Width * Scale.X / 2) - (textPos.X / 2), Pos.Y + (Texture.Height * Scale.Y / 2) - (textPos.Y / 2)), TextColor);
        }
    }
}
