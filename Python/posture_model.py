import subprocess
import json
import os

# """bin\OpenPoseDemo.exe --image_dir \Users\Naama\Desktop\fixSitting\ --write_json \Users\Naama\Desktop\fixSitting\ --display 0 --face --hand --net_resolution 320x176 --face_net_resolution 320x320 --write_images \Users\Naama\Desktop\fixSitting\"""
MODEL_COMMAND = r"bin\OpenPoseDemo.exe --image_dir {}\ --write_json {}\ --display 0 --write_images {}\ --face --net_resolution 320x176 --face_net_resolution 320x320"


def get_body_face_json(model_path, image_input_path, json_output_path, image_output_path, json_name):
    os.chdir(model_path)
    formatted_command = MODEL_COMMAND.format(image_input_path, json_output_path, image_output_path )
    print(formatted_command)
    formatted_command = formatted_command.split(" ")
    subprocess.run(formatted_command)
    with open(json_name, 'r') as file:
        return parse_json(file.read())


def parse_json(file):
    json_dict = json.loads(file)
    name_dict = {"Nose":0 ,"Neck":1, "RShoulder":2, "LShoulder":5, "REye":15, "LEye":16, "REar":17, "LEar":18}
    body_dict = {}
    # the list of the values
    info = json_dict['people'][0]['pose_keypoints_2d']
    for item in name_dict.items():
        i = item[1] * 3
        body_dict[item[0]] = info[i:i+3]
    # print(body_dict)
    chin = json_dict['people'][0]['face_keypoints_2d']
    body_dict['Chin'] = chin[24:27]
    return body_dict


# if __name__ == '__main__':
#     # parse_json("""{"version":1.2,"people":[{"pose_keypoints_2d":[365.168,266.531,0.878559,337.912,406.363,0.202459,230.925,409.1,0.145328,0,0,0,0,0,0,469.4,417.386,0.377126,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,326.801,233.581,0.91248,387.161,236.293,0.944671,285.673,263.757,0.805041,409.136,266.465,0.567238,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],"face_keypoints_2d":[283.272,241.325,0.627647,279.926,260.061,0.640038,278.588,280.135,0.552835,283.272,299.54,0.456778,291.302,318.945,0.493929,302.677,336.343,0.568218,319.405,349.056,0.501881,338.141,355.747,0.395361,356.208,355.747,0.353497,370.929,355.747,0.649329,381.635,343.703,0.733194,392.341,328.313,0.807457,400.371,312.254,0.745202,405.724,295.525,0.776963,409.069,278.128,0.789259,412.415,261.399,0.808927,413.084,244.002,0.792604,305.353,221.251,0.765442,313.383,214.56,0.86518,324.758,211.214,0.830858,336.803,211.214,0.878522,347.509,215.229,0.915247,371.598,215.898,0.91862,382.304,210.545,0.895061,395.018,211.214,0.875158,405.055,217.236,0.825067,409.739,227.943,0.820994,360.223,231.957,0.913156,360.223,244.671,0.89834,360.892,256.046,0.888585,361.561,268.091,0.842097,348.178,278.128,0.861593,354.2,279.466,0.943268,359.553,280.804,0.903978,364.907,279.466,1.0233,370.26,278.128,0.965999,312.714,236.641,0.906905,322.082,231.957,0.882925,332.788,231.957,0.949001,340.148,238.649,0.888942,332.119,240.656,0.93084,320.744,241.325,0.902534,373.605,238.649,0.915853,382.304,232.627,0.927702,392.341,233.296,0.927822,398.363,239.318,0.95087,392.341,241.994,0.96592,381.635,241.325,0.969374,331.45,304.893,0.86979,342.825,298.871,0.887273,352.862,294.187,0.896159,359.553,295.525,0.936407,366.245,294.187,0.915812,374.944,297.533,0.917715,381.635,306.231,0.991397,374.944,312.254,0.909097,366.245,314.93,0.911282,359.553,314.93,0.9003,351.524,312.923,0.860103,342.825,310.246,0.899496,336.134,304.224,0.880653,352.193,302.217,0.89086,359.553,302.886,0.945286,366.245,302.217,0.900558,376.951,305.562,0.915068,366.245,303.555,0.896939,359.553,304.224,0.94523,352.862,303.555,0.898257,327.435,235.303,0.832506,386.319,236.641,0.881488],"hand_left_keypoints_2d":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],"hand_right_keypoints_2d":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],"pose_keypoints_3d":[],"face_keypoints_3d":[],"hand_left_keypoints_3d":[],"hand_right_keypoints_3d":[]}]}""")
#     get_body_face_json(r"\Users\Naama\Downloads\openpose-1.4.0-win64-cpu-binaries\openpose-1.4.0-win64-cpu-binaries",
#                        r"\Users\Naama\Desktop\fixSitting", r"\Users\Naama\Desktop\fixSitting",
#                        r"\Users\Naama\Desktop\fixSitting\frame_keypoints.json")
