import cv2
import socket
import sys
from matplotlib.patches import Circle
import matplotlib.pyplot as plt


def take_image(output_path):
    video_capture = cv2.VideoCapture(0)
    # Check success
    if not video_capture.isOpened():
        raise Exception("Could not open video device")
    # Read picture. ret === True on success
    ret, frame = video_capture.read()
    # Close device
    video_capture.release()
    try:
        cv2.imwrite(output_path + r"\frame.jpg", frame)  # save frame as JPEG file
    finally:
        pass
    return ret


def initialize_socket():
    # Create a TCP/IP socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_address = ('localhost', 10000)
    print(sys.stderr, 'starting up on %s port %s' % server_address)
    sock.bind(server_address)
    return sock


def draw_circles(image_path, points, circle_size, alpha, tag):
    fig, ax = plt.subplots(1)
    img = plt.imread(image_path)
    ax.set_aspect('equal')
    ax.imshow(img)
    plt.axis('off')
    for x, y in points:
        circ = Circle((x, y),  circle_size, alpha=alpha, color=tag)
        ax.add_patch(circ)
    plt.savefig(image_path)
    # plt.show()
