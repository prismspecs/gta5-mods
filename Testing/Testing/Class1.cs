using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;
using GTA.UI;

namespace Testing
{
    public class Test : Script
    {
        
        GTA.UI.CustomSprite MySprite;

        public Test()
        {
            this.Tick += onTick;
            this.KeyUp += onKeyUp;
            this.KeyDown += onKeyDown;

            //this.Interval = 1000;   // run every second
        }

        private void onTick(object sender, EventArgs e)
        {
            Ped player = Game.Player.Character;

            Vector3 source = player.Position;

            // get 5 units in front of player
            Vector3 direction = source + Game.Player.Character.ForwardVector * 25f;

            //        Vector3 spawnPos = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0));

            //RaycastResult rc = Raycast(Vector3 source, Vector3 direction, float maxDistance, IntersectOptions options, Entity ignoreEntity = null);
            //RaycastResult rc = World.Raycast(source, direction, 999, IntersectOptions.Everything, null);
            RaycastResult rc = World.Raycast(source, Game.Player.Character.ForwardVector, IntersectFlags.Everything);

            
            World.DrawMarker(MarkerType.VerticalCylinder, rc.HitPosition, Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 1f), Color.Yellow);


            World.DrawMarker(MarkerType.VerticalCylinder, World.GetCrosshairCoordinates().HitPosition, Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 2f), Color.Red);

            //UI.Notify(rc.HitCoords.ToString());
        }
        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Y)
            {
                MySprite = new GTA.UI.CustomSprite("test.png", new SizeF(100,100), new PointF(100,100));
                markEntityOnScreen(Game.Player.Character, MySprite);


            }
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {

        }

        public static void markEntityOnScreen(Entity entity, CustomSprite spr, Color? color = null)
        {
            // get the position of the entity on screen & update the screen position of the sprite
            PointF screenPos = GTA.UI.Screen.WorldToScreen(entity.Position);
            spr.Position = screenPos;           // update screen position of sprite
            spr.Color = color ?? spr.Color;     // update sprite color if needed; if null, keep original color
            spr.Draw();
        }
    }
}
