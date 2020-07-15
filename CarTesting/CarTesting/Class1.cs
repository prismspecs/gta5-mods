/*
 * worthwhile stuff
 * Vehicle NearbyVehicle = World.GetClosestVehicle(SpawnLocation, 10);
 * civilian1.VehicleDrivingFlags = VehicleDrivingFlags.FollowTraffic;
 * 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;

public class CarTesting : Script
{
    public CarTesting()
    {
        this.Tick += onTick;
        this.KeyUp += onKeyUp;
        this.KeyDown += onKeyDown;
    }

    private void onTick(object sender, EventArgs e)
    {
    }

    private void onKeyUp(object sender, KeyEventArgs e)
    {
        Ped player = Game.Player.Character;
        if (e.KeyCode == Keys.A)
        {
            //Vehicle vehicle = World.CreateVehicle(VehicleHash.Adder, Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5);
            //vehicle.PlaceOnGround();

            Vehicle vehicle1 = GTA.World.CreateVehicle(VehicleHash.Taxi, player.Position + player.ForwardVector * 6);
            vehicle1.PlaceOnGround();

            string model_name = "a_m_y_business_02";
            Ped civilian1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 3f));
            civilian1.VehicleDrivingFlags = VehicleDrivingFlags.FollowTraffic;

            civilian1.Task.ClearAllImmediately();
            civilian1.Task.EnterVehicle(vehicle1, VehicleSeat.Any, -1, 10f);
            civilian1.AlwaysKeepTask = true;


        }
    }

    private void onKeyDown(object sender, KeyEventArgs e)
    {
    }
}