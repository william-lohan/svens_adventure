using Godot;
using System;
using System.Data.SqlTypes;

public partial class AnimateThenDestruct : Node3D
{
    [Export]
    private Node3D hasAnimation;

    [Export]
    private string animationName;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        AnimationPlayer animationPlayer = hasAnimation.GetNode<AnimationPlayer>("AnimationPlayer");

        animationPlayer.AnimationFinished += SelfDestruct;

        animationPlayer.Play(animationName);
	}

	private void SelfDestruct(StringName animationFinishedName)
	{
        GetParent().RemoveChild(this);
	}
}
