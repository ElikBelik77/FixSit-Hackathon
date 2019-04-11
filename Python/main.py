# External lib
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
    take_image()

