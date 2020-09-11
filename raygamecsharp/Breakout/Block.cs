using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace raygamecsharp.Breakout
{
    class Block : GameObject
    {
        BreakoutScore scoreTracker;
        public Block(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(40, 20, 0);
        }


        public override void Start()
        {
            collider = new RectangleCollider();
            collider.IsStatic = true;
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            scoreTracker = (BreakoutScore)FindByName("Score");
        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.gameObject.name == "Ball") 
            {
                scoreTracker.AddPoints();
                Destroy(this);
            }
            
        }

        public override void Draw()
        {
            base.Draw();
            DrawRectangle((int)(transform.translation.X - transform.scale.X / 2), (int)(transform.translation.Y - transform.scale.Y / 2), (int)transform.scale.X, (int)transform.scale.Y, WHITE);
        }
    }
}
