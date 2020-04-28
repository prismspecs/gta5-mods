import processing.net.*;

Server myServer;
Client myClient;

void setup() {
  size(200, 200);
  // Starts a myServer on port 5204
  myServer = new Server(this, 3000); 

  myClient = new Client(this, "0:0:0:0:0:0:0:1", 3000);
}

void draw() {
  //myServer.write("Test" + frameCount + "\n");
  if (myClient.available() > 0) { 
    println(myClient.readString());
  }
}

void keyPressed() {
  //myServer.write("Test" + frameCount);
  myServer.write("Forward\n");
}

// ServerEvent message is generated when a new client connects 
// to an existing server.
void serverEvent(Server someServer, Client someClient) {
  println("We have a new client: " + someClient.ip());
}
