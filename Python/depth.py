import cv2
def depth (height,width):
    #resize pixels
    height*=4.09;
    width*=4;
    #find factor of resizing
    f=330/height;
    width*=f;
    #find range
    if (width>=190) and (width<=225):
        return 0;
    elif width<190:
        return 1;
    else:
        return -1

