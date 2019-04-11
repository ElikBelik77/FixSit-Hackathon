import cv2
import socket


def take_image(output_path):
    video_capture = cv2.VideoCapture(0)
    # Check success
    if not video_capture.isOpened():
        raise Exception("Could not open video device")
    # Read picture. ret === True on success
    ret, frame = video_capture.read()
    # Close device
    video_capture.release()
    cv2.imwrite(output_path + "frame.jpg", frame)  # save frame as JPEG file


def initialize_socket():
    # Create a TCP/IP socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_address = ('localhost', 10000)
    print(sys.stderr, 'starting up on %s port %s' % server_address)
    sock.bind(server_address)
    return sock

