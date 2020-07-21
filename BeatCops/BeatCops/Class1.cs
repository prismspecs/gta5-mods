using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Threading.Tasks;

public class BeatCops : Script
{


    public BeatCops()
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
    }

    private void onKeyDown(object sender, KeyEventArgs e)
    {
        // 
        if (e.KeyCode == Keys.H)
        {
            /*
            ||||||||||||||||| Begin Software 4 Artists |||||||||||||||||
            */

            Ped player = Game.Player.Character;
            GTA.Math.Vector3 spawn_location = player.Position + (player.ForwardVector * 5f);
            //string model_name = "a_m_y_business_02";
            string model_name = "s_m_y_cop_01";

            Ped ped1 = GTA.World.CreatePed(model_name, spawn_location);
            ped1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            model_name = "s_m_y_cop_01";
            Ped ped2 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 6f));
            ped2.Weapons.Give(WeaponHash.Bat, 999, true, true);

            // make them fight!
            ped1.Task.ClearAllImmediately();
            ped2.Task.ClearAllImmediately();
            ped1.Task.FightAgainst(ped2);
            ped2.Task.FightAgainst(ped1);

            /*
            |||||||||||||||||| End Software 4 Artists |||||||||||||||||
            */



            // experiment 1
            //GTA.UI.Screen.ShowSubtitle("creating a cop");            

            //Ped player = Game.Player.Character;
            //GTA.Math.Vector3 spawnLoc = player.Position + (player.ForwardVector * 5f);

            //string model_name = "s_m_y_cop_01";
            //Ped cop1 = GTA.World.CreatePed(model_name, spawnLoc);
            //cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //cop1.Task.FightAgainst(player);

            // experiment 2
            //GTA.UI.Screen.ShowSubtitle("creating a cop & neutral ped");

            //Ped player = Game.Player.Character;

            //string model_name = "s_m_y_cop_01";
            //Ped cop1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 5f));
            //cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //model_name = "a_m_y_business_02";
            //Ped civilian1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 6f));

            //cop1.Task.FightAgainst(civilian1);

            // experiment 3
            //GTA.UI.Screen.ShowSubtitle("creating two cops to fight each other");

            //Ped player = Game.Player.Character;

            //string model_name = "s_m_y_cop_01";
            //Ped cop1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 5f));
            //cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //model_name = "s_m_y_cop_01";
            //Ped cop2 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 6f));

            //// create a new relationship group
            //RelationshipGroup newGroup1 = World.AddRelationshipGroup("newgroup1");
            //cop1.RelationshipGroup = newGroup1;
            //RelationshipGroup newGroup2 = World.AddRelationshipGroup("newgroup2");
            //cop1.RelationshipGroup = newGroup2;

            //newGroup1.SetRelationshipBetweenGroups(newGroup2, Relationship.Hate);
            //newGroup2.SetRelationshipBetweenGroups(newGroup1, Relationship.Hate);

            //GTA.UI.Notification.Show(cop1.RelationshipGroup.ToString() + ", " + cop2.RelationshipGroup.ToString());

            //// things i've tried that do not work
            //cop1.Task.FightAgainst(cop2);
            //cop1.AlwaysKeepTask = true;
            //// 46 is the "fight to the death" attribute
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop1, 46, true);
            //Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, cop1, 0, 0);

            // experiment 4 -- sanity check
            //GTA.UI.Screen.ShowSubtitle("sanity check, exact same code w different models");

            //Ped player = Game.Player.Character;

            //string model_name = "s_m_y_cop_01";
            //Ped cop1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 5f));
            //cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //model_name = "a_m_y_business_02";
            //Ped cop2 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 6f));

            //// create a new relationship group
            //RelationshipGroup newGroup1 = World.AddRelationshipGroup("newgroup1");
            //cop1.RelationshipGroup = newGroup1;
            //RelationshipGroup newGroup2 = World.AddRelationshipGroup("newgroup2");
            //cop1.RelationshipGroup = newGroup2;

            //newGroup1.SetRelationshipBetweenGroups(newGroup2, Relationship.Hate);
            //newGroup2.SetRelationshipBetweenGroups(newGroup1, Relationship.Hate);

            //GTA.UI.Notification.Show(cop1.RelationshipGroup.ToString() + ", " + cop2.RelationshipGroup.ToString());

            //// things i've tried that do not work
            //cop1.Task.FightAgainst(cop2);
            //cop1.AlwaysKeepTask = true;
            //// 46 is the "fight to the death" attribute
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop1, 46, true);
            //Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, cop1, 0, 0);

            //// experiment 5 -- adding cop to player group
            //GTA.UI.Screen.ShowSubtitle("experiment 5: ");

            //Ped player = Game.Player.Character;
            //RelationshipGroup playerGroup = player.RelationshipGroup;

            //string model_name = "s_m_y_cop_01";
            //Ped cop1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 5f));
            //cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);
            //cop1.RelationshipGroup = playerGroup;

            // experiment 6 ...
            //Ped player = Game.Player.Character;
            //GTA.Math.Vector3 spawnLoc = player.Position + (player.ForwardVector * 5f);
            //string model_name = "a_m_y_business_02";
            ////string model_name = "s_m_y_cop_01";

            //Ped ped1 = GTA.World.CreatePed(model_name, spawnLoc);
            //ped1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //model_name = "s_m_y_cop_01";
            //Ped ped2 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 5f));
            //ped2.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //ped1.Task.ClearAllImmediately();
            //ped2.Task.ClearAllImmediately();
            //ped1.Task.FightAgainst(ped2);
            //ped2.Task.FightAgainst(ped1);

        }

        if (e.KeyCode == Keys.J)
        { 
            Game.Player.WantedLevel = 0;
            Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, Game.Player, true);
            Ped player = Game.Player.Character;
            GTA.Math.Vector3 playerpos = player.Position;
            Ped[] allpeds = GTA.World.GetNearbyPeds(playerpos, 50);

            foreach (Ped p in allpeds)
            {
                if (p != player)
                {
                    p.Task.FightAgainstHatedTargets(100);
                    //World.SetRelationshipBetweenGroups((Relationship)5, Game.get_Player().get_Character().get_RelationshipGroup(), num);
                    //World.SetRelationshipBetweenGroups((Relationship)5, num, Game.get_Player().get_Character().get_RelationshipGroup());
                }
            }


            foreach (Ped p in allpeds)
            {
                foreach (Ped q in allpeds) if (p.Handle != q.Handle)
                    {
                        p.Task.FightAgainst(q);
                    }
            }
        }
    }
}