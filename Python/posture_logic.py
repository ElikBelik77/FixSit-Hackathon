import parameters
import posture_model
import utils
import base64
import PIL
from PIL import Image


def check_for_function(general_dict, func_dict):
    condition = True
    for i in func_dict:
        coordinate_list = general_dict[i]
        chance = coordinate_list[2]
        if chance < 0.001:
            condition = False
            break
    return condition


def get_coordinate(general_dict, func_dict):
    func_coordinate = []
    for i in func_dict:
        coordinate_list = general_dict[i]
        func_coordinate.append([coordinate_list[0], coordinate_list[1]])
    return func_coordinate


def main_logic(image_path, model_path):
    body_dictionary = posture_model.get_body_face_json(
                        model_path,
                       image_path, image_path, r"\Users\Naama\Desktop" ,
                       image_path + r"\frame_keypoints.json")
    # body_dictionary = {'Nose': [365.168, 266.531, 0.878559], 'Neck': [337.912, 406.363, 0.202459], 'RShoulder': [230.925, 409.1, 0.145328], 'LShoulder': [469.4, 417.386, 0.377126], 'REye': [326.801, 233.581, 0.91248], 'LEye': [387.161, 236.293, 0.944671], 'REar': [285.673, 263.757, 0.805041], 'LEar': [409.136, 266.465, 0.567238], 'Chin': [356.208, 355.747, 0.353497]}
    print(body_dictionary)
    states = []
    correct_points = []

    for_func_depth = ["Nose", "Chin", "LEar", "REar"]

    condition = check_for_function(body_dictionary, for_func_depth)
    if condition:
        func_coordinate = get_coordinate(body_dictionary, for_func_depth)
        correct_points.append(func_coordinate[0].copy())
        states.append(parameters.depth(func_coordinate[0], func_coordinate[1],
                                                   func_coordinate[2], func_coordinate[3]))
    else:
        states.append(2)
        correct_points.append(0)

    for_func_chin_to_neck = ["Chin", "Neck"]

    condition = check_for_function(body_dictionary, for_func_chin_to_neck)
    if condition:
        func_coordinate = get_coordinate(body_dictionary, for_func_chin_to_neck)
        correct_points.append(func_coordinate[1].copy())
        states.append(parameters.chin_to_neck(func_coordinate[0], func_coordinate[1]))
    else:
        states.append(2)
        correct_points.append(0)

    for_func_ears_shoulders = ["RShoulder", "LShoulder", "REar", "LEar"]

    condition = check_for_function(body_dictionary, for_func_ears_shoulders)
    if condition:
        func_coordinate = get_coordinate(body_dictionary, for_func_ears_shoulders)
        correct_points.append([func_coordinate[0].copy(), func_coordinate[1].copy()].copy())
        states.append(parameters.ears_to_shoulders(func_coordinate[3], func_coordinate[2],
                                                   func_coordinate[1], func_coordinate[0]))
    else:
        states.append(2)
        correct_points.append(0)
    description = ""
    print(states)
    print(correct_points)
    count = 0
    corrupt = []
    correct = []
    if not states[0] == 0:
        if not states[0] == 2:
            corrupt.append(correct_points[0])
            description = description + "Fix your distance from the screen.\n"
        else:
            count = count + 1
    else:
        correct.append(correct_points[0])
    if not states[1] == 0:
        if not states[1] == 2:
            corrupt.append(correct_points[1])
            description = description + "Tilt your head up.\n"
        else:
            count = count + 1
    else:
        correct.append(correct_points[1])
    if not states[2] == 0:
        if not states[2] == 2:
            corrupt.append(correct_points[2][0])
            corrupt.append(correct_points[2][1])
            description = description + "Fix shoulders location.\n"
        else:
            count = count + 1
    else:
        correct.append(correct_points[2][0])
        correct.append(correct_points[2][1])
    print(corrupt)
    print(correct)

    utils.draw_circles(image_path + r"\frame.jpg", corrupt, 20, 0.4, 'r')
    utils.draw_circles(image_path + r"\frame.jpg", correct, 20, 0.4, 'g')

    if len(corrupt) > 0:
        img = Image.open(image_path + r"\frame.jpg")
        img.resize((800, 600), PIL.Image.ANTIALIAS)
        img.save(image_path + r"\frame.jpg")
        with open(image_path + r"\frame.jpg", "rb") as image_file:
            return '{ "answer":"bad", "description":"' + description + '" , "image":"' + base64.b64encode(image_file.read()).decode("utf-8") + '"}'
    else:
        if count > 1:
            return '{ "answer":"image_error" }'
        return '{ "answer":"good" }'


    # print(body_dict)
if __name__ == '__main__':
    utils.take_image(r"C:\Users\Naama\Desktop\fixSitting")
    main_logic(r"C:\Users\Naama\Desktop\fixSitting", r"C:\Users\Naama\Downloads\openpose-1.4.0-win64-cpu-binaries\openpose-1.4.0-win64-cpu-binaries")
