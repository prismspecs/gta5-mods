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

using System.Net;
using System.Drawing.Imaging;

using NativeUI;
using NativeUI.PauseMenu;

namespace Testing
{
    #region ThreadedClass
    [ScriptAttributes(NoDefaultInstance = true)]
    public class AI : Script
    {
        public AI()
        {
            Tick += OnTick;
            Aborted += OnShutdown;

            Interval = 3000;
        }

        private Ped ped = null;
        private int wait = -1;
        public string animation = "HandsUp";

        public void SetWait(int ms)
        {
            if (ms > wait)
            {
                wait = ms;
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (wait > -1)
            {
                Wait(wait);
                wait = -1;
            }

            if (ped == null)
            {
                ped = World.CreatePed(PedHash.Beach01AMY, Game.Player.Character.Position + (GTA.Math.Vector3.RelativeFront * 3));
            }

            // Repeat animation if alive
            if (ped != null && ped.IsAlive)
            {
                if (animation == "HandsUp")
                {
                    ped.Task.HandsUp(1000);
                }
                else if (animation == "Jump")
                {
                    ped.Task.Jump();
                }
            }
        }

        private void OnShutdown(object sender, EventArgs e)
        {
            // Clear pedestrian on script abort
            ped?.Delete();
        }
    }
    #endregion

    public class Test : Script
    {
        // menu
        MenuPool menuPool;
        UIMenu modMenu;
        UIMenuItem ImageTest;
        UIMenuItem DeleteNearby;

        // drawing images
        GTA.UI.CustomSprite MySprite;
        bool IsDrawingSprite = false;

        // threading
        AI instance = null;

        //
        Ped player;
        Vehicle vehicle;

        public Test()
        {
            this.Tick += onTick;
            this.KeyDown += onKeyDown;

            //this.Interval = 1000;   // run every second

            // menu
            menuPool = new MenuPool();
            SetupMenu();
            modMenu.OnItemSelect += MenuSelect;

            // get player into position etc
            player = Game.Player.Character;
            Sub(player.Position);
            player.Position = new Vector3(30.9f,22.8f,70.055f); // arbitrary, nice road position

            // prepare players starting position
            //DeleteAllNearby();

            // put player in vehicle
            vehicle = GTA.World.CreateVehicle(VehicleHash.Taxi, player.Position);
            vehicle.PlaceOnGround();
            player.Task.WarpIntoVehicle(vehicle, VehicleSeat.Driver);
        }


        #region Delete All Nearby Entities
        void DeleteAllNearby()
        {
            Entity[] NearbyEntities = World.GetNearbyEntities(player.Position, 25f);
            foreach (var e in NearbyEntities)
            {
                
                // there seems to be some entities that you can't delete without glitching it out
                if(e is Ped || e is Vehicle) { 
                    if (e == player)
                    {
                        Sub("found one player entity");
                        continue;
                    }

                    else if (e != player.AttachedEntity && e != player)
                    {
                        e.Delete();
                    }
                }
            }
        }
        #endregion


        #region Menu Item Selection Handler
        void MenuSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if(item == ImageTest) {
                DoImageTest();
            }

            if(item == DeleteNearby)
            {
                DeleteAllNearby();
            }
        }
        #endregion

        void SetupMenu()
        {
            modMenu = new UIMenu("Testing Menu", "Blah");
            menuPool.Add(modMenu);

            ImageTest = new UIMenuItem("DL Image", "description");
            modMenu.AddItem(ImageTest);

            DeleteNearby = new UIMenuItem("Delete Nearby", "delete all nearby entities");
            modMenu.AddItem(DeleteNearby);

            GTA.UI.Screen.ShowSubtitle("setting up menu");
        }

        void Sub(object logtext)
        {
            GTA.UI.Screen.ShowSubtitle(logtext.ToString());
        }

        private void onTick(object sender, EventArgs e)
        {
            // menu
            if(menuPool != null)
            {
                menuPool.ProcessMenus();
            }

            Vector3 source = player.Position;

            // get 5 units in front of player
            Vector3 direction = source + Game.Player.Character.ForwardVector * 25f;

            //Vector3 spawnPos = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0));

            //RaycastResult rc = Raycast(Vector3 source, Vector3 direction, float maxDistance, IntersectOptions options, Entity ignoreEntity = null);
            //RaycastResult rc = World.Raycast(source, direction, 999, IntersectOptions.Everything, null);
            RaycastResult rc = World.Raycast(source, Game.Player.Character.ForwardVector, IntersectFlags.Everything);

            
            World.DrawMarker(MarkerType.VerticalCylinder, rc.HitPosition, Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 1f), Color.Yellow);


            World.DrawMarker(MarkerType.VerticalCylinder, World.GetCrosshairCoordinates().HitPosition, Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 2f), Color.Red);

            //UI.Notify(rc.HitCoords.ToString());

            if(IsDrawingSprite)
            {
                DrawSprite(Game.Player.Character, MySprite);
            }
        }
        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad7)
            {

                modMenu.Visible = !modMenu.Visible;

            }

            if (e.KeyCode == Keys.NumPad9)
            {
                // this works if the other script method is
                // internal static void
                //ExThread.mythread1();

                instance = InstantiateScript<AI>();

                //ExThread test = new ExThread();
                //instance.mythread1();

                // Creating object of ExThread class 
                //ExThread obj = new ExThread();

                // Creating thread 
                // Using thread class 
                //Thread thr = new Thread(new ThreadStart(obj.mythread1));
                //thr.Start();

            }
        }

        public void DoImageTest()
        {
            SaveImage("C:/Program Files (x86)/Steam/steamapps/common/Grand Theft Auto V/scripts/test2.png", ImageFormat.Png, "https://post.medicalnewstoday.com/wp-content/uploads/sites/3/2020/02/322868_1100-1100x628.jpg");

            MySprite = new GTA.UI.CustomSprite("C:/Program Files (x86)/Steam/steamapps/common/Grand Theft Auto V/scripts/test2.png", new SizeF(100, 100), new PointF(100, 100));

            IsDrawingSprite = true;

            //GTA.UI.Screen.ShowSubtitle(System.Reflection.Assembly.GetExecutingAssembly().Location);
            GTA.UI.Screen.ShowSubtitle("drawing sprite");
        }


        public void DrawSprite(Entity entity, CustomSprite spr, Color? color = null)
        {
            // get the position of the entity on screen & update the screen position of the sprite
            PointF screenPos = GTA.UI.Screen.WorldToScreen(entity.Position);

            spr.Position = screenPos;           // update screen position of sprite
            spr.Color = color ?? spr.Color;     // update sprite color if needed; if null, keep original color
            spr.Draw();
        }

        public void SaveImage(string filename, ImageFormat format, string imageUrl)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(imageUrl);
                Bitmap bitmap; bitmap = new Bitmap(stream);

                if (bitmap != null)
                {
                    bitmap.Save(filename, format);
                }

                stream.Flush();
                stream.Close();
                client.Dispose();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        GTA.UI.Screen.ShowSubtitle("HTTP Status Code: " + (int)response.StatusCode);
                    }
                    else
                    {
                        GTA.UI.Screen.ShowSubtitle("no status avail");
                    }
                }
                else
                {
                    GTA.UI.Screen.ShowSubtitle(ex.Status.ToString());
                }
            }
        }
    }
}
