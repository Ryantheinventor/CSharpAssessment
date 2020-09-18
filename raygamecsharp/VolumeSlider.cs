using RGCore;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static RGCore.Core_basic_window;
using static RGCore.GameObjectList;
using System.Numerics;
using RGCore.RGPhysics;
using System;

namespace MiniAtariArcade
{
    class VolumeSlider : GameObject
    {
        bool preventTracking = false;
        bool isTracking = false;
        Vector2 SliderPos = new Vector2(0,100);
        public VolumeSlider(string name, Vector2 pos) : base(name, pos) 
        { 
        
        }

        public override void Start()
        {
            base.Start();
            SliderPos = new Vector2(transform.translation.X, (transform.translation.Y + 50) - (100 * savedData.Volume));
        }

        public override void Update()
        { 
            if (IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON) && !preventTracking && !isTracking)
            {
                Vector2 mousePos = GetMousePosition();
                if (mousePos.X > transform.translation.X - 10 && mousePos.X < transform.translation.X + 10
                    && mousePos.Y > transform.translation.Y - 60 && mousePos.Y < transform.translation.Y + 60)
                {
                    isTracking = true;
                }
                else
                {
                    if (!(mousePos.X > transform.translation.X - 10 && mousePos.X < transform.translation.X + 10
                        && mousePos.Y > transform.translation.Y - 60 && mousePos.Y < transform.translation.Y + 60))
                    {
                        preventTracking = true;
                    }
                }
            }
            else if (!IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON)) 
            {
                isTracking = false;
                preventTracking = false;
            }

            if(isTracking)
            {
                Vector2 mousePos = GetMousePosition();
                SliderPos.Y = mousePos.Y;
                if (SliderPos.Y > transform.translation.Y + 50) 
                {
                    SliderPos.Y = transform.translation.Y + 50;
                }
                if (SliderPos.Y < transform.translation.Y - 50)
                {
                    SliderPos.Y = transform.translation.Y - 50;
                }
            }

            int zero = (int)transform.translation.Y + 50;
            savedData.Volume = 0.01f*(zero - SliderPos.Y);
            SetMasterVolume(savedData.Volume);

        }


        public override void Draw()
        {
            DrawRectangle((int)transform.translation.X - 3, (int)transform.translation.Y - 50, 6, 100, WHITE);
            Rectangle sourceRec = new Rectangle(0, 0, 40, 40);
            Rectangle destRecMute = new Rectangle((int)transform.translation.X - 20, (int)transform.translation.Y + 75, 40, 40);
            Rectangle destRecFull = new Rectangle((int)transform.translation.X - 20, (int)transform.translation.Y - 115, 40, 40);
            DrawTexturePro(textures["Mute"], sourceRec, destRecMute, new Vector2(0, 0), 0, WHITE);
            DrawTexturePro(textures["Full"], sourceRec, destRecFull, new Vector2(0, 0), 0, WHITE);
            DrawCircle((int)SliderPos.X, (int)SliderPos.Y, 10, WHITE);

            base.Draw();
        }

    }
}
