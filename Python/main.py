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



def initialize_socket():
    # Create a TCP/IP socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_address = ('localhost', 10000)
    print(sys.stderr, 'starting up on %s port %s' % server_address)
    sock.bind(server_address)
    return sock




POSTURE_REQUEST = "posture"

if __name__ == '__main__':
    sock = initialize_socket()
    sock.listen(1)
    while True:
        # Wait for a connection
        print(sys.stderr, 'waiting for a connection')
        connection, client_address = sock.accept()
        try:
            print(sys.stderr, 'connection from', client_address)
            # Receive the request
            while True:
                request = connection.recv(16)
                if request == POSTURE_REQUEST:
                    pass
                else:
                    connection.send("Err")
        finally:
            # Clean up the connection
            connection.close()