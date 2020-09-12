using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using static RGCore.Core_basic_window;
using System.Numerics;
using System;

namespace MiniAtariArcade
{
    class SceneButtonScore : LoadSceneButton
    {
        string game = "";

        public SceneButtonScore(string name, Vector2 pos, string textureName, int scene, string game) : base(name, pos, textureName, scene)
        {
            this.game = game;
        }

        public override void Draw()
        {
            base.Draw();
            if (game == "Asteroids" && savedScores.AsteroidsScore > 0) {
                DrawText($"{savedScores.AsteroidsName}:{savedScores.AsteroidsScore}",(int)(transform.translation.X-transform.scale.X/2), (int)(transform.translation.Y + transform.scale.Y / 2)+5, 40, WHITE);
            }
            else if (game == "Breakout" && savedScores.BreakoutScore > 0)
            {
                DrawText($"{savedScores.BreakoutName}:{savedScores.BreakoutScore}", (int)(transform.translation.X - transform.scale.X / 2), (int)(transform.translation.Y + transform.scale.Y / 2) + 5, 40, WHITE);
            }

        }


    }
}
