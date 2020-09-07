using System;
using GTA;
using GTA.Math;
using GTA.Native;
using System.Drawing;
using System.Windows.Forms;

namespace Testing
{

    [ScriptAttributes(NoDefaultInstance = true)]
    public class ImageFace : Script
    {
        Test main;
        Ped player = null;

        public ImageFace()
        {
            Tick += OnTick;
            Aborted += OnShutdown;

        }

        // create a reference to parent script
        public void setMain(Test _main)
        {
            main = _main;
            main.Sub("Starting ImageFace");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (player == null)
            {
                player = Game.Player.Character;
            }

            GetWeird();

            // screen to world: using mouse to get 3d coords

            //Game.EnableControlThisFrame(GTA.Control.CursorX);
            //Game.EnableControlThisFrame(GTA.Control.CursorY);
            //float x = Game.GetControlValueNormalized(GTA.Control.CursorX);
            //float y = Game.GetControlValueNormalized(GTA.Control.CursorY);

            //Function.Call(Hash.DRAW_RECT, x, y, 0.01f, 0.01f, 0, 0, 255, 255, false);

            //Vector3 dir;
            //Vector3 cam3DPos = ScreenRelToWorld(GameplayCamera.Position, GameplayCamera.Rotation, new Vector2(x, y), out dir);
            ////World.DrawMarker(MarkerType.DebugSphere, cam3DPos, Vector3.Zero, Vector3.Zero, new Vector3(0.01f, 0.01f, 0.01f), Color.Red);

            //RaycastResult r = World.Raycast(cam3DPos, dir, 50f, IntersectFlags.Everything, Game.Player.Character);
            //if (r.DidHit)
            //{
            //    World.DrawMarker(MarkerType.DebugSphere, r.HitPosition, Vector3.Zero, Vector3.Zero, new Vector3(0.2f, 0.2f, 0.2f), Color.Blue);
            //}





            // world to screen: using 3d coords to render to screen
            Vector2 point2D = World3DToScreen2D(player.Position);
            Function.Call(Hash.DRAW_RECT, point2D.X, point2D.Y, 0.1f, 0.1f, 0, 255, 255, 255, false);
            //main.Sub(point2DZero.ToString());
            
            // get the coords of player head bone and draw a rect on it
            Vector3 v = Function.Call<Vector3>(Hash.GET_PED_BONE_COORDS, player, Bone.SkelHead);
            Vector2 v2 = World3DToScreen2D(v);


            Function.Call(Hash.DRAW_RECT, v2.X, v2.Y, 0.1f, 0.1f, 255, 255, 0, 255, false);
            //main.Sub(point2D.ToString());

        }

        void GetWeird()
        {
            Entity[] NearbyEntities = World.GetNearbyEntities(player.Position, 15f);
            foreach (var e in NearbyEntities)
            {

                if (e is Ped && e != player)
                {


                    // draw every bone
                    EntityBoneCollection ebc = e.Bones;
                    foreach (EntityBone eb in ebc) {
                        if(eb.Index == Bone.SkelHead) { 
                        Vector3 v = eb.Position;
                        //Vector3 v = Function.Call<Vector3>(Hash.GET_PED_BONE_COORDS, e.Handle, Bone.SkelHead);
                        Vector2 point2D = World3DToScreen2D(v);
                        Function.Call(Hash.DRAW_RECT, point2D.X, point2D.Y, 0.002f, 0.002f * 1.78, 255, 255, 0, 125, false);


                        //World.DrawMarker(MarkerType.DebugSphere, eb.Position, Vector3.Zero, Vector3.Zero, new Vector3(0.25f, 0.25f, 0.25f), Color.Yellow);

                        //Vector2 point = testtest(v);
                        //Function.Call(Hash.DRAW_RECT, point.X, point.Y, 0.002f, 0.002f, 255, 255, 0, 125, false);
                        //main.Sub(point.ToString());
                    }
                    }
                    //Vector3 v = Function.Call<Vector3>(Hash.GET_PED_BONE_COORDS, e.Handle, Bone.SkelHead);
                    //Vector2 point2D;
                    //if (World3DToScreen2D(v, out point2D))
                    //{
                    //    Function.Call(Hash.DRAW_RECT, point2D.X, point2D.Y, 0.1f, 0.1f, 255, 255, 0, 255, false);
                    //}

                }
            }
        }

