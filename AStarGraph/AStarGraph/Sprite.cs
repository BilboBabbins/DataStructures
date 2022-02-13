using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphVisualiz
{
    class Sprite
    {
        //? = turn nullable type
        public Texture2D Texture { get; set; }
        public Vector2 Pos { get; set; }
        public Rectangle? Source { get; set; }
        public Color Tint { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; }

        public Sprite(Texture2D texture, Vector2 pos, Color color,
            Rectangle? source = null, float rotation = 0)
        {
            Texture = texture;
            Pos = pos;
            Tint = color;
            Source = source;
            Rotation = rotation;
            Origin = new Vector2(0, 0);
            Scale = new Vector2(1, 1);
            Effects = SpriteEffects.None;
            LayerDepth = 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Pos, Source, Tint, Rotation, Origin, Scale,
                Effects, LayerDepth);
        }

        


    }
}
