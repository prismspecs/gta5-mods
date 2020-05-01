import socket
import time

# how many characters the header length is
# just going to be used to let socket know how much
# data to expect
HEADERSIZE = 10

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# socket.gethostname() returns localhost name
print(socket.gethostname(), "is your hostname")
s.bind((socket.gethostname(), 3000))
s.listen()

while True:
    # now our endpoint knows about the OTHER endpoint
    clientsocket, address = s.accept()
    print(f"Connection from {address} has been established.")

    msg = "Welcome to the server!\n"
    msg = f"{len(msg):<{HEADERSIZE}}"+msg
    #print(msg)

    clientsocket.send(bytes(msg,"utf-8"))

    while True:
        try:
            # send
            msg = f"The time is {time.time()}\n"
            msg = f"{len(msg):<{HEADERSIZE}}"+msg

            print(msg)

            clientsocket.send(bytes(msg,"utf-8"))

            # receive
            data = clientsocket.recv(1023)
            print("received:", data.decode("utf-8"))

            if not data:
                break

            #time.sleep(1)
        except ConnectionResetError:
            print("client disconnected")
            s.close()
            break
        except socket.error as e:
            print("got unhandled error ", e)


# import socket
# import time
#
# HOST = '127.0.0.1'
# PORT = 3000
#
# print("your hostname is", socket.gethostname())
#
# HOST = socket.gethostname();
#
# while True:
#     with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
#         s.bind((HOST, PORT))
#         s.listen()
#         conn, addr = s.accept()
#         with conn:
#             print("connected by", addr)
#             while True:
#                 try:
#                     data = conn.recv(1024)
#                     print("received:", data.decode("utf-8"))
#                     if not data:
#                         break
#                     #conn.sendall(data)
#                     #conn.sendall(b'test\n')
#                     conn.send(bytes("you connected to the server\n", "utf-8"))
#                     time.sleep(0.1)
#                 except ConnectionResetError:
#                     print("client disconnected")
#                     s.close()
#                     break
#                 except socket.error as e:
#                     print("got unhandled error ", e)
