import depth
import posture_model


def main_logic(image_path):
    body_dictionary = posture_model.get_body_face_json(
        r"\Users\Naama\Downloads\openpose-1.4.0-win64-cpu-binaries\openpose-1.4.0-win64-cpu-binaries",
                       image_path, image_path,
                       image_path + r"\frame_keypoints.json")
    print(body_dictionary)
    return '{ "answer":"good" }'
