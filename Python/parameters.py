import cv2
import math


def depth(hu, hl, wl, wr):
    # Resize pixels
    hu[1] *= 4.09
    hu[0] *= 4
    hl[1] *= 4.09
    hl[0] *= 4
    wl[1] *= 4.09
    wl[0] *= 4
    wr[1] *= 4.09
    wr[0] *= 4
    height = math.sqrt((hu[0]-hl[0])**2+(hu[1]-hl[1])**2)
    width = math.sqrt((wl[0]-wr[0])**2+(wl[0]-wr[0])**2)
    # Find factor of resizing
    f = 330/height
    width *= f
    # Find range
    if 190 <= width <= 225:
        return 0
    elif width < 190:
        return 1
    else:
        return -1


def ears_to_shoulders(earl, earr, shoull, shoulr):
    # Resizing pixels
    earl[1] *= 4.09
    earl[0] *= 4
    earr[1] *= 4.09
    earr[0] *= 4
    shoull[1] *= 4.09
    shoull[0] *= 4
    shoulr[1] *= 4.09
    shoulr[0] *= 4
    dl = math.sqrt((earl[0]-shoull[0])**2+(earl[1]-shoull[1])**2)
    dr = math.sqrt((earr[0] - shoulr[0]) ** 2 + (earr[1] - shoulr[1]) ** 2)
    averageDist=(dl+dr)/2
    # Classifying
    if 325 <= averageDist <= 400:
        return 0
    elif averageDist < 325:
        return -1
    else:
        return 1


def chin_to_breast(chin, breast):
    # Resizing pixels
    chin[0] *= 4
    chin[1] *= 4.09
    breast[0] *= 4
    breast[1] *= 4.09
    d = math.sqrt((chin[0]-breast[0])**2+(chin[1]-breast[1])**2)
    # Classifying
    if 165 <= d <= 220:
        return 0
    elif d < 165:
        return 1
    else:
        return -1

