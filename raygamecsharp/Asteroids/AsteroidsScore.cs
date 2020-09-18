using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace MiniAtariArcade.Asteroids
{
    class AsteroidsScore : GameObject
    {
        public int score = 0;

        public AsteroidsScore(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(20, 20, 0);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Draw()
        {
            base.Draw();
            //this object only draws score is added by the asteroids
            DrawText($"Score:{score}",(int)transform.translation.X, (int)transform.translation.Y,45,WHITE);
        }

    }
}
