using Godot;
using System;

public partial class DayNight : WorldEnvironment
{

    private ProceduralSkyMaterial skyMaterial;

    private Timer timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        skyMaterial = Environment.Sky.SkyMaterial as ProceduralSkyMaterial;
        timer = new Timer
        {
            WaitTime = 60.0d
        };
        timer.Timeout += ChangeDayNight;
        timer.Start();
        AddChild(timer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void ChangeDayNight()
    {}
}
