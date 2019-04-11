# External lib
import socket
import sys
import cv2


# Internal lib
import depth
import posture_model_connection


def take_image():
    video_capture = cv2.VideoCapture(0)
    # Check success
    if not video_capture.isOpened():
        raise Exception("Could not open video device")
    # Read picture. ret === True on success
    ret, frame = video_capture.read()
    # Close device
    video_capture.release()
    cv2.imwrite("frame.jpg", frame)  # save frame as JPEG file







if __name__ == '__main__':
    # Create a TCP/IP socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_address = ('localhost', 10000)
    print(sys.stderr, 'starting up on %s port %s' % server_address)
    sock.bind(server_address)
    # Listen for incoming connections
    sock.listen(1)
    while True:
        # Wait for a connection
        print(sys.stderr, 'waiting for a connection')
        connection, client_address = sock.accept()
        try:
            print(sys.stderr, 'connection from', client_address)

            # Receive the data in small chunks and retransmit it
            while True:
                data = connection.recv(16)
                print(sys.stderr, 'received "%s"' % data)
                if data:
                    print(sys.stderr, 'sending data back to the client')
                    connection.sendall(data)
                else:
                    print(sys.stderr, 'no more data from', client_address)
                    break
        finally:
            # Clean up the connection
            connection.close()