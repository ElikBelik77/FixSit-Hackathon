import cv2


def depth(height, width):
    f = 330 / height
    width *= f
    if (width >= 190) and (width <= 225):
        return 0
    elif width < 190:
        return 1
    else:
        return -1

