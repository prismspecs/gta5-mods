using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

// network test
using System.Net;
using System.Net.Sockets;
using System.IO;

public class TCPhook
{

    public TCPhook()
    {



    }

    public static void Main()
    {

      // network test
      bool socketReady = false;

      TcpClient mySocket;
      NetworkStream theStream;
      StreamWriter theWriter;
      StreamReader theReader;
      String Host = "localhost";
      Int32 Port = 3000;

      System.Console.WriteLine("script started");
      //Host = Dns.GetHostName();
      System.Console.WriteLine("looking for host " + Host);

      // set up the socket
      System.Console.WriteLine("!");
      mySocket = new TcpClient(Host, Port);
      theStream = mySocket.GetStream();
      theWriter = new StreamWriter(theStream);
      theReader = new StreamReader(theStream);
      socketReady = true;
      System.Console.WriteLine("connected to " + Host);


      // write
      if (socketReady)
      {
        String tmpString = "test" + "\r\n";
        theWriter.Write(tmpString);
        //theWriter.WriteLine(tmpString);
        //UI.ShowSubtitle("sent: " + tmpString, 1000);
        theWriter.Flush();
      }



      // read
      if (socketReady)
        if (theStream.DataAvailable)
             theReader.ReadLine();



      // close
      if (socketReady)
      {
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
      }



    }



    // // alternative function for sending...
    // public void sendData(String data)
    // {
    //     byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data);
    //     if (theStream.CanWrite)
    //     {
    //         theStream.Write(bytes, 0, bytes.Length);
    //     }
    // }


    // public void maintainConnection()
    // {
    //     if (!theStream.CanRead)
    //     {
    //         setupSocket();
    //     }
    // }

    // private void onTick(object sender, EventArgs e)
    // {
    //     if(theStream.CanRead)
    //     {
    //         String incoming = readSocket();
    //         System.Console.WriteLine("Received: " + incoming);
    //
    //         if(incoming == "Forward")
    //         {
    //
    //         }
    //
    //     } else
    //     {
    //
    //     }
    //
    //     System.Console.WriteLine("sending test123 to server");
    //     writeSocket("test123");
    //
    //     maintainConnection();
    //
    // }
}
