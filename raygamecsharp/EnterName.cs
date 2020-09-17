using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using RGCore.RGPhysics;
using static RGCore.GameObjectList;
using static RGCore.Core_basic_window;
using System;
using System.Collections.Generic;

namespace MiniAtariArcade
{
    class EnterName : GameObject
    {
        string playerName = "AAA";
        public EnterName(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(20, 20, 0);
        }

        public override void Start()
        {
            base.Start();
        }

        //a-z = 97-122
        public override void Update()
        {
            int key = GetKeyPressed();
            if (key >= 97 && key <= 122) 
            {
                string input = "" + (char)key;
                playerName = (playerName.Substring(1) + input).ToUpper();
                Console.WriteLine(playerName);
            }
            if (IsKeyPressed(KeyboardKey.KEY_ENTER)) 
            {
                if (LastGame == "Breakout") 
                {
                    savedData.BreakoutName = playerName;
                    savedData.BreakoutScore = LastScore;
                }
                if (LastGame == "Asteroids")
                {
                    savedData.AsteroidsName = playerName;
                    savedData.AsteroidsScore = LastScore;
                }

                LoadScene(0);
            }

        }

        public override void Draw()
        {
            base.Draw();
            DrawText(playerName, (int)transform.translation.X, (int)transform.translation.Y, 150, WHITE);
        }

    }




}
