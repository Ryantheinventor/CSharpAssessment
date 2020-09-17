using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using static RGCore.Core_basic_window;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace MiniAtariArcade.Asteroids
{
    class AsteroidsShip : Sprite
    {
        int life = 3;

        string textureName = "";
        float shipMaxSpeed = 350f;
        float shipAcceleration = 250f;
        int rotationSpeed = 360;
        float bulletSpeed = 600f;

        float bulletWaitTime = 0.3f;
        float curWaitTime = 0f;
        AsteroidsScore score;
        Vector2 pointDirection = new Vector2();//a vector 1 unit away from and in front of origin 

        bool inThrustAnim = false;

        public AsteroidsShip(string name, Vector2 pos, string textureName) : base(name, pos)
        {
            this.textureName = textureName;
            //transform.scale = new Vector3(texture.width, texture.height, 0);
            color = WHITE;
        }
        public override void Start()
        {
            SetSprite(textures[textureName], 1, 1);
            transform.scale = new Vector3(texture.width, texture.height, 0);
            collider = new RectangleCollider();
            collider.IsKinematic = true;
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            score = (AsteroidsScore)FindByName("Score");

        }

        public override void Update()
        {
            if (IsKeyDown(KeyboardKey.KEY_A)) 
            {
                transform.rotation.Z -= rotationSpeed * GetFrameTime();
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                transform.rotation.Z += rotationSpeed * GetFrameTime();
            }

            //
            if (transform.rotation.Z <= -1)
            {
                transform.rotation.Z = 360 + transform.rotation.Z;
            }
            if (transform.rotation.Z >= 360)
            {
                transform.rotation.Z = 0 + (transform.rotation.Z-360);
            }


            UpdatePointDirection();
            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                //apply force in poit direction
                collider.Velocity -= pointDirection * (shipAcceleration * GetFrameTime());
                float speed = MathF.Abs(MathF.Sqrt(MathF.Pow(collider.Velocity.X, 2) + MathF.Pow(collider.Velocity.Y, 2)));
                if (speed > shipMaxSpeed)
                {
                    float maxSpeedPercent = shipMaxSpeed / speed;
                    collider.Velocity.X *= maxSpeedPercent;
                    collider.Velocity.Y *= maxSpeedPercent;
                }

                if (!IsSoundPlaying(sounds["thrust"]))
                    PlaySound(sounds["thrust"]);
                if (!inThrustAnim)
                {
                    SetSprite(textures["ShipThrust"], 2, 0.1f);
                    inThrustAnim = true;
                }
            }
            else 
            {
                SetSprite(textures[textureName], 1, 1);
                inThrustAnim = false;
            }
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
            if (transform.translation.Y  < -50)
            {
                transform.translation.Y = 930;
            }

            if (curWaitTime < bulletWaitTime)
            {
                curWaitTime += GetFrameTime();
            }

            //fire weapon
            if (IsKeyPressed(KeyboardKey.KEY_SPACE) && curWaitTime >= bulletWaitTime) 
            {
                Bullet newBullet = new Bullet("Bullet",new Vector2(transform.translation.X,transform.translation.Y));
                PlaySound(sounds["fire"]);
                NewObject(newBullet);
                newBullet.collider.Velocity = -pointDirection * bulletSpeed;
                curWaitTime = 0;
            }

            //check if still alive
            if (life <= 0) 
            {
                //TODO end the game here
                if (savedData.AsteroidsScore < score.score)
                {
                    LastScore = score.score;
                    LastGame = "Asteroids";
                    LoadScene(1);
                }
                else 
                {
                    LoadScene(0);
                }
            }

        }


        /// <summary>
        /// updates the point direction
        /// </summary>
        public void UpdatePointDirection() 
        {
            int angleDeg = (int)transform.rotation.Z + 90;
            float angleRad = angleDeg * (MathF.PI / 180);
            pointDirection.X = MathF.Cos(angleRad);
            pointDirection.Y = MathF.Sin(angleRad);
        }


        public override void OnCollisionStay(Collider other)
        {
            if (other.gameObject.name == "Asteroid") 
            {
                life--;
                Destroy(other.gameObject);
            }
        }


        public override void Draw()
        {
            base.Draw();
            for (int i = 0; i < life; i++) 
            {
                Rectangle destRec = new Rectangle(200+(40*i), 10, transform.scale.X, transform.scale.Y);
                Rectangle sourceRec = new Rectangle(0,0, transform.scale.X, transform.scale.Y);
                DrawTexturePro(texture, sourceRec, destRec, new Vector2(0, 0), 0, WHITE);
            }
        }
    }
}
