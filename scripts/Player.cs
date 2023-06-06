using Godot;
using System;

public partial class Player : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<AnimationPlayer>("AnimationPlayer").Play("IdleTrack");
	}

}