        public static Vector2 World3DToScreen2D(Vector3 pos)
        {
            var x2dp = new OutputArgument();
            var y2dp = new OutputArgument();

            Function.Call<bool>(Hash.GET_SCREEN_COORD_FROM_WORLD_COORD, pos.X, pos.Y, pos.Z, x2dp, y2dp);
            return new Vector2(x2dp.GetResult<float>(), y2dp.GetResult<float>());
        }

        public static bool TestWorld3DToScreen2D(Vector3 pos, out Vector2 result)
        {
            var x2dp = new OutputArgument();
            var y2dp = new OutputArgument();

            bool success = Function.Call<bool>((Hash)0x34E82F05DF2974F5, pos.X, pos.Y, pos.Z, x2dp, y2dp); //  GET_SCREEN_COORD_FROM_WORLD_COORD and previously, _WORLD3D_TO_SCREEN2D
            result = new Vector2(x2dp.GetResult<float>(), y2dp.GetResult<float>());
            return success;
        }

        public static Vector3 ScreenRelToWorld(Vector3 camPos, Vector3 camRot, Vector2 coord, out Vector3 forwardDirection)
        {
            var camForward = RotationToDirection(camRot);
            var rotUp = camRot + new Vector3(1, 0, 0);
            var rotDown = camRot + new Vector3(-1, 0, 0);
            var rotLeft = camRot + new Vector3(0, 0, -1);
            var rotRight = camRot + new Vector3(0, 0, 1);

            var camRight = RotationToDirection(rotRight) - RotationToDirection(rotLeft);
            var camUp = RotationToDirection(rotUp) - RotationToDirection(rotDown);

            var rollRad = -DegToRad(camRot.Y);

            var camRightRoll = camRight * (float)Math.Cos(rollRad) - camUp * (float)Math.Sin(rollRad);
            var camUpRoll = camRight * (float)Math.Sin(rollRad) + camUp * (float)Math.Cos(rollRad);

            var point3D = camPos + camForward * 1.0f + camRightRoll + camUpRoll;
            Vector2 point2D;
            if (!TestWorld3DToScreen2D(point3D, out point2D))
            {
                forwardDirection = camForward;
                return camPos + camForward * 1.0f;
            }
            var point3DZero = camPos + camForward * 1.0f;
            Vector2 point2DZero;
            if (!TestWorld3DToScreen2D(point3DZero, out point2DZero))
            {
                forwardDirection = camForward;
                return camPos + camForward * 1.0f;
            }

            const double eps = 0.001;
            if (Math.Abs(point2D.X - point2DZero.X) < eps || Math.Abs(point2D.Y - point2DZero.Y) < eps)
            {
                forwardDirection = camForward;
                return camPos + camForward * 1.0f;
            }
            var scaleX = (coord.X - point2DZero.X) / (point2D.X - point2DZero.X);
            var scaleY = (coord.Y - point2DZero.Y) / (point2D.Y - point2DZero.Y);
            var point3Dret = camPos + camForward * 1.0f + camRightRoll * scaleX + camUpRoll * scaleY;
            forwardDirection = camForward + camRightRoll * scaleX + camUpRoll * scaleY;
            return point3Dret;
        }

        public static float DegToRad(float _deg)
        {
            double Radian = (Math.PI / 180) * _deg;
            return (float)Radian;
        }

        public static Vector3 RotationToDirection(Vector3 rotation)
        {
            var z = DegToRad(rotation.Z);
            var x = DegToRad(rotation.X);
            var num = Math.Abs(Math.Cos(x));
            return new Vector3
            {
                X = (float)(-Math.Sin(z) * num),
                Y = (float)(Math.Cos(z) * num),
                Z = (float)Math.Sin(x)
            };
        }

        private void OnShutdown(object sender, EventArgs e)
        {

        }
    }

}
