using System;
using System.Collections.Generic;
using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.GameObjectList;
using static RGCore.Core_basic_window;
using System.Numerics;
using RGCore.RGPhysics;

namespace MiniAtariArcade
{
    class Mouse : Sprite
    {
        string textureName = "";
        public Mouse(string name, Vector2 pos, string textureName) : base(name, pos)
        {
            this.textureName = textureName;
        }

        public override void Start()
        {
            SetSprite(textures[textureName], 1, 1);
            transform.scale = new Vector3(texture.width, texture.height, 0);
            base.Start();
        }

        public override void Update()
        {
            transform.translation = new Vector3(GetMousePosition(), 0);
        }


    }
}
