using System;
using GTA;

namespace Testing
{

    [ScriptAttributes(NoDefaultInstance = true)]
    public class Covid : Script
    {

        Test main;

        public string animation = "smokers_cough_lazyworker";

        Ped player = null;

        public Covid()
        {
            Tick += OnTick;
            Aborted += OnShutdown;

        }

        // create a reference to parent script
        public void setMain(Test _main)
        {
            main = _main;
            main.Sub("Starting Covid");
            this.Interval = 1000;   // run every second
        }

        private void OnTick(object sender, EventArgs e)
        {
            if(player == null)
            {
                player = Game.Player.Character;
            }
            Random random = new Random();
            var n = random.NextDouble();
            if (n > .7)
            {
                Cough();
            }
        }

        private void Cough()
        {
            //PlayAnimation("anim@sports@ballgame@handball@", "ball_idle", 8.0f, -8.0f, -1, (AnimationFlags) 49, 0.0f);
            player.Task.PlayAnimation("timetable@gardener@smoking_joint", "idle_cough", 8.0f, -1, (AnimationFlags)49);
        }

        private void OnShutdown(object sender, EventArgs e)
        {

        }
    }
}
