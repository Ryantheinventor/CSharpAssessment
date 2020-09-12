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
    class Asteroid : Sprite
    {
        AsteroidsScore score;
        Random random = new Random();
        int size = 1;
        string textureName = "";
        AsteroidSpawner spawner;
        public Asteroid(string name, Vector2 pos, string textureName ,int size) : base(name, pos)
        {
            this.textureName = textureName;
            this.size = size;
        }


        public override void Start()
        {
            SetSprite(textures[textureName], 1, 1);
            transform.scale = new Vector3(texture.width, texture.height, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            spawner = (AsteroidSpawner)FindByName("Spawner");
            score = (AsteroidsScore)FindByName("Score");

        }
        public override void PhysicsUpdate()
        {
            transform.translation += new Vector3(collider.Velocity, 0) * GetFrameTime();
            //screen wrap
            if (transform.translation.X > 1650)
            {
                transform.translation.X = -30;
            }
            if (transform.translation.X < -50)
            {
                transform.translation.X = 1630;
            }
            if (transform.translation.Y > 950)
            {
                transform.translation.Y = -30;
            }
            if (transform.translation.Y < -50)
            {
                transform.translation.Y = 930;
            }

        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.gameObject.name == "Bullet") 
            {
                switch (size) 
                {
                    case 0:
                        score.score += 15;
                        break;
                    case 1:
                        score.score += 10;
                        break;
                    case 2:
                        score.score += 5;
                        break;
                }
                if (size > 0)
                {
                    Vector2 spawnPos = new Vector2(transform.translation.X, transform.translation.Y);
                    string textureSize = "Sml";
                    size--;
                    switch (size)
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
                    for (int i = 0; i < 2; i++)
                    {
                        int variant = random.Next(0, spawner.asteroidVariants);
                        Asteroid newAsteroid = new Asteroid("Asteroid", spawnPos, $"Asteroid{textureSize}{variant}", size);
                        NewObject(newAsteroid);
                        float speed = random.Next(spawner.minAsteroidSpeed, spawner.maxAsteroidSpeed);
                        float angleRad = (float)random.NextDouble() * 2;
                        newAsteroid.collider.Velocity.X = MathF.Cos(angleRad) * speed;
                        newAsteroid.collider.Velocity.Y = MathF.Sin(angleRad) * speed;
                    }
                }
                Destroy(this);
                Destroy(other.gameObject);
            }
        }

    }
}
