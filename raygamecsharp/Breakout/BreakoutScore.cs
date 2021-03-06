﻿using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;
using System.Collections.Generic;

namespace MiniAtariArcade.Breakout
{
    /// <summary>
    /// Displays the score for 
    /// </summary>
    class BreakoutScore : GameObject
    {
        float timeBonus = 100f;
        int pointsPerBlock = 50;
        public int realScore = 0;
        public int displayScore = 0;
        BreakoutBall ball;
        public BreakoutScore(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(20, 20, 0);
        }

        public override void Start()
        {
            base.Start();
            ball = (BreakoutBall)FindByName("Ball");
        }

        public override void Update()
        {
            if (ball.HasStarted && timeBonus > 0) 
            {
                timeBonus -= GetFrameTime();
            }
            if (timeBonus < 0) 
            {
                timeBonus = 0;
            }
        }

        /// <summary>
        /// Add points to score with time bonus calculated
        /// </summary>
        public int AddPoints() 
        {
            realScore += pointsPerBlock + (int)timeBonus;
            return pointsPerBlock + (int)timeBonus;
        }

        public override void Draw()
        {
            DrawText($"{realScore}", (int)transform.translation.X, (int)transform.translation.Y, 50, WHITE);
        }
    }
}
