using Godot;
using System;

public partial class RotateCamera : Node3D
{
    [Export(PropertyHint.Range, "0.1,10.0,0.1")]
    private float speed = 1;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        RotateY(- (float) delta * speed);
	}
}
