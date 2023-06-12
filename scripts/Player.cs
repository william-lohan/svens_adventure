using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [Export]
    private CameraControl cameraControl;

	public const float Speed = 4.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    private PackedScene jumpEffect;

    private bool isJumping = false;

    private bool isDoubleJump = false;

    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        jumpEffect = GD.Load<PackedScene>("res://prefabs/jump_effect_prefab.tscn");

        animationPlayer = GetNode<AnimationPlayer>("Sven/AnimationPlayer");
        animationPlayer.Play("IdleTrack");
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (IsOnFloor())
        {
            isJumping = false;
            isDoubleJump = false;
        }
        else
        {
			velocity.Y -= gravity * (float)delta;
        }

		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && (IsOnFloor() || !isDoubleJump))
        {
			velocity.Y = JumpVelocity;

            Node3D jumpEffectInstance = jumpEffect.Instantiate() as Node3D;
            jumpEffectInstance.Position = Position;
            GetParent().AddChild(jumpEffectInstance);

            if (isJumping)
                isDoubleJump = true;

            isJumping = true;
        }

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBack");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        

		if (direction != Vector3.Zero)
		{
            direction = direction.Rotated(Vector3.Up, cameraControl.Rotation.Y);
            Rotation = new Vector3(Rotation.X, cameraControl.Rotation.Y, Rotation.Z);

			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
            animationPlayer.Play("WalkTrack");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
            animationPlayer.Play("IdleTrack");
		}

		Velocity = velocity;
		MoveAndSlide();

        // handle collision
        int count = GetSlideCollisionCount();
        for (int i = 0; i < count; i++)
        {
            KinematicCollision3D collision = GetSlideCollision(i);
            if (collision.GetCollider() is Crate crateCollision)
            {
                crateCollision.Push(Velocity);
            }
        }
	}
}
