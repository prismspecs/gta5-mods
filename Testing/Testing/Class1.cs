using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;

namespace Testing
{
    public class Test : Script
    {


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
            RaycastResult rc = World.Raycast(source, Game.Player.Character.ForwardVector, IntersectOptions.Everything);

            World.DrawMarker(MarkerType.VerticalCylinder, rc.HitCoords, Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 1f), Color.Yellow);


            World.DrawMarker(MarkerType.VerticalCylinder, World.GetCrosshairCoordinates().HitCoords, Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 2f), Color.Red);

            //UI.Notify(rc.HitCoords.ToString());
        }
        private void onKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
