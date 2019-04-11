# External lib
import sys
import json


# Internal lib
import posture_logic
import utils


POSTURE_REQUEST = "posture_request"
POSTURE_STATUS = "posture_status"
IMAGE_PATH = r""

if __name__ == '__main__':
    sock = utils.initialize_socket()
    sock.listen(1)
    while True:
        # Wait for a connection
        print(sys.stderr, 'waiting for a connection')
        connection, client_address = sock.accept()
        try:
            print(sys.stderr, 'connection from', client_address)
            # Receive the request
            while True:
                request = connection.recv(5)
                if not request:
                    print(sys.stderr, 'no more data from', client_address)
                    break
                size = int(request)
                request = connection.recv(size)
                request = json.loads(request)
                if request[POSTURE_REQUEST] == POSTURE_REQUEST:
                    utils.take_image(IMAGE_PATH)
                    result = posture_logic(IMAGE_PATH)
                    connection.send(f"{len(result):05d} {result}".encode('utf-8'))
                else:
                    connection.send(f"00003 Err".encode('utf-8'))
        finally:
            # Clean up the connection
            connection.close()