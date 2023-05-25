from panda3d.core import NodePath
from direct.actor.Actor import Actor
from ursina import Entity, CapsuleCollider, Vec3, raycast, time, rotate_around_point_2d
from ursina.input_handler import held_keys
from camera_control import CameraTarget

CAM_HEIGHT = 2
SPEED = 2

class Player(Entity, CameraTarget):

    walking = False
    camera_rotation = 0

    def __init__(self):

        Entity.__init__(self)

        # setup collider
        self.collider = CapsuleCollider(self, center=Vec3(0, 0, 0), height=1, radius=1.33017/2)
        self.collider.visible = True

        # setup mesh
        self.actor = Actor("models/sven.gltf")
        self.actor.setH(180) # rotate model
        self.actor.reparent_to(self)
        self.actor.loop("IdleTrack")

        # make target for camera
        self.camera_target = Entity(parent=self, y=CAM_HEIGHT)

    def get_camera_target_origin(self) -> Entity:
        return self.camera_target
    
    def set_control_perspective(self, rotation: float):
        self.camera_rotation = rotation
    
    def on_enable(self):
        pass
    
    def update(self):

        forward = rotate_around_point_2d((0,1), (0,0), self.camera_rotation)
        right = rotate_around_point_2d((1,0), (0,0), self.camera_rotation)
        
        self.direction = Vec3(
            Vec3(forward[0], 0, forward[1]) * (held_keys["w"] - held_keys["s"])
            + Vec3(right[0], 0, right[1]) * (held_keys["d"] - held_keys["a"])
            ).normalized()
        
        feet_ray = raycast(self.position+Vec3(0,0.5,0), self.direction, ignore=(self,), distance=.5, debug=False)
        head_ray = raycast(self.position+Vec3(0,CAM_HEIGHT-.1,0), self.direction, ignore=(self,), distance=.5, debug=False)
        if not feet_ray.hit and not head_ray.hit:

            move_amount = self.direction * time.dt * SPEED

            if raycast(self.position+Vec3(-.0,1,0), Vec3(1,0,0), distance=.5, ignore=(self,)).hit:
                move_amount[0] = min(move_amount[0], 0)
            if raycast(self.position+Vec3(-.0,1,0), Vec3(-1,0,0), distance=.5, ignore=(self,)).hit:
                move_amount[0] = max(move_amount[0], 0)
            if raycast(self.position+Vec3(-.0,1,0), Vec3(0,0,1), distance=.5, ignore=(self,)).hit:
                move_amount[2] = min(move_amount[2], 0)
            if raycast(self.position+Vec3(-.0,1,0), Vec3(0,0,-1), distance=.5, ignore=(self,)).hit:
                move_amount[2] = max(move_amount[2], 0)
            self.position += move_amount

            if  move_amount.length() > 0:
                if not self.walking:
                    self.walking = True
                    self.actor.loop("WalkTrack")
            else:
                if self.walking:
                    self.walking = False
                    self.actor.loop("IdleTrack")
        
        # Face direction
        if self.direction.length() > 0:
            self.look_at(self.position + self.direction)
            

    def input(self, key: str):
        pass

    def on_disable(self):
        # stop animations
        self.actor.stop()
