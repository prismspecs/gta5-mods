/*
 * worthwhile stuff
 * Vehicle NearbyVehicle = World.GetClosestVehicle(SpawnLocation, 10);
 * civilian1.VehicleDrivingFlags = VehicleDrivingFlags.FollowTraffic;
 * 
 * taskSeq = new TaskSequence();
 * taskSeq.AddTask.WarpIntoVehicle(raceCar01, VehicleSeat.Driver);
   taskSeq.AddTask.DriveTo(raceCar01, p01, 5f, racerSpeed, (int)DrivingStyle.AvoidTrafficExtremely);
   

   Game.Player.Character.Task.WarpIntoVehicle(NewCar, VehicleSeat.Driver);

 * https://forums.gta5-mods.com/topic/5940/automatic-vehicle-spawn-and-rotation/4
 * https://github.com/crosire/scripthookvdotnet/blob/main/source/scripting_v3/GTA/Entities/Vehicles/VehicleHash.cs
 * https://gtaforums.com/topic/792877-list-of-over-100-coordinates-more-comming/
 * http://gtaxscripting.blogspot.com/2013/07/tut-drawing-shapes-and-text-on-screen.html
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;
using GTA.UI;

public class CarTesting : Script
{
    private float Gas = 100f;
    private int PedsHelped = 5;

    public CarTesting()
    {
        this.Tick += onTick;
        this.KeyUp += onKeyUp;
        this.KeyDown += onKeyDown;

        Game.Player.Character.Task.ClearAllImmediately();
    }

    private void onTick(object sender, EventArgs e)
    {

        //string DrawText = "GAS: " + Gas + "\nPeople Picked Up: " + PedsHelped;
        //TextElement debugUIText = new TextElement(DrawText, new Point(40, 20), 0.5f, Color.White, GTA.UI.Font.ChaletLondon, Alignment.Left, true, true);


        // experiment: drawing "bar" using background black ticks and foreground white ticks
        TextElement backgroundUI = new TextElement(formatBar("GAS", 1)[0], new Point(40, 20), 0.5f, Color.Black, GTA.UI.Font.Pricedown, Alignment.Left, false, false);
        TextElement foregroundUI = new TextElement(formatBar("GAS", Gas/100)[1], new Point(40, 20), 0.5f, Color.White, GTA.UI.Font.Pricedown, Alignment.Left, false, false);

        GTA.Native.Function.Call(Hash.DRAW_RECT, .123, .07, 0.2, 0.1, 255, 255, 255, 46);

        backgroundUI.Draw();
        foregroundUI.Draw();

        Gas -= .005f;
    }

    private string[] formatBar(string label, float percent)
    {
        string bars = "";
        for(int i = 0; i < 40; i++)
        {
            bars += "|";
        }
        string backgroundText = label + " " + bars;

        bars = "";
        for (int i = 0; i < 40 * percent; i++)
        {
            bars += "|";
        }
        string foregroundText = label + " " + bars;

        string[] combined = { backgroundText, foregroundText };

        return combined;
    }

    private void onKeyUp(object sender, KeyEventArgs e)
    {
        Ped player = Game.Player.Character;
        if (e.KeyCode == Keys.O)
        {
            //Vehicle vehicle = World.CreateVehicle(VehicleHash.Adder, Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5);
            //vehicle.PlaceOnGround();

            Vehicle vehicle1 = GTA.World.CreateVehicle(VehicleHash.Taxi, player.Position + player.ForwardVector * 6);
            vehicle1.PlaceOnGround();

            //string model_name = "a_m_y_business_02";
            //Vector3 playerPos = player.Position + (player.ForwardVector * 8f);
            //float Z = World.GetGroundHeight(playerPos);
            //Vector3 groundedPos = new Vector3(playerPos.X, playerPos.Y, Z);
            //Vector3 civPos = World.GetNextPositionOnStreet((player.Position + (player.ForwardVector * 3f)).Around(1f));
            //Ped civilian1 = GTA.World.CreatePed(model_name, groundedPos);
            //civilian1.VehicleDrivingFlags = VehicleDrivingFlags.FollowTraffic;

            //civilian1.Task.ClearAllImmediately();
            //civilian1.Task.EnterVehicle(vehicle1, VehicleSeat.Any, -1, 10f);
            //civilian1.AlwaysKeepTask = true;

            Vector3 target1 = new Vector3(126.975f, 3714.419f, 46.827f);

            // derive target from map marker
            if (Game.IsWaypointActive)
            {
                target1 = World.WaypointPosition;
                target1 = World.GetNextPositionOnStreet(target1.Around(100f));
            }

            TaskSequence taskSeq = new TaskSequence();
            taskSeq.AddTask.WarpIntoVehicle(vehicle1, VehicleSeat.Driver);
            taskSeq.AddTask.DriveTo(vehicle1, target1, 100f, 10f, DrivingStyle.Normal);
            //taskSeq.AddTask.DriveTo(vehicle1, target2, 100f, 10f, DrivingStyle.Normal);

            //civilian1.Task.PerformSequence(taskSeq);
            player.Task.PerformSequence(taskSeq);

        }

        if (e.KeyCode == Keys.L)
        {
            
        }
    }

    private void onKeyDown(object sender, KeyEventArgs e)
    {
    }
}