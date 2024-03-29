# External lib
import sys
import json


# Internal lib
import posture_logic
import utils


POSTURE_REQUEST = "request"
POSTURE_STATUS = "posture_status"
ERROR_ANSWER = '{"answer":"error"}'
CAMERA_ERROR_ANSWER = '{"answer":"camera_error"}'
IMAGE_PATH = r"\Users\Naama\Desktop\fixSitting"
MODEL_PATH = r"\Users\Naama\Downloads\openpose-1.4.0-win64-cpu-binaries\openpose-1.4.0-win64-cpu-binaries"
MSG_LEN = 10


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
                request = connection.recv(MSG_LEN)
                if not request or int(request) == 0:
                    print(sys.stderr, 'no more data from', client_address)
                    break
                size = int(str(request.decode('utf-8')))
                request = str(connection.recv(size).decode('utf-8'))
                parsed = json.loads(request)
                if parsed[POSTURE_REQUEST] == POSTURE_STATUS:
                    succeed = utils.take_image(IMAGE_PATH)
                    if not succeed:
                        print(sys.stderr, 'could not take image - camera error')
                        connection.send(f'{len(CAMERA_ERROR_ANSWER):0{MSG_LEN}d}{CAMERA_ERROR_ANSWER}'.encode('utf-8'))
                        break
                    result = posture_logic.main_logic(IMAGE_PATH, MODEL_PATH)
                    print(result)
                    connection.send(f"{len(result):0{MSG_LEN}d}{result}".encode('utf-8'))
                else:
                    connection.send(f'{len(ERROR_ANSWER):0{MSG_LEN}d}{ERROR_ANSWER}'.encode('utf-8'))
        except Exception as e:
            print(sys.stderr, e)
        finally:
            # Clean up the connection
            connection.close()
