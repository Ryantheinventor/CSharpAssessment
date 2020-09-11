using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace raygamecsharp.Pong
{
    class PongBall : GameObject
    {
        private float waitTime = 1.5f;
        private float timeSinceReset = 1.5f;
        public bool HasStarted = false;
        public float Speed = 1000f;
        public PongBall(string name, Vector2 pos) : base(name,pos)
        {
            transform.scale = new Vector3(40, 40, 0);
        }


        public override void Start()
        {
            collider = new RectangleCollider();
            collider.Bounce = 1f;
            collider.IsKinematic = true;
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            
        }

        public override void Update()
        {
            base.Update();
            if (IsKeyPressed(KeyboardKey.KEY_SPACE) && !HasStarted)//start the game after pressing space
            {
                Reset();
                HasStarted = true;
                timeSinceReset = waitTime;
            }
            if (timeSinceReset < waitTime) 
            {
                timeSinceReset += GetFrameTime();
            }
        }

        public void Reset() 
        {
            Random random = new Random();
            float xValue = random.Next(75, 85) / 100f;
            float yValue = MathF.Sqrt(1f - xValue);
            if (random.Next(0, 2) == 0)
            {
                xValue *= -1;
            }
            if (random.Next(0, 2) == 0)
            {
                yValue *= -1;
            }
            transform.translation = new Vector3(800,450,0);
            collider.Velocity = new Vector2(xValue*Speed, yValue*Speed);
            timeSinceReset = 0;
        }

        public override void PhysicsUpdate()
        {
            if (timeSinceReset < waitTime) 
            {
                transform.translation = new Vector3(800, 450, 0);
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawCircle((int)transform.translation.X, (int)transform.translation.Y, transform.scale.X / 2, WHITE);
        }

        

    }
}
