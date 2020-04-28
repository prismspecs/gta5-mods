using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;

public class Test : Script
{

    List<Ped> group_members = new List<Ped>();
    public bool isActive = false;
    private List<int> riotRelationships;
    public int copgroup1, copgroup2;

    public object TRUE { get; private set; }

    public Test()
    {
        this.Tick += onTick;
        this.KeyUp += onKeyUp;
        this.KeyDown += onKeyDown;

        copgroup1 = World.AddRelationshipGroup("cops1");
        copgroup2 = World.AddRelationshipGroup("cops2");


    }

    private void onTick(object sender, EventArgs e)
    {
        if (isActive)
        {

            Game.Player.WantedLevel = 0;
            Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, Game.Player, true);
            Function.Call(Hash.SET_WANTED_LEVEL_MULTIPLIER, 0.0);
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

    private string RandomString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 1; index < 6; ++index)
        {
                Random RNG = new Random();
                int randomNumber = RNG.Next(1, 1000);

                char ch = Convert.ToChar(Convert.ToInt32(System.Math.Floor(26.0 * randomNumber + 65.0)));
            stringBuilder.Append(ch);
        }
        return stringBuilder.ToString();
    }

    private void onKeyDown(object sender, KeyEventArgs e)
    {
    }

    private void onKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.NumPad3)
        {
            Ped player = Game.Player.Character;
            GTA.Math.Vector3 playerpos = player.Position;

            isActive = !isActive;
            UI.Notify("Riot Mode " + isActive.ToString());

            Ped[] allpeds = GTA.World.GetNearbyPeds(playerpos, 50);
            foreach (Ped p in allpeds)
            {
                if (p != player)
                {
                    p.Weapons.Give(WeaponHash.Bat, 1, true, true);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, p, 46, true);
                    //Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, p, true);
                    //Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, p, 0, 0);

                    int num = World.AddRelationshipGroup(RandomString());
                    p.RelationshipGroup = num;
                    this.riotRelationships.Add(num);
                    foreach (int riotRelationship in this.riotRelationships)
                    {
                        World.SetRelationshipBetweenGroups(Relationship.Hate, num, riotRelationship);
                        World.SetRelationshipBetweenGroups(Relationship.Hate, riotRelationship, num);
                    }
                }
            }
        }

        if (e.KeyCode == Keys.NumPad1)
        {
            
            UI.ShowSubtitle("create cop new");
            Ped player = Game.Player.Character;
            GTA.Math.Vector3 spawnLoc = player.Position + (player.ForwardVector * 5f);
            
            int playerRGroup = player.RelationshipGroup;

            World.SetRelationshipBetweenGroups(Relationship.Hate, copgroup1, copgroup2);
            World.SetRelationshipBetweenGroups(Relationship.Hate, copgroup2, copgroup1);
            //World.RemoveRelationshipGroup(-1533126372);
            
            //string model_name = "a_m_y_golfer_01"; // s_m_y_cop_01
            string model_name = "a_m_y_business_01"; // s_m_y_cop_01
            Ped cop1 = GTA.World.CreatePed(model_name, spawnLoc);
            Ped cop2 = GTA.World.CreatePed(model_name, spawnLoc);

            UI.ShowSubtitle(cop1.RelationshipGroup.GetHashCode().ToString()); 

            cop1.Task.ClearAllImmediately();
            cop2.Task.ClearAllImmediately();

            cop1.RelationshipGroup = copgroup1;
            cop2.RelationshipGroup = copgroup2;

            Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, cop1, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop1, 46, true);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, cop1, 0, 0);

            Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, cop2, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop2, 46, true);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, cop2, 0, 0);

            cop1.Weapons.Give(WeaponHash.Bat, 999, true, true);
            cop2.Weapons.Give(WeaponHash.Bat, 999, true, true);

            //cop1.Task.FightAgainst(player);
            //cop2.Task.FightAgainst(player);

            cop1.Task.FightAgainstHatedTargets(100);
            cop1.AlwaysKeepTask = true;
            cop2.Task.FightAgainstHatedTargets(100);
            cop2.AlwaysKeepTask = true;
        }
    }

    /*
        if (e.KeyCode == Keys.NumPad0)
        {
            Vehicle vehicle = World.CreateVehicle(VehicleHash.Adder, Game.Player.Character.Position + Game.Player.Character.ForwardVector * 3.0f, Game.Player.Character.Heading + 90);
            vehicle.CanTiresBurst = false;
            vehicle.PlaceOnGround();
            vehicle.NumberPlate = "GRAY";
        }

        if (e.KeyCode == Keys.E)
        {
            UI.ShowSubtitle("E Pressed new");
            Ped player = Game.Player.Character;
            GTA.Math.Vector3 spawnLoc = player.Position + (player.ForwardVector * 5f);

            // find model name by opening OpenIV...
            string model_name = "a_c_shepherd";
            Ped companion = GTA.World.CreatePed(model_name, spawnLoc);

        }

        if (e.KeyCode == Keys.NumPad1)
        {
            UI.ShowSubtitle("create cop new");
            Ped player = Game.Player.Character;
            GTA.Math.Vector3 spawnLoc = player.Position + (player.ForwardVector * 5f);

            // find model name by opening OpenIV...
            string model_name = "s_m_y_cop_01";
            Ped cop = GTA.World.CreatePed(model_name, spawnLoc);
            group_members.Add(cop);

            Ped cop2 = GTA.World.CreatePed(model_name, spawnLoc);
            group_members.Add(cop2);
            // bodyguard.Weapons.Give(WeaponHash.Pistol, 9999, true, true);

            Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, cop, true);
            Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, cop2, true);

            // ...
            int copgroup1 = World.AddRelationshipGroup("cops1");
            int copgroup2 = World.AddRelationshipGroup("cops2");
            int playerRGroup = player.RelationshipGroup;

            cop.RelationshipGroup = copgroup1;
            cop2.RelationshipGroup = copgroup2;

            World.SetRelationshipBetweenGroups(Relationship.Hate, copgroup1, copgroup2);
            World.SetRelationshipBetweenGroups(Relationship.Hate, copgroup2, copgroup1);

            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop, 46, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop2, 46, true);

            cop.Task.AimAt(cop2, 10);
            cop2.Task.AimAt(cop, 10);
            cop.Task.FightAgainst(cop2);
            cop2.Task.FightAgainst(cop);

            cop.Weapons.Give(WeaponHash.SMG, 999, true, true);

            // this is how you create different groups and so on...
            //PedGroup playerGroup = player.CurrentPedGroup;
            //PedGroup randGroup = new PedGroup();
            //Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, cop, randGroup); // puts the bodyguard into the players group
        }

        if (e.KeyCode == Keys.NumPad3)
        {
            UI.ShowSubtitle("test 3");

            Ped player = Game.Player.Character;
            GTA.Math.Vector3 spawnLoc1 = player.Position + (player.ForwardVector * 5f);
            GTA.Math.Vector3 spawnLoc2 = player.Position + (player.ForwardVector * 7f);

            int RELATIONSHIP_PROTESTERS = World.AddRelationshipGroup("PROTESTERS"); //Thanks to SHDNV, it converts the string to an int hash automatically
            int RELATIONSHIP_COP = Function.Call<int>(Hash.GET_HASH_KEY, "COP");

            World.SetRelationshipBetweenGroups(Relationship.Hate, RELATIONSHIP_COP, RELATIONSHIP_PROTESTERS);
            World.SetRelationshipBetweenGroups(Relationship.Hate, RELATIONSHIP_PROTESTERS, RELATIONSHIP_COP);

            string model_name = "s_m_y_cop_01";
            Ped cop1 = GTA.World.CreatePed(model_name, spawnLoc1);
            Ped cop2 = GTA.World.CreatePed(model_name, spawnLoc2);
            cop1.RelationshipGroup = RELATIONSHIP_PROTESTERS;
            cop2.RelationshipGroup = RELATIONSHIP_COP;

            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop1, 46, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, cop2, 46, true);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, cop1, 0);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, cop2, 0);

            cop1.Weapons.Give(WeaponHash.MicroSMG, 120, true, true);

            //cop1.Task.FightAgainstHatedTargets(5);
            //cop2.Task.FightAgainstHatedTargets(5);
            //cop1.Task.FightAgainst(cop2);
            //cop2.Task.FightAgainst(cop1);

        }

        // make everyone fight?
        if (e.KeyCode == Keys.NumPad2)
        {
            Ped player = Game.Player.Character;
            GTA.Math.Vector3 playerpos = player.Position;
            Ped[] allpeds = GTA.World.GetNearbyPeds(playerpos, 5);
            foreach (Ped p in allpeds)
            {
                if (p.Handle != player.Handle) {
                    Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, p, true);
                    Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, p, 0, 0);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, p, 46, true);
                    foreach (Ped q in allpeds)
                        if (p.Handle != q.Handle)
                            p.Task.FightAgainst(q);
                }
            }
        }

        if (e.KeyCode == Keys.NumPad4)
        {
            UI.Notify("Riot Mode Enabled");
            Game.Player.WantedLevel = 0;
            Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, Game.Player, true);
            Function.Call(Hash.SET_WANTED_LEVEL_MULTIPLIER, 0.0);
            Ped player = Game.Player.Character;
            GTA.Math.Vector3 playerpos = player.Position;
            Ped[] allpeds = GTA.World.GetAllPeds();
            foreach (Ped p in allpeds)
            {
                if (p != player)
                {
                    p.Weapons.Give(WeaponHash.Bat, 1, true, true);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, p, 46, true);
                    //p.Task.FightAgainst(p);
                    //p.Task.FightAgainst(player);                }           
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

        if (e.KeyCode == Keys.NumPad5)
        {

            Ped player = Game.Player.Character;
            GTA.Math.Vector3 playerpos = player.Position;

            string model_name = "s_m_y_cop_01";
            Ped ped = World.CreatePed(model_name, playerpos.Around(5));

            while (!ped.Exists())
            {
                Script.Wait(100);
            }

            ped.Task.ClearAllImmediately();

            ped.AlwaysKeepTask = true;
            

            // dont let the playerped decide what to do when there is combat etc.
            Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, ped, true);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped, 0, 0);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);
        }
    } */
}