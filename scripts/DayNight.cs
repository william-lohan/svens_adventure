using Godot;
using System;

public partial class DayNight : WorldEnvironment
{

    private ProceduralSkyMaterial skyMaterial;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        skyMaterial = Environment.Sky.SkyMaterial as ProceduralSkyMaterial;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
