import socket

HEADERSIZE = 10

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((socket.gethostname(), 1234))

while True:
    full_msg = ''
    new_msg = True
    while True:
        msg = s.recv(16)
        if new_msg:
            print("new msg len:",msg[:HEADERSIZE])
            msglen = int(msg[:HEADERSIZE])
            new_msg = False

        print(f"full message length: {msglen}")

        full_msg += msg.decode("utf-8")

        print(len(full_msg))

        if len(full_msg)-HEADERSIZE == msglen:
            print("full msg recvd")
            print(full_msg[HEADERSIZE:])
            new_msg = True
            full_msg = ""

        # send
        s.send("test".encode())

'''
import socket
import time

HOST = '127.0.0.1'
PORT = 1234
BUFFER_SIZE = 1024
MESSAGE = "Hello, World!"

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    while 1:
        s.send(MESSAGE.encode())
        data = s.recv(BUFFER_SIZE)
        print("received:", data.decode("utf-8"))
        time.sleep(0.1)
    s.close()
'''
