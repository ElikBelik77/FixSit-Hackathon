import depth
import posture_model


def main_logic(image_path, model_path):
    body_dictionary = posture_model.get_body_face_json(
                        model_path, image_path, image_path, image_path + r"\frame_keypoints.json")
    print(body_dictionary)
    return '{ "answer":"good" }'
