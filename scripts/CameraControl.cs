using Godot;
using System;

public partial class CameraControl : Node3D
{
    [Export(PropertyHint.Range, "1.0,10.0,1.0")]
    private float mouseSensitivity = 5.0f;

    [Export]
    private Vector3 offset = Vector3.Up * 1.0f;

    private Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Captured;
        player = GetTree().GetNodesInGroup("Player")[0] as Player;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        GlobalPosition = player.GlobalPosition + offset;
	}

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseMotion inputEventMouseMotion)
        {
            // TODO inputEventMouseMotion.Velocity?? RotateX(/* deg to rad*/) ...ect
            Rotation = new Vector3(
                Mathf.Clamp(Rotation.X - inputEventMouseMotion.Relative.Y / 1_000 * mouseSensitivity, -1.0f, 0.5f),
                Rotation.Y - inputEventMouseMotion.Relative.X / 1_000 * mouseSensitivity,
                0.0f
            );

                        

        }
    }
}
