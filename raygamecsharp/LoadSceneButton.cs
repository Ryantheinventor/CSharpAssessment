using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using System.Numerics;
using System;

namespace MiniAtariArcade
{
    class LoadSceneButton : Sprite
    {
        int scene = 0;
        string textureName = "";
        public LoadSceneButton(string name, Vector2 pos, string textureName, int scene) : base(name, pos)
        {
            this.textureName = textureName;
            this.scene = scene;
            //transform.scale = new Vector3(texture.width, texture.height, 0);
            color = WHITE;
        }
        public override void Start()
        {
            SetSprite(textures[textureName], 1, 1);
            transform.scale = new Vector3(texture.width, texture.height, 0);
            base.Start();
        }

        public override void Update()
        {
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) 
            {
                if (CheckCollisionPointRec(GetMousePosition(), new Rectangle(transform.translation.X - transform.scale.X / 2, transform.translation.Y - transform.scale.Y / 2, transform.scale.X, transform.scale.Y)))
                {
                    LoadScene(scene);
                }
            }

        }
    }
}
