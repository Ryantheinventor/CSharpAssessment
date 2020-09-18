using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace MiniAtariArcade
{
    class PointPopUp : GameObject
    {
        int fontSize = 20;
        float alpha = 1f;
        int points = 0;
        public float moveUpSpeed = 10f;
        Color color = WHITE;
        public PointPopUp(string name, Vector2 pos, int points, Color color) : base(name, pos) 
        {
            this.points = points;
            this.color = color;
        }

        public override void Update()
        {
            alpha -= GetFrameTime();
            if (alpha <= 0) 
            {
                Destroy(this);
            }
            transform.translation.Y -= moveUpSpeed * GetFrameTime();
        }

        public override void Draw()
        {
            string text = $"+{points}";
            int textSize = MeasureText(text,fontSize);
            DrawText(text,(int)transform.translation.X - textSize/2, (int)transform.translation.Y,fontSize,Fade(color, alpha));
        }
    }
}
