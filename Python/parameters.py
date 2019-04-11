import math



def depth(nose, chin, earl, earr):
    height = math.sqrt((nose[0]-chin[0])**2+(nose[1]-chin[1])**2)
    width = math.sqrt((earl[0]-earr[0])**2+(earl[0]-earr[0])**2)
    # Find factor of resizing
    f = 110/height
    width *= f
    # Find range
    if 115 <= width <= 150:
        return 0
    elif width < 115:
        return 1
    else:
        return -1



def chin_to_neck(chin, neck):
    d = math.sqrt((chin[0]-neck[0])**2+(chin[1]-neck[1])**2)
    # Classifying
    if 90 <= d <= 125:
        return 0
    elif d < 90:
        return 1
    else:
        return -1



def ears_to_shoulders(earl, earr, shoull, shoulr):
    dl = math.sqrt((earl[0]-shoull[0])**2+(earl[1]-shoull[1])**2)
    dr = math.sqrt((earr[0] - shoulr[0]) ** 2 + (earr[1] - shoulr[1]) ** 2)
    averageDist=(dl+dr)/2
    # Classifying
    if 190 <= averageDist <= 225:
        return 0
    elif averageDist < 180:
        return -1
    else:
        return 1

