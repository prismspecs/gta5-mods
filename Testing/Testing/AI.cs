using System;
using GTA;

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

}
