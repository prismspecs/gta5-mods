using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Threading;

// network test
using System.Net;
using System.Net.Sockets;
using System.IO;

public class TCPhook : Script
{
    // network test
    bool socketReady = false;

    TcpClient mySocket;
    NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    String Host = "localhost";
    Int32 Port = 3000;

    public TCPhook()
    {
        this.Tick += onTick;
        this.KeyUp += onKeyUp;
        this.KeyDown += onKeyDown;

        this.Interval = 1000;   // run every second

        UI.Notify("script started");
        Host = Dns.GetHostName();
        UI.Notify("looking for host " + Host);
        setupSocket();

    }

    public void setupSocket()
    {
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
            UI.Notify("connected to " + Host);
        }
        catch (Exception e)
        {
            UI.Notify("Socket error:" + e);
        }
    }


    public void writeSocket(string theLine)
    {
        if (!socketReady)
            return;
        String tmpString = theLine + "\r\n";
        theWriter.Write(tmpString);
        //theWriter.WriteLine(tmpString);
        //UI.ShowSubtitle("sent: " + tmpString, 1000);
        theWriter.Flush();
    }

    // alternative function for sending...
    public void sendData(String data)
    {
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data);
        if (theStream.CanWrite)
        {
            theStream.Write(bytes, 0, bytes.Length);
        }
    }

    public String readSocket()
    {
        if (!socketReady)
            return "";
        if (theStream.DataAvailable)
            return theReader.ReadLine();
        return "";
    }

    public void closeSocket()
    {
        if (!socketReady)
            return;
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }

    public void maintainConnection()
    {
        if (!theStream.CanRead)
        {
            setupSocket();
        }
    }

    private void onTick(object sender, EventArgs e)
    {
        if(theStream.CanRead)
        {
            String incoming = readSocket();
            UI.Notify("Received: " + incoming);

            if(incoming == "Forward")
            {

            }

        } else
        {

        }

        maintainConnection();

    }

    private void onKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void onKeyUp(object sender, KeyEventArgs e)
    {
        //UI.Notify("key pressed");
        if (e.KeyCode == Keys.NumPad3)
        {
            UI.Notify("sending test123 to server");
            writeSocket("test123");
        }

        if (e.KeyCode == Keys.NumPad2)
        {
            // make player move forward...
            Ped player = Game.Player.Character;
            Vector3 newVec = player.Position + player.ForwardVector * 5;
            player.Task.RunTo(newVec);
            sendData(player.Position.ToString());
        }

        /*
        bool is_ped, is_vehicle, is_object; //declare these at the top, they will be used later to mark what kind of entity you hit
        Vector3 camCoords = CAM::GET_GAMEPLAY_CAM_COORD();
        Vector3 farCoords = getCoordsFromCam(5000.0F); //get coords of point 5000m from your crosshair
        Vector3 endCoords, surfaceNormal;
        BOOL hit; //placeholder variables that will be filled by _GET_RAYCAST_RESULT
        Entity entityHit = 0; //also will be filled by RAYCAST_RESULT, but you should clear it to 0/NULL/-1 each time so that you dont pick up the last entity you hit if the ray misses
                              // THESE TWO NATIVES MUST BE CALLED RIGHT ONE AFTER ANOTHER IN ORDER TO WORK:
                              // Cast ray from camera to farCoords:
        int ray = WORLDPROBE::_CAST_RAY_POINT_TO_POINT(camCoords.x, camCoords.y, camCoords.z, farCoords.x, farCoords.y, farCoords.z, -1, player, 7);
        // flag "-1" means it will intersect with anything.
        // "player" means it will ignore the player (player = PLAYER_PED_ID()). Not sure what 7 means.
        // You can change "player" to "0" if you want the ray to be able to hit yourself; or you can change it to another entity you want the ray to ignore.
        WORLDPROBE::_GET_RAYCAST_RESULT(ray, &hit, &endCoords, &surfaceNormal, &entityHit); // fills the bool "hit" with whether or not the ray was stopped before it reached farCoords
                                                                                            // fills "endCoords" with the coords of where the ray was stopped on its way to farCoords
                                                                                            // fills "surfaceNormal" with some kind of offset-coords of where the entity was hit relative to said entity
                                                                                            // fills "entityHit" with the handle of the entity that was hit (if it was an entity)
                                                                                            //If you want to use the entityHit for something, use code like below:
        if (hit && ENTITY::DOES_ENTITY_EXIST(entityHit)) {     active = true; //use an "active" bool if you need it for what you're doing
            is_ped, is_vehicle, is_object = false; //reset these from what they were with the last entityHit
            switch (ENTITY::GET_ENTITY_TYPE(entityHit)) {
                //GET_ENTITY_TYPE returns 1 if ped, 2 if veh and 3 if object (and 0 if none of those).
                case 1:
                    is_ped = true;
                    break;
                case 2:
                    is_vehicle = true;
                    break;
                case 3:
                    is_object = true;
                    break;
                default:
                    active = false; //something is wrong, the entity hit is not a ped/vehicle/object, so set inactive
                    break;
            }     //the below native will retrieve the offset from the entity's main coords to the coords of where the ray hit the entity
            Vector3 offset = ENTITY::GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS(entityHit, endCoords.x, endCoords.y, endCoords.z);
        }
        */
    }
}
