from ursina import Entity, mouse
from ursina.camera import Camera
from ursina import Vec3, clamp, time, lerp

SENSITIVITY = 40
CAM_SPEED = 10

class CameraTarget():

    # get target camera should track and/or pivot on
    def get_camera_target_origin(self) -> Entity:
        raise NotImplementedError()
    
    # set an adjustment so controls make sense to the user
    def set_control_perspective(self, rotation: float):
        pass
    
class CameraControl(Entity):

    def __init__(self, camera: Camera):
        Entity.__init__(self)

        self.camera = camera
        self.camera.parent = self
        self.camera.position = Vec3(0,0,-3)
        self.camera.rotation = Vec3(0,0,0)

    def followThirdPerson(self, target: CameraTarget):

        if hasattr(self, "current_target"):
            self.current_target.set_control_perspective(0)

        # setup new target
        self.current_target = target
        self.camera_target = target.get_camera_target_origin()
        self.position = self.camera_target.world_position

        # 3rd person
        self.camera.fov = 90
        mouse.locked = True

    # must be called manually / in main.py update loop for now
    def update(self):

        # update y rotation
        self.rotation_y += mouse.velocity[0] * SENSITIVITY

        # update x rotation
        self.rotation_x -= mouse.velocity[1] * SENSITIVITY
        self.rotation_x = clamp(self.rotation_x, -90, 90)

        # update position
        self.position = lerp(self.position, self.camera_target.world_position, CAM_SPEED * time.dt)

        self.current_target.set_control_perspective(self.rotation_y)

