from ursina import Ursina, EditorCamera, camera, Keys, Sky, color, Shader as UShader
from panda3d.core import Shader as PandaShader
from camera_control import CameraControl
from floor import Floor
from player import Player

if __name__ == "__main__":
    app = Ursina(title = "Sven's Adventure")

    # global shader
    shader = PandaShader.load(PandaShader.SL_GLSL, vertex="shaders/jitter.vert", fragment="shaders/unlit.frag")
    app.render.setShader(shader)

    # sky
    # sky = Sky(texture=None, shader=UShader(language=UShader.GLSL, vertex="shaders/sky.vert", fragment="shaders/sky.frag"))
    # sky.set_shader_input("groundColor", color.rgb(139, 69, 19))  # Brown color for the ground
    # sky.set_shader_input("topColor", color.rgb(135, 206, 235))  # Light blue color for the top of the sky
    # sky.set_shader_input("horizonColor", color.rgb(70, 130, 180))  # Dark blue color for the horizon
    sky = Sky()

    #entities
    floor = Floor()
    player = Player()
    cam = CameraControl(camera)
    cam.followThirdPerson(player)

    # EditorCamera()

    # global input
    def input(key: str):
        if key == Keys.escape:
            quit()

    app.run()
