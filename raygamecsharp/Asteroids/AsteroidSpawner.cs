﻿using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace MiniAtariArcade.Asteroids
{
    class AsteroidSpawner : GameObject
    {
        public int maxAsteroids = 30;
        //the largest number on asteroid textures (must be one for all sizes)
        public int asteroidVariants = 0;
        public int minAsteroidSpeed = 100;
        public int maxAsteroidSpeed = 200;
        Random random = new Random();
        public bool hasStarted = false;
        float spawnWaitTime = 5f;
        float curWaitTime = 0f;
        AsteroidsShip player;
        public AsteroidSpawner(string name, Vector2 pos) : base(name, pos)
        {

        }

        public override void Start()
        {
            base.Start();
            player = (AsteroidsShip)FindByName("Player");
        }

        public override void Update()
        {
            curWaitTime += GetFrameTime();
            if (!hasStarted)
            {
                hasStarted = (IsKeyDown(KeyboardKey.KEY_W) || IsKeyDown(KeyboardKey.KEY_D) || IsKeyDown(KeyboardKey.KEY_A) || IsKeyDown(KeyboardKey.KEY_SPACE));
            }
            if (curWaitTime > spawnWaitTime && objects.Count - 3 < maxAsteroids && hasStarted) 
            {
                Vector2 spawnPos = new Vector2(random.Next(0, 1600), random.Next(0, 900));
                while (spawnPos.X < player.transform.translation.X + 150 && spawnPos.X > player.transform.translation.X - 150) 
                {
                    spawnPos.X = random.Next(0, 1600);
                }
                string textureSize = "sml";
                int intSize = random.Next(0, 3);
                switch (intSize)
                {
                    case 0:
                        textureSize = "Sml";
                        break;
                    case 1:
                        textureSize = "Med";
                        break;
                    case 2:
                        textureSize = "Big";
                        break;
                }
                int variant = random.Next(0, asteroidVariants);


                Asteroid newAsteroid = new Asteroid("Asteroid", spawnPos, $"Asteroid{textureSize}{variant}", intSize);

                NewObject(newAsteroid);

                float speed = random.Next(minAsteroidSpeed,maxAsteroidSpeed);

                
                float angleRad = (float)random.NextDouble() * 2;
                newAsteroid.collider.Velocity.X = MathF.Cos(angleRad) * speed;
                newAsteroid.collider.Velocity.Y = MathF.Sin(angleRad) * speed;
                curWaitTime = 0;
            }
        }




    }
}