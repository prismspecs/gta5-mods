## Getting Visual Studio set up for modding
+ Install all of the C# stuff in VS (I used Community 2017), its 15gb or so
+ Installed all the ScriptHook stuff above
+ New Project -> Visual C# -> Class Library (.NET Framework)
+ Need to make sure that the target framework is the same as current ScriptHookV version, which for me is currently 4.8
+ Right click References -> Add Reference -> System.Windows.Forms and then "Browse" to find ScriptHookV .Net dll
+ Basic script structure:
```
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
    public Test()
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
    }
}
```

+ Used savegame to bypass prologue mission
  + https://www.gta5-mods.com/misc/fresh-start-introduction-completed-savegame

+ Useful links
  + [Francis Generated the Documentation](https://frnsys.com/misc/gtav/)
  + https://github.com/crosire/scripthookvdotnet/tree/master/source/scripting_v3/GTA
  + https://github.com/crosire/scripthookvdotnet/wiki/Getting-Started
  + I just googled around and got the latest ScriptHookV and ScriptHookVDotNot as well as OpenIV
  + Probably can get them from: https://github.com/crosire/scripthookvdotnet/releases
  + Tutorial series to get up and running: https://www.youtube.com/watch?v=PaQZEdES7No&list=PLbhQKmLHe3WVkQx1oO3XZuGcfk0ddzlUt&index=1
  + https://gtaforums.com/topic/814258-c-ultimate-modding-thread/
  + https://gtaforums.com/topic/793572-vnet-creating-c-scripts-for-v/
  + [[.NET] [C#] [SHVDN] Modding Basics! Vector3s and Vehicles Manipulation [Part 3]](https://forums.gta5-mods.com/topic/7113/net-c-shvdn-modding-basics-vector3s-and-vehicles-manipulation-part-3)

+ Some tutorials/info
  + https://forums.gta5-mods.com/topic/13821/tutorial-making-a-c-bodyguard-script-with-grand-theft-auto-v-mod-creator
  + https://gtaforums.com/topic/813669-cnet-simple-helpful-functions/
  + https://forums.gta5-mods.com/category/8/tutorials
  + https://gtaforums.com/topic/843323-c-making-peds-fight-each-other-with-specific-weapon-question/ (having peds fight each other)
  + [Documentation: TASK_GO_STRAIGHT_TO_COORD on Ped movement](https://gtamods.com/wiki/TASK_GO_STRAIGHT_TO_COORD)
  + [Forum: Task GOTO_ENTITY](https://gtaforums.com/topic/807241-task_goto_entity-k9-script/)
  + [Search for GTA.Native on GitHub to browse open source mods](https://github.com/search?p=7&q=using+GTA.Native%3B&type=Code)
  + [Mod: RealWeather, use real weather data in game](https://gitlab.com/Jitnaught/RealWeather-GTA5/-/blob/master/RealWeather/script.cpp#L267)
  + [Forum: How to create new Threads in GTA](https://forums.gta5-mods.com/topic/28846/is-it-possible-to-create-new-threads-inside-a-gta5-script/5)
  + [DeepDrive on GitHub: Using OpenAI Universe to pilot GTA5 car](https://github.com/deepdrive/deepdrive)
