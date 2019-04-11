import subprocess
import json

MODEL_COMMAND = """{}OpenPoseDemo.exe --image_dir {}\ --write_json {}\ --display 0 --render_pose 0 --face --hand"""


def get_body_face_json(model_path, input_path, output_path):
    formatted_command = MODEL_COMMAND.format(model_path, input_path, output_path).split(" ")
    subprocess.run(formatted_command)
    return parse_json(output_path.read())


def parse_json(file):
    return json.loads(file)



