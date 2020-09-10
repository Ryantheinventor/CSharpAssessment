﻿using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace raygamecsharp.Pong
{
    class ScoreCounter : GameObject
    {
        public int score = 0;
        public int player = 1;
        public ScoreCounter(string name, Vector2 pos, int player) : base(name, pos)
        {
            this.player = player;
        }


        public override void Draw()
        {
            DrawText($"{score}", (int)transform.translation.X, (int)transform.translation.Y, 50, WHITE);
        }

    }
}
