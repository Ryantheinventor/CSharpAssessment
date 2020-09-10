using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace raygamecsharp.Pong
{
    class BackBoard : CollisionTestRectangle
    {
        int player = 1;
        public BackBoard(string name, Vector2 pos, Vector2 scale, int player) : base(name, pos, scale)
        {
            this.player = player;
        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.gameObject.name == "Ball")
            {
                foreach (GameObject g in objects)
                {
                    if (g.name == "Score1" && player == 1)
                    {
                        ((ScoreCounter)g).score++;
                    }
                    if (g.name == "Score2" && player == 2)
                    {
                        ((ScoreCounter)g).score++;
                    }
                    if (g.name.StartsWith("Player")) 
                    {
                        g.transform.translation.Y = 450;
                    }
                }
                ((Ball)other.gameObject).Reset();
                
            }
        }

    }
}
