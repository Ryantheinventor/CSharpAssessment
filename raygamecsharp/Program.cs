/*******************************************************************************************
*
*   raylib [core] example - Basic window
*
*   Welcome to raylib!
*
*   To test examples, just press F6 and execute raylib_compile_execute script
*   Note that compiled executable is placed in the same folder as .c file
*
*   You can find all basic examples on C:\raylib\raylib\examples folder or
*   raylib official webpage: www.raylib.com
*
*   Enjoy using raylib. :)
*
*   This example has been created using raylib 1.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2013-2016 Ramon Santamaria (@raysan5)
*
********************************************************************************************/


using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using static MiniAtariArcade.ScoreSaver;
using System.Numerics;
using RGCore.RGPhysics;
using System;
using MiniAtariArcade;
using System.Collections.Generic;

namespace RGCore
{
    public class Core_basic_window
    {
        public static string LastGame = "";
        public static int LastScore = 0;
        public static SavedData savedData;
        static Vector2 cameraPos = new Vector2(0,0);
        public static bool showDebug1 = false;
        public static bool showDebug2 = false;

        public static void Start()
        {
            //call all start functions in GameObjects
            foreach (GameObject g in objects) 
            {
                g.Start();
            }
        }

        public static void Update() 
        {
            //Debug
            if (IsKeyPressed(KeyboardKey.KEY_LEFT_BRACKET))
            {
                showDebug1 = !showDebug1;
            }
            if (IsKeyPressed(KeyboardKey.KEY_RIGHT_BRACKET))
            {
                showDebug2 = !showDebug2;
            }
            if (IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                LoadScene(0);
            }
            if (IsKeyPressed(KeyboardKey.KEY_TWO))
            {
                LoadScene(2);
            }
            if (IsKeyPressed(KeyboardKey.KEY_THREE))
            {
                LoadScene(3);
            }
            if (IsKeyPressed(KeyboardKey.KEY_FOUR))
            {
                LoadScene(4);
            }

            //Run all update functions
            foreach (GameObject g in objects)
            {
                g.Update();
            }

            //Physics update
            Physics.Update();

            //Run all physics update functions
            foreach (GameObject g in objects)
            {
                g.PhysicsUpdate();
            }

            //Load queue in main object list
            UpdateObjectList();

        }

        public static void Draw() 
        {
            ClearBackground(BLACK);
            //Draws all GameObjects
            foreach (GameObject g in objects)
            {
                g.Draw();
            }

            //Debug
            if (showDebug1) {
                DrawRectangle(0, 0, 400, 900, Fade(RED, 0.5f));
                DrawText(GetFPS().ToString(), 10, 10, 20, GREEN);
                DrawText($"Object Count:{objects.Count}", 10, 40, 20, GREEN);
                DrawText(GetObjectListString(), 10, 60, 10, GREEN);
            }
            if (showDebug2) 
            {
                foreach (GameObject g in objects)
                {
                    if (g.collider != null)
                    {
                        g.collider.Draw();
                    }
                }
            }
        }

        public static int Main()
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            const int screenWidth = 1600;
            const int screenHeight = 900;

            InitWindow(screenWidth, screenHeight, "Raylib Game C#");
            InitAudioDevice();
            SetTargetFPS(60);
            LoadTextures();
            LoadSounds();
            LoadSavedData();
            SetMasterVolume(savedData.Volume);



            LoadScene(0);
            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                Update();
                BeginDrawing();
                Draw();
                EndDrawing();
            }
            SaveScores();
            // De-Initialization
            //--------------------------------------------------------------------------------------
            CloseWindow();        // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }
    }
}