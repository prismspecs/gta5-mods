import socket
import time

HOST = '127.0.0.1'  # Standard loopback interface address (localhost)
PORT = 3000        # Port to listen on (non-privileged ports are > 1023)

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()
    conn, addr = s.accept()
    with conn:
        print('Connected by', addr)
        try:
            while True:
                data = conn.recv(1024)
                print('Received: ', data)
                if not data:
                    break
                conn.sendall(data)
                conn.sendall(b'fuc')
                time.sleep(0.1)
        except KeyboardInterrupt:
            print("press ctrl c to terminate")
            pass
