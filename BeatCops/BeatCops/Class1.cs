using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;

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
            // experiment 1
            //GTA.UI.Screen.ShowSubtitle("creating a cop");            

            //Ped player = Game.Player.Character;
            //GTA.Math.Vector3 spawnLoc = player.Position + (player.ForwardVector * 5f);

            //string model_name = "s_m_y_cop_01";
            //Ped cop1 = GTA.World.CreatePed(model_name, spawnLoc);
            //cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //cop1.Task.FightAgainst(player);

            // experiment 2
            GTA.UI.Screen.ShowSubtitle("creating a cop & neutral ped");

            Ped player = Game.Player.Character;

            string model_name = "s_m_y_cop_01";
            Ped cop1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 5f));
            cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);

            model_name = "a_m_y_business_02";
            Ped civilian1 = GTA.World.CreatePed(model_name, player.Position + (player.ForwardVector * 6f));

            cop1.Task.FightAgainst(civilian1);

            // experiment 3

        }

        if(e.KeyCode == Keys.J)
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