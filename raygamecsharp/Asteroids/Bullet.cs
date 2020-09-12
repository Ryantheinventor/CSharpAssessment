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
    class Bullet : GameObject
    {
        public Bullet(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(5, 5, 0);

        }


        public override void Start()
        {
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
        }

        public override void PhysicsUpdate()
        {
            transform.translation += new Vector3(collider.Velocity, 0) * GetFrameTime();

            if (transform.translation.X > 1650 || transform.translation.X < -50 || transform.translation.Y > 950 || transform.translation.Y < -50)
            {
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
