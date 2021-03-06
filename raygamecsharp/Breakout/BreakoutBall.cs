﻿using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using RGCore.RGPhysics;
using static RGCore.GameObjectList;
using static RGCore.Core_basic_window;
using System;

namespace MiniAtariArcade.Breakout
{
    class BreakoutBall : GameObject
    {
        public float speedMultiplier = 1.01f;
        public bool HasStarted = false;
        public float Speed = 500f;
        BreakoutPaddle player;
        BreakoutScore score;
        public BreakoutBall(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(20, 20, 0);
        }


        public override void Start()
        {
            collider = new RectangleCollider
            {
                Bounce = 1f,
                IsKinematic = true
            };
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            player = (BreakoutPaddle)FindByName("Player");
            score = (BreakoutScore)FindByName("Score");
        }

        public override void Update()
        {
            base.Update();
            if (IsKeyPressed(KeyboardKey.KEY_SPACE) && !HasStarted)//start the game after pressing space
            {
                HasStarted = true;
                Random random = new Random();
                float xValue = random.Next(60, 75) / 100f;
                float yValue = MathF.Sqrt(1f - xValue) * -1;
                if (random.Next(0, 2) == 0)
                {
                    xValue *= -1;
                }
                collider.Velocity = new Vector2(xValue * Speed, yValue * Speed);
            }
            
        }

        public override void PhysicsUpdate()
        {
            //keep the ball on the paddle before the game starts
            if (!HasStarted)
            {
                transform.translation = new Vector3(player.transform.translation.X,player.transform.translation.Y-20,0);
            }
        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.gameObject.name == "Floor")//end the game
            {
                if (savedData.BreakoutScore < score.realScore)//new high score
                {
                    LastGame = "Breakout";
                    LastScore = score.realScore;
                    LoadScene(1);
                }
                else //no new high score
                {
                    LoadScene(0);
                }
                
            }
            //play bounce sounds
            if (other.gameObject.name == "Wall")
            {
                PlaySound(sounds["pongBounce2"]);
            }
            if (other.gameObject.name == "Player")
            {
                PlaySound(sounds["pongBounce1"]);
            }
            //slowly increase speed
            if (other.gameObject.name == "Block")
            {
                collider.Velocity *= speedMultiplier;
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawCircle((int)transform.translation.X, (int)transform.translation.Y, transform.scale.X / 2, WHITE);
        }
    }
}
