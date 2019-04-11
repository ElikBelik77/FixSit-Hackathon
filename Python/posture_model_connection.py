import subprocess
import json

# """bin\OpenPoseDemo.exe --image_dir \Users\Naama\Desktop\fixSitting\ --write_json \Users\Naama\Desktop\fixSitting\ --display 0 --face --hand --net_resolution 320x176 --face_net_resolution 320x320 --write_images \Users\Naama\Desktop\fixSitting\"""
MODEL_COMMAND = """{}OpenPoseDemo.exe --image_dir {}\\ --write_json {}\\ --display 0 --face --hand --net_resolution 320x176 --face_net_resolution 320x320 --write_images {}\\"""


def get_body_face_json(model_path, input_path, output_path):
    formatted_command = MODEL_COMMAND.format(model_path, input_path, output_path, output_path).split(" ")
    subprocess.run(formatted_command)
    return parse_json(output_path.read())


def parse_json(file):
    return json.loads(file)



