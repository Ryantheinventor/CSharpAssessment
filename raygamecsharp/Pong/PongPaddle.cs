using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace raygamecsharp.Pong
{
    class PongPaddle : GameObject
    {
        int player = 1;
        public int speed = 700;
        public PongPaddle(string name, Vector2 pos, int player) : base(name, pos)
        {
            this.player = player;
            transform.scale = new Vector3(40, 120, 0);
        }

        public override void Start()
        {
            collider = new RectangleCollider();
            collider.IsStatic = true;
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            if (player == 1)
            {
                if (IsKeyDown(KeyboardKey.KEY_W))
                {
                    transform.translation.Y -= speed * GetFrameTime();
                }
                if (IsKeyDown(KeyboardKey.KEY_S))
                {
                    transform.translation.Y += speed * GetFrameTime();
                }
            }
            if (player == 2)
            {
                if (IsKeyDown(KeyboardKey.KEY_UP))
                {
                    transform.translation.Y -= speed * GetFrameTime();
                }
                if (IsKeyDown(KeyboardKey.KEY_DOWN))
                {
                    transform.translation.Y += speed * GetFrameTime();
                }
            }


            if (transform.translation.Y > 800)
            {
                transform.translation.Y = 800;
            }
            if (transform.translation.Y < 100)
            {
                transform.translation.Y = 100;
            }

        }

        public override void Draw()
        {
            base.Draw();
            DrawRectangle((int)(transform.translation.X- transform.scale.X / 2), (int)(transform.translation.Y - transform.scale.Y / 2), (int)transform.scale.X, (int)transform.scale.Y, WHITE);
        }

    }
}
