from ursina import Entity

class Floor(Entity):

    def __init__(self):
        Entity.__init__(self, model="models/floor.gltf", collider = "box" )

    def on_enable(self):
        pass
    
    def update(self):
        pass

    def input(self, key: str):
        pass

    def on_disable(self):
        pass
