using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using RGCore.RGPhysics;
using static RGCore.GameObjectList;
using static RGCore.Core_basic_window;
using System;
using System.Collections.Generic;

namespace raygamecsharp.Textures
{
    class EnterName : GameObject
    {
        string name = "aaa";
        public EnterName(string name, Vector2 pos) : base(name, pos)
        {
            transform.scale = new Vector3(20, 20, 0);
        }

        public override void Start()
        {
            base.Start();
            
        }
        //a-z = 97-122
        public override void Update()
        {
            int key = GetKeyPressed();
            if (key >= 97 && key <= 122) 
            { 
                
            }
        }

    }




}
