using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using RGCore.RGPhysics;
using static RGCore.GameObjectList;
using System;
using System.Collections.Generic;
namespace MiniAtariArcade
{
    class Text : GameObject
    {
        public int fontSize = 10;
        public string content = "";
        public Color color = WHITE;
        public Text(string name, Vector2 pos, string content, int fontSize, Color color) : base(name, pos) 
        {
            this.color = color;
            this.fontSize = fontSize;
            this.content = content;
        }

        public override void Draw()
        {
            base.Draw();
            DrawText(content, (int)transform.translation.X, (int)transform.translation.Y, fontSize, WHITE);
        }

    }
}
