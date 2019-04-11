# External lib
import sys
import json


# Internal lib
import posture_logic
import utils


POSTURE_REQUEST = "request"
POSTURE_STATUS = "posture_status"
IMAGE_PATH = r"\Users\Naama\Desktop\fixSitting"

if __name__ == '__main__':
    sock = utils.initialize_socket()
    sock.listen(1)
    while True:
        # Wait for a connection
        print(sys.stderr, 'waiting for a connection1 ' )
        connection, client_address = sock.accept()
        try:
            print(sys.stderr, 'connection from', client_address)
            # Receive the request
            while True:
                request = connection.recv(5)
                if not request or int(request) == 0:
                    print(sys.stderr, 'no more data from', client_address)
                    break
                size = int(str(request.decode('utf-8')))
                request = str(connection.recv(size).decode('utf-8'))
                parsed = json.loads(request)
                if parsed[POSTURE_REQUEST] == POSTURE_STATUS:
                    utils.take_image(IMAGE_PATH)
                    result = posture_logic.main_logic(IMAGE_PATH)
                    connection.send(f"{len(result):05d}{result}".encode('utf-8'))
                else:
                    connection.send(f"00003Err".encode('utf-8'))
        except Exception as e :
            print(e)
        finally:
            # Clean up the connection
            connection.close()