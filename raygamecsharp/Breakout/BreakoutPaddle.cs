using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace MiniAtariArcade.Breakout
{
    class BreakoutPaddle : GameObject
    {
        public int speed = 550;
        public BreakoutPaddle(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(70, 20, 0);
        }

        public override void Start()
        {
            
            collider = new RectangleCollider();
            collider.IsStatic = true;
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            for (int x = 554; x <= 1046; x += 41) 
            {
                for (int y = 116; y <= 200; y += 21)
                {
                    NewObject(new Block("Block", new Vector2(x, y)));
                    
                }
                
            }

        }

        public override void Update()
        {
            base.Update();
            //move
            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                transform.translation.X -= speed * GetFrameTime();
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                transform.translation.X += speed * GetFrameTime();
            }

            //limit movement
            if (transform.translation.X > 1040)
            {
                transform.translation.X = 1040;
            }
            if (transform.translation.X < 560)
            {
                transform.translation.X = 560;
            }

        }

        public override void Draw()
        {
            base.Draw();
            DrawRectangle((int)(transform.translation.X - transform.scale.X / 2), (int)(transform.translation.Y - transform.scale.Y / 2), (int)transform.scale.X, (int)transform.scale.Y, WHITE);
        }

    }
}
