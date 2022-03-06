using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphVisualiz
{
    class Button : Sprite
    {
        public bool clicked;
        public SpriteFont Label;
        public string Text;
        public Color TextColor;
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, Texture.Width, Texture.Height);
            }
        }


        public Button(Texture2D texture, Vector2 pos, Color color, SpriteFont label, string text, Color textColor, Rectangle? source = null, float rotation = 0)
              : base(texture, pos, color, source, rotation)
        {
            //hitbox = new Rectangle((int)pos.X, (int)pos.Y, ;
            Label = label;
            Text = text;
            TextColor = textColor;
        }

        public void Pressed(MouseState ms)
        {

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (HitBox.Contains(ms.Position))
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Vector2 textSize = Label.MeasureString(Text);
            spriteBatch.DrawString(Label, $"{Text}", new Vector2(Pos.X + (Texture.Width/2) - (textSize.X/2), Pos.Y + (Texture.Height/2) - (textSize.Y/2)), TextColor);
        }
    }
}
